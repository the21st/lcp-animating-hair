uniform sampler2D tex;
uniform vec3 axis;
uniform vec3 eye;

const float K_a = 0.1;
const float K_d = 0.2;
const float K_s = 0.7;
const float shininess = 180.0;

const float rho_reflect = 0.75;
const float rho_transmit = 0.25;

varying vec4 pos;
varying float opacityFactor;

float vecSin(vec3 a, vec3 b) // a and b are normalized
{
	return length(a - dot(a, b) * b);
}

void main()
{
	vec4 color = texture2D( tex, gl_TexCoord[0].st ); // we only need alpha information
	color[3] = opacityFactor * color[3]; // modify alpha
		
	vec4 lightDiffuse = gl_LightSource[0].diffuse;
	vec4 lightAmbient = gl_LightSource[0].ambient;
	vec4 lightSpec = gl_LightSource[0].specular;
	
	color.xyz = gl_FrontMaterial.diffuse.xyz;
	color.x *= lightDiffuse.x;
	color.y *= lightDiffuse.y;
	color.z *= lightDiffuse.z;
	
	
	vec3 t = normalize( axis );
	
	vec3 l = vec3(normalize( gl_LightSource[0].position - pos ));
	
	float intensity = K_d * vecSin(t, l);
	
	vec3 diffuse = intensity * color.xyz;
	
	
	vec3 e = vec3(normalize( vec4( eye, 1 ) - pos ));
	
	float dotTL = dot( t, l );
	float dotTE = dot( t, e );
	float sinTL = sqrt( 1 - dotTL * dotTL );
	float sinTE = sqrt( 1 - dotTE * dotTE );
	
	vec3 specular = K_s * pow( dotTL * dotTE + sinTL * sinTE, shininess ) * lightSpec.xyz;
	
	
	vec3 crossTL = cross(t, l);
	vec3 crossTE = cross(t, e);
	float K_goldman = dot(crossTL, crossTE) / ( length(crossTL) * length(crossTE) );
	
	float f_dir = rho_reflect * (1 + K_goldman) / 2 + rho_transmit * (1 - K_goldman) / 2;
	
	
	color.xyz = f_dir * (diffuse + specular);
	
	color.xyz += K_a * gl_FrontMaterial.ambient.xyz;
	
	gl_FragColor = color;
}