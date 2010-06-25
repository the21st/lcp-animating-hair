uniform sampler2D hairTexture;
uniform sampler2D deepOpacityMap;

const float K_a = 0.05;
const float K_d = 0.30;
const float K_s = 0.65;
const float shininess = 180.0;

const float rho_reflect = 0.75;
const float rho_transmit = 0.25;

varying vec3 vertexPos;
varying vec3 lightPos;
varying vec3 eyePos;
varying vec3 hairTangent;
varying float opacityFactor;
varying vec4 shadowCoord;

// TODO: change to uniforms
const float n = 1.0;
const float f = 30.0;
const float width = 800.0;
const float height = 600.0;

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
	
	color.xyz = f_dir * (K_d * diffuse * color.xyz + K_s * specular * lightSpec.xyz);
	//color.xyz = K_d * diffuse * color.xyz + K_s * max(specular, 0.0) * lightSpec.xyz;
	
	color.xyz += K_a * gl_FrontMaterial.ambient.xyz;
	
	
	float shadow = 1.0;
	float tmp;
	vec3 delta; // TODO: prisposob okolnostiam
	delta[0] = 0.02;
	delta[1] = 0.04;
	delta[2] = 0.06;
	vec3 lightCoord = shadowCoord.xyz / shadowCoord.w;
	lightCoord = vec3(0.5) * ( lightCoord + vec3(1.0) );
	float depth = lightCoord.z;
	depth = (2.0 * n) / (f + n - depth * (f - n)); // linearny depth medzi 0 a 1
	float depthStart = texture2D( deepOpacityMap, lightCoord.xy ).a;
	
	depthStart += delta[0];
	if ( depth < depthStart )
	{
		tmp = (depthStart - depth) / delta[0];
		shadow = (1.0 - tmp) * texture2D( deepOpacityMap, lightCoord.xy ).r;
		//shadow = tmp*0.25 + 0.5;
	}
	else
	{
		depthStart += delta[1];
		if ( depth < depthStart )
		{
			tmp = (depthStart - depth) / delta[1];
			shadow = tmp * texture2D( deepOpacityMap, lightCoord.xy ).r;
			shadow += (1 - tmp) * texture2D( deepOpacityMap, lightCoord.xy ).g;
			//shadow = tmp*0.25+0.25;
		}
		else
		{
			depthStart += delta[2];
			if ( depth < depthStart )
			{
				tmp = (depthStart - depth) / delta[2];
				shadow = tmp * texture2D( deepOpacityMap, lightCoord.xy ).g;
				shadow += (1 - tmp) * texture2D( deepOpacityMap, lightCoord.xy ).b;
				//shadow = tmp*0.25;
			}
			else
			{
				shadow = texture2D( deepOpacityMap, lightCoord.xy ).b;
				//shadow = 0.0;
			}
		}
	}
	
	if (shadow < 0 || shadow > 1)
		shadow = 0;
		
	// na fragmenty ktorym prislucha prave 'diera' v depth mape neaplikujem tien
	// (diery = dosledok alpha thresholdingu)
	if ( depthStart > 0.999 )
		shadow = 0;
	
	color.rgb *= ( 1.0 - shadow );
	
	gl_FragColor = color;
}