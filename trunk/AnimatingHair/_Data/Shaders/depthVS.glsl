uniform vec3 axis;
uniform vec3 eye;
attribute float sign1, sign2;

vec2 a2, pa2;

const float renderSizeHorizontal = 0.2;
const float renderSizeVertical = 0.5;

void main()
{
	vec4 v = gl_ModelViewMatrix * gl_Vertex;
	
	vec4 a = gl_ModelViewMatrix * vec4(axis, 1);
	
	vec2 a2 = a.xy;
	a2 = normalize(a2);
	
	vec2 pa2;
	pa2.x = -a2.y;
	pa2.y = a2.x;
	
	v.x += sign1 * renderSizeHorizontal * pa2.x + sign2 * renderSizeVertical * a2.x;
	v.y += sign1 * renderSizeHorizontal * pa2.y + sign2 * renderSizeVertical * a2.y;
	
	gl_Position = gl_ProjectionMatrix * v;
}