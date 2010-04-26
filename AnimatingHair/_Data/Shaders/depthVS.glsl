uniform vec3 axis;
uniform vec3 eye;
attribute float sign1, sign2;

const float renderSizeHorizontal = 0.2;
const float renderSizeVertical = 0.5;

void main()
{
	gl_TexCoord[0] = gl_MultiTexCoord0;
	vec4 v = gl_ModelViewMatrix * gl_Vertex;
	
	vec4 a = gl_ModelViewMatrix * vec4(axis, 1);
	
	vec2 aProj = a.xy;
	aProj = normalize(aProj);
	
	v.x += sign1 * renderSizeHorizontal * (-aProj.y) + sign2 * renderSizeVertical * aProj.x;
	v.y += sign1 * renderSizeHorizontal * aProj.x + sign2 * renderSizeVertical * aProj.y;
	
	gl_Position = gl_ProjectionMatrix * v;
}