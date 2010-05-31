uniform sampler2D depthMap;
uniform sampler2D hairTexture;
uniform vec3 eye;
uniform float dist; // TODO: debugging, will be removed
uniform float alphaTreshold;
uniform float intensityFactor;

varying float opacityFactor;

// TODO: change to uniforms
const float n = 1.0;
const float f = 30.0;
const float size = 1024.0;

void main()
{
	vec4 color;
	
	vec3 delta; // TODO: prisposob okolnostiam
	delta[0] = 0.02;
	delta[1] = 0.04;
	delta[2] = 0.06;
	
	float intensity = texture2D( hairTexture, gl_TexCoord[0].st ).a; // intensity of shadow is the alpha value from texture
	intensity *= opacityFactor; // times opacity factor from Vertex shader
	intensity *= intensityFactor;
	
	if ( intensity < alphaTreshold )
	{
		discard;
	}
	
	float depth = (2.0 * n) / (f + n - gl_FragCoord.z * (f - n)); // linearny depth medzi 0 a 1
	
	vec2 scrCoord = vec2 (1.0 / size, 1.0 / size) * gl_FragCoord.xy;
	float depthStart = texture2D( depthMap, scrCoord ).x;
	depthStart = (2.0 * n) / (f + n - depthStart * (f - n)); // linearny depth medzi 0 a 1
	
	//if ( depthStart > 0.98 )
	//{
	//	discard;
	//}
	
	float depthEnd = depthStart + delta[0] + delta[1];
	//float depthEnd = depthStart + delta[0];
	
	color.r = 0;
	color.g = 0;
	color.b = intensity;
	color.a = depthStart;
	
	
	if ( depth < depthEnd )
	{
		color.g = intensity;
	}
	
	depthEnd -= delta[1];
	
	if ( depth < depthEnd )
	{
		color.r = intensity;
	}
	
	gl_FragColor = color;
}