uniform sampler2D hairTexture;
uniform sampler2D deepOpacityMap;
uniform sampler2D shadowMap;
uniform float deepOpacityMapDistance;

uniform float K_a;
uniform float K_d;
uniform float K_s;
uniform float shininess;

uniform float rho_reflect;
uniform float rho_transmit;

varying vec3 vertexPos;
varying vec3 lightPos;
varying vec3 eyePos;
varying vec3 hairTangent;

varying float opacityFactor;
varying vec4 shadowCoord;

uniform float near;
uniform float far;

float vecSin(vec3 a, vec3 b) // a and b are normalized
{
	return length(a - dot(a, b) * b);
}

void main()
{
	vec4 color = texture2D( hairTexture, gl_TexCoord[0].st ); // we only need alpha information
	color.a = opacityFactor * color.a; // modify alpha
		
	vec4 lightDiffuse = gl_LightSource[0].diffuse;
	vec4 lightAmbient = gl_LightSource[0].ambient;
	vec4 lightSpec = gl_LightSource[0].specular;
	//lightSpec = vec4( 0.0 );
	
	color.xyz = gl_FrontMaterial.diffuse.xyz;
	color.x *= lightDiffuse.x;
	color.y *= lightDiffuse.y;
	color.z *= lightDiffuse.z;
	
	
	vec3 t = hairTangent;
	vec3 l = normalize( lightPos - vertexPos );
	vec3 e = normalize( eyePos - vertexPos );
	
	float diffuse = vecSin(t, l);
	
	
	float dotTL = dot( t, l );
	float dotTE = dot( t, e );
	float sinTL = sqrt( 1 - dotTL * dotTL );
	float sinTE = sqrt( 1 - dotTE * dotTE );
	
	float specular = pow( dotTL * dotTE + sinTL * sinTE, shininess );
	
	
	vec3 crossTL = cross(t, l);
	vec3 crossTE = cross(t, e);
	float K_goldman = dot(crossTL, crossTE) / ( length(crossTL) * length(crossTE) );
	
	float f_dir = rho_reflect * (1 + K_goldman) / 2 + rho_transmit * (1 - K_goldman) / 2;
	
	//color.xyz = f_dir * (K_d * diffuse * color.xyz + K_s * specular * lightSpec.xyz);
	color.xyz = f_dir * (K_d * diffuse * color.xyz + K_s * max(specular, 0.0) * lightSpec.xyz);
	
	
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
		
	// na fragmenty ktorym prislucha prave 'diera' v depth mape neaplikujem tien
	// (diery = dosledok alpha thresholdingu)
	if ( depthStart > 0.999 )
		shadow = 0;
		
	float depth2 = lightCoord.z - 0.0005;
	float distanceFromLight = texture2D( shadowMap, lightCoord.xy ).z;
 	
	shadow += distanceFromLight < depth2 ? 1.0 : 0.0 ;
	
	shadow = clamp( shadow, 0.0, 0.9 );
	
	color.rgb *= ( 1.0 - shadow );
	
	color.rgb += K_a * gl_FrontMaterial.ambient.xyz;
	
	//color.rgb = vec3( 1 - shadow );
	
	gl_FragColor = color;
}