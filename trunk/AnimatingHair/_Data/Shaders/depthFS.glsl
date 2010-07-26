uniform sampler2D tex;
uniform float alphaTreshold;

varying float opacityFactor;

void main()
{
	vec4 color = texture2D( tex, gl_TexCoord[0].st );
	
	gl_FragDepth = gl_FragCoord.z;
	
	float intensity = color.a;
	intensity *= opacityFactor;
	if ( intensity < alphaTreshold )
	{
		discard;
	}
}