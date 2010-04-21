uniform vec3 axis;
uniform vec3 eye;
attribute float sign1, sign2;

vec4 v, a;
vec2 a2, pa2;

const float renderSizeHorizontal = 0.2;
const float renderSizeVertical = 0.5;

varying vec4 pos;
varying float opacityFactor;

void main()
{
	gl_TexCoord[0] = gl_MultiTexCoord0;
	
	v = gl_ModelViewMatrix * gl_Vertex;
	
	pos = v;
	
	a = gl_ModelViewMatrix * vec4(axis, 0);
	
	a2 = vec2(a.x, a.y);
	a2 = normalize(a2);
	
	pa2.x = -a2.y;
	pa2.y = a2.x;
	
	v.x += sign1 * renderSizeHorizontal * pa2.x + sign2 * renderSizeVertical * a2.x;
	v.y += sign1 * renderSizeHorizontal * pa2.y + sign2 * renderSizeVertical * a2.y;
	
	gl_Position = gl_ProjectionMatrix * v;
	
	
	vec4 d_p = v - vec4(eye, 1);
	float cosTheta = dot(normalize(d_p), normalize(a));
	float sinTheta = sqrt( 1 - (cosTheta * cosTheta) );
	opacityFactor = sinTheta;
}