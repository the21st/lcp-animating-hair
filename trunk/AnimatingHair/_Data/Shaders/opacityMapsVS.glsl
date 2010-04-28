uniform vec3 axis;
uniform vec3 eye;
attribute float sign1, sign2;

const float renderSizeHorizontal = 0.2;
const float renderSizeVertical = 0.5;

varying float opacityFactor;

void main()
{
	gl_TexCoord[0] = gl_MultiTexCoord0;
	
	vec4 v = gl_ModelViewMatrix * gl_Vertex;
	
	vec3 vertexPos = vec3( gl_Vertex );
	
	vec3 hairTangent = normalize( axis );
	vec2 aProj2 = hairTangent.xy;
	aProj2 = normalize(aProj2);
	
	vertexPos.x += sign1 * renderSizeHorizontal * (-aProj2.y) + sign2 * renderSizeVertical * aProj2.x;
	vertexPos.y += sign1 * renderSizeHorizontal * aProj2.x + sign2 * renderSizeVertical * aProj2.y;
	
	vec3 a = normalize( vec3( gl_ModelViewMatrix * vec4(axis, 1) ) );
	vec2 aProj = a.xy;
	aProj = normalize(aProj);
	
	v.x += sign1 * renderSizeHorizontal * (-aProj.y) + sign2 * renderSizeVertical * aProj.x;
	v.y += sign1 * renderSizeHorizontal * aProj.x + sign2 * renderSizeVertical * aProj.y;
	
	v = gl_ProjectionMatrix * v;
	gl_Position = v;
	
	
	vec3 d_p = eye - vertexPos;
	float cosTheta = dot( normalize(d_p), hairTangent );
	float sinTheta = sqrt( 1 - (cosTheta * cosTheta) );
	opacityFactor = sinTheta;
}