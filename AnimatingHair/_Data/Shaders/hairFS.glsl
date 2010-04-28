uniform sampler2D hairTexture;
uniform sampler2D deepOpacityMap;
uniform sampler2D depthMap;

const float K_a = 0.05;
const float K_d = 0.2;
const float K_s = 0.75;
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
	//lightSpec = vec4(0.0);
	
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
	
	// NOTE: je nutny tento riadok so self-shadows?
	//color.xyz = f_dir * (K_d * diffuse * color.xyz + K_s * specular * lightSpec.xyz);
	color.xyz = K_d * diffuse * color.xyz + K_s * max(specular, 0.0) * lightSpec.xyz;
	
	color.xyz += K_a * gl_FrontMaterial.ambient.xyz;
	
	
	float shadow = 1.0;
	float tmp;
	vec3 delta; // TODO: prisposob okolnostiam
	delta[0] = 0.02;
	delta[1] = 0.04;
	delta[2] = 0.06;
	vec4 shadowCoordinateWdivide = shadowCoord / shadowCoord.w;
	float depth = shadowCoordinateWdivide.z;
	depth = (2.0 * n) / (f + n - depth * (f - n)); // linearny depth medzi 0 a 1
	float depthStart = texture2D( depthMap, shadowCoordinateWdivide.st ).x;
	depthStart = (2.0 * n) / (f + n - depthStart * (f - n)); // linearny depth medzi 0 a 1
	depthStart -= 0.001; // TODO : vyladit
	float asd = depth;
	//color.r = asd;
	//color.g = asd;
	//color.b = asd;
	
	//depthStart += delta[0];
	//if ( depth < depthStart )
	//{
		//tmp = (depthStart - depth) / delta[0];
		//shadow = (1.0 - tmp) * texture2D( deepOpacityMap, shadowCoordinateWdivide.st ).r;
	//}
	//else
	//{
		//depthStart += delta[1];
		//if ( depth < depthStart )
		//{
			//tmp = (depthStart - depth) / delta[1];
			//shadow = tmp * texture2D( deepOpacityMap, shadowCoordinateWdivide.st ).r;
			//shadow += (1 - tmp) * texture2D( deepOpacityMap, shadowCoordinateWdivide.st ).g;
		//}
		//else
		//{
			//depthStart += delta[2];
			//if ( depth < depthStart )
			//{
				//tmp = (depthStart - depth) / delta[2];
				//shadow = tmp * texture2D( deepOpacityMap, shadowCoordinateWdivide.st ).g;
				//shadow += (1 - tmp) * texture2D( deepOpacityMap, shadowCoordinateWdivide.st ).b;
			//}
			//else
			//{
				//shadow = texture2D( deepOpacityMap, shadowCoordinateWdivide.st ).b;
			//}
		//}
	//}
	//
	//color.xyz *= shadow;
	
	//color.xyz = texture2D( deepOpacityMap, gl_TexCoord[0].st ).xyz; // we only need alpha information
	gl_FragColor = color;
}