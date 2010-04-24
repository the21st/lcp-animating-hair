uniform sampler2D tex;
uniform vec3 eye;

varying vec4 pos;

const float n = 1.0;
const float f = 30.0;

void main()
{
	vec2 scrCoord = vec2 (1.0 / 512.0, 1.0 / 512.0) * gl_FragCoord.xy;
	
	vec4 color;
	
	float delta = 0.4;
	
	//float intensity = texture2D( tex, gl_TexCoord[0].st )[3]; // intensity of shadow is the alpha value from texture
	float intensity = texture2D( tex, scrCoord ).x;
	
	//float depth = length( vec4(eye, 1) - pos ); // medzi 5 a 10
	float depth = (2.0 * n) / (f + n - gl_FragCoord.z * (f - n)); // linearny depth medzi 0 a 1
	
	intensity = (2.0 * n) / (f + n - intensity * (f - n)); // linearny depth medzi 0 a 1
	
	float depthStart = gl_FragDepth;
	//float depthStart = (2.0 * n) / (f + n - gl_FragDepth * (f - n));
	
	//float depthEnd = depthStart + 3 * delta;
	
	//float x = depthEnd - depth;
	
	//float z = gl_FragCoord.z / gl_FragCoord.w;
	
	color.r = intensity;
	color.g = intensity;
	color.b = intensity;
	
	//color.z = intensity;
	//
	//x -= delta;
	//if ( x > 0 )
		//color.y = intensity;
	//
	//x -= delta;	
	//if ( x > 0 )
		//color.x = intensity;
	
	gl_FragColor = color;
}