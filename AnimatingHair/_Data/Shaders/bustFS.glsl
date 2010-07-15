varying vec4 diffuse, ambientGlobal, ambient;
varying vec3 normal, lightDir, halfVector;
varying float dist;
uniform float deepOpacityMapDistance;

varying vec4 shadowCoord;
uniform sampler2D deepOpacityMap;

// TODO: change to uniforms
const float near = 1.0;
const float far = 30.0;

void main()
{
	vec3 n, halfV, viewV, ldir;
	float NdotL, NdotHV;
	vec4 color = ambientGlobal;
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
	
	/*
	float shadow = 1.0;
	float tmp;
	vec3 delta; // TODO: prisposob okolnostiam
	//delta[0] = 0.02;
	//delta[1] = 0.04;
	//delta[2] = 0.06;
	delta[0] = deepOpacityMapDistance;
	delta[1] = 2 * deepOpacityMapDistance;
	delta[2] = 3 * deepOpacityMapDistance;
	
	vec3 lightCoord = shadowCoord.xyz / shadowCoord.w;
	lightCoord = vec3(0.5) * ( lightCoord + vec3(1.0) );
	float depth = lightCoord.z;
	depth = (2.0 * near) / (far + near - depth * (far - near));
	float depthStart = texture2D( deepOpacityMap, lightCoord.xy ).a;
	
	depthStart += delta[0];
	if ( depth < depthStart )
	{
		tmp = (depthStart - depth) / delta[0];
		shadow = (1.0 - tmp) * texture2D( deepOpacityMap, lightCoord.xy ).r;
	}
	else
	{
		depthStart += delta[1];
		if ( depth < depthStart )
		{
			tmp = (depthStart - depth) / delta[1];
			shadow = tmp * texture2D( deepOpacityMap, lightCoord.xy ).r;
			shadow += (1 - tmp) * texture2D( deepOpacityMap, lightCoord.xy ).g;
		}
		else
		{
			depthStart += delta[2];
			if ( depth < depthStart )
			{
				tmp = (depthStart - depth) / delta[2];
				shadow = tmp * texture2D( deepOpacityMap, lightCoord.xy ).g;
				shadow += (1 - tmp) * texture2D( deepOpacityMap, lightCoord.xy ).b;
			}
			else
			{
				shadow = texture2D( deepOpacityMap, lightCoord.xy ).b;
			}
		}
	}
	
	if (shadow < 0 || shadow > 1)
		shadow = 0;
	
	if ( depthStart > 0.999 ) // na fragmenty ktorym prislucha prave 'diera' v depth mape neaplikujem tien
		shadow = 0;
	
	color.rgb *= ( 1.0 - shadow );
	*/
	
	
	gl_FragColor = color;
}
