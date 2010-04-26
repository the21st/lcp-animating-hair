uniform sampler2D tex;

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

float vecSin(vec3 a, vec3 b) // a and b are normalized
{
	return length(a - dot(a, b) * b);
}

void main()
{
	vec4 color = texture2D( tex, gl_TexCoord[0].st ); // we only need alpha information
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
	
	
	//color.xyz = f_dir * (K_d * diffuse * color.xyz + K_s * specular * lightSpec.xyz);
	color.xyz = K_d * diffuse * color.xyz + K_s * max(specular, 0.0) * lightSpec.xyz;
	
	color.xyz += K_a * gl_FrontMaterial.ambient.xyz;
	
	gl_FragColor = color;
}