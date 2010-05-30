uniform sampler2D deepOpacityMap;
uniform int mode;

void main()
{
	vec4 color;
	
	color = texture2D( deepOpacityMap, gl_TexCoord[0].st );
	color[0] = color[mode];
	color[1] = color[mode];
	color[2] = color[mode];
	color[3] = 1;
	
	gl_FragColor = color;
}