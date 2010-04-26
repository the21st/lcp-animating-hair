uniform sampler2D depthMap;
uniform sampler2D hairTexture;
uniform vec3 eye;
uniform float dist; // TODO: debugging, will be removed
uniform float alphaTreshold;

varying float opacityFactor;

// TODO: change to uniforms
const float n = 1.0;
const float f = 30.0;
const float size = 512.0;

void main()
{
	vec4 color;
	
	vec3 delta;
	delta[0] = 0.03;
	delta[1] = 0.05;
	delta[2] = 0.07;
	
	float intensity = texture2D( hairTexture, gl_TexCoord[0].st ).a; // intensity of shadow is the alpha value from texture
	intensity *= opacityFactor;
	if ( intensity < alphaTreshold )
	{
		discard;
	}
	
	float depth = (2.0 * n) / (f + n - gl_FragCoord.z * (f - n)); // linearny depth medzi 0 a 1
	
	vec2 scrCoord = vec2 (1.0 / size, 1.0 / size) * gl_FragCoord.xy;
	float depthStart = texture2D( depthMap, scrCoord ).x;
	depthStart = (2.0 * n) / (f + n - depthStart * (f - n)); // linearny depth medzi 0 a 1
	depthStart -= 0.01;
	
	//if ( depthStart > 0.98 )
	//{
		//discard;
	//}
	
	float depthEnd = depthStart + delta[0] + delta[1] + delta[2];
	//float depthEnd = depthStart + delta[0] + delta[1];
	//float depthEnd = depthStart + delta[0];
		
	color.r = intensity;
	color.g = intensity;
	color.b = intensity;
	
	if ( depth > depthStart + 0.2 - dist )
	{
		discard;
	}
	
	gl_FragColor = color;
}