uniform sampler2D depthMap;
uniform sampler2D hairTexture;
uniform vec3 eye;
uniform float dist;

varying float billboardDepth;

// TODO: change to uniforms
const float n = 1.0;
const float f = 30.0;
const float size = 512.0;

void main()
{
	vec4 color;
	
	float delta = 0.06;
	
	float intensity = texture2D( hairTexture, gl_TexCoord[0].st ).a; // intensity of shadow is the alpha value from texture
	
	//float depth = length( vec4(eye, 1) - pos ); // medzi 5 a 10
	float depth = (2.0 * n) / (f + n - gl_FragCoord.z * (f - n)); // linearny depth medzi 0 a 1
	
	vec2 scrCoord = vec2 (1.0 / size, 1.0 / size) * gl_FragCoord.xy;
	float depthStart = texture2D( depthMap, scrCoord ).x;
	depthStart = (2.0 * n) / (f + n - depthStart * (f - n)); // linearny depth medzi 0 a 1
	depthStart -= 0.01;
	
	float depthEnd = depthStart + 3 * delta;
	
	//float z = gl_FragCoord.z / gl_FragCoord.w;
	
	float bDepth = billboardDepth;
	bDepth -= n;
	bDepth /= (f - n);
	
	color.r = intensity;
	color.g = intensity;
	color.b = intensity;
	//color.r = depth;
	//color.g = depth;
	//color.b = depth;
	
	//vec3 pos2 = pos.xyz;
	//pos2 /= pos.w;
	//float depth2 = length( eye - pos2 );
	//depth2 -= n;
	//depth2 /= (f - n);
	//
	if ( depth > depthStart + 0.15 - dist )
	//if ( bDepth > depthStart + 0.015 - dist )
	{
		discard;
	}
	
	//asd -= 3.0 * delta;
	//if ( asd > 0 )
	//{
		//color.r = intensity;
		//color.g = intensity;
		//color.b = intensity;
	//}
	
	gl_FragColor = color;
}