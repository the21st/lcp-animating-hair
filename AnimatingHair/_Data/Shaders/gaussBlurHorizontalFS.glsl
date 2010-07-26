uniform sampler2D RTScene; // the texture with the scene you want to blur
varying vec2 vTexCoord;
 
uniform float blurSize;
vec2 u_Scale;

vec2 gaussFilter[7];

void main()
{
	gaussFilter[0] = vec2( -3.0,	0.015625);
	gaussFilter[1] = vec2(-2.0,	0.09375);
	gaussFilter[2] = vec2(-1.0,	0.234375);
	gaussFilter[3] = vec2(0.0,	0.3125);
	gaussFilter[4] = vec2(1.0,	0.234375);
	gaussFilter[5] = vec2(2.0,	0.09375);
	gaussFilter[6] = vec2(3.0,	0.015625);

	u_Scale = vec2( blurSize, 0 );
	
	vec4 color = vec4( 0.0 );
	vec4 add;
	for( int i = 0; i < 7; i++ )
	{
		add	= texture2D( RTScene, vec2( vTexCoord.x+gaussFilter[i].x*u_Scale.x, vTexCoord.y+gaussFilter[i].x*u_Scale.y ) );
		color.rgb += add.rgb * gaussFilter[i].y;
	}
	
	color.a = texture2D( RTScene, vec2( vTexCoord.x, vTexCoord.y ) ).a;

	gl_FragColor = color;
}