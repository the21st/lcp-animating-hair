varying vec4 diffuse, ambientGlobal, ambient;
varying vec3 normal, lightDir, halfVector;
varying float dist;
uniform float deepOpacityMapDistance;

varying vec4 shadowCoord;
uniform sampler2D deepOpacityMap;
uniform sampler2D shadowMap;

uniform float near;
uniform float far;

void main()
{
	vec3 n, halfV, viewV, ldir;
	float NdotL, NdotHV;
	vec4 color = vec4( 0.0 );
	float att;
	
	/* a fragment shader can't write a varying variable, hence we need
	a new variable to store the normalized interpolated normal */
	n = normalize(normal);
	
	/* compute the dot product between normal and normalized lightdir */
	NdotL = max( dot( n, normalize( lightDir ) ), 0.0 );
	
	att = 1.0 / (gl_LightSource[0].constantAttenuation +
				gl_LightSource[0].linearAttenuation * dist +
				gl_LightSource[0].quadraticAttenuation * dist * dist);
				
	color += att * (diffuse * NdotL + ambient);
		
	halfV = normalize( halfVector );
	NdotHV = max( dot( n,halfV ), 0.0 );
	color += att * gl_FrontMaterial.specular * gl_LightSource[0].specular * 
				pow( NdotHV, gl_FrontMaterial.shininess );
	
	
	
	float shadow = 0.0;
	float tmp;
	vec3 delta;
	delta[0] = deepOpacityMapDistance;
	delta[1] = 2 * deepOpacityMapDistance;
	delta[2] = 3 * deepOpacityMapDistance;
	
	vec3 lightCoord = shadowCoord.xyz / shadowCoord.w;
	lightCoord = vec3(0.5) * ( lightCoord + vec3(1.0) );
	float depth = lightCoord.z;
	depth = (2.0 * near) / (far + near - depth * (far - near)); // linearny depth medzi 0 a 1
	float depthStart = texture2D( deepOpacityMap, lightCoord.xy ).a;
	
	depthStart += delta[0];
	if ( depth < depthStart )
	{
		tmp = (depthStart - depth) / delta[0];
		shadow = (1.0 - tmp) * texture2D( deepOpacityMap, lightCoord.xy ).r;
		//shadow = 0;
	}
	else
	{
		depthStart += delta[1];
		if ( depth < depthStart )
		{
			tmp = (depthStart - depth) / delta[1];
			shadow = tmp * texture2D( deepOpacityMap, lightCoord.xy ).r;
			shadow += (1 - tmp) * texture2D( deepOpacityMap, lightCoord.xy ).g;
			//shadow = 0.333;
		}
		else
		{
			depthStart += delta[2];
			if ( depth < depthStart )
			{
				tmp = (depthStart - depth) / delta[2];
				shadow = tmp * texture2D( deepOpacityMap, lightCoord.xy ).g;
				shadow += (1 - tmp) * texture2D( deepOpacityMap, lightCoord.xy ).b;
				//shadow = 0.666;
			}
			else
			{
				shadow = texture2D( deepOpacityMap, lightCoord.xy ).b;
				//shadow = 1;
			}
		}
	}
	
	if (shadow < 0 || shadow > 1)
		shadow = 0;
		
	if ( depthStart > 0.999 )
		shadow = 0;
		
	float depth2 = lightCoord.z - 0.001;
	float distanceFromLight = texture2D( shadowMap, lightCoord.xy ).z;
 	
 	shadow += distanceFromLight < depth2 ? 1.0 : 0.0 ;
	
	shadow = clamp( shadow, 0.0, 0.9 );
	
	color.rgb *= ( 1.0 - shadow );
	
	color += ambientGlobal;
	
	gl_FragColor = color;
}
