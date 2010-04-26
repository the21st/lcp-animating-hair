uniform sampler2D tex;
uniform float alphaTreshold;

void main()
{
	vec4 color = texture2D( tex, gl_TexCoord[0].st );
	
	gl_FragDepth = gl_FragCoord.z;
	
	if ( color.a < alphaTreshold )
	{
		//gl_FragDepth = 10.0;
		discard;
	}
}