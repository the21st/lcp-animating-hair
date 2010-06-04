uniform vec3 axis;
uniform vec3 eye;
attribute float sign1, sign2;

const float renderSizeHorizontal = 0.2;
const float renderSizeVertical = 0.5;

varying float opacityFactor;

void main()
{
	gl_TexCoord[0] = gl_MultiTexCoord0;
	
	vec3 vertexPos = vec3( gl_Vertex );
	vec3 hairTangent = normalize( axis );
	vec3 eyePos = eye;
	
	vec3 look = eye - vertexPos;
	look = normalize( look );
	vec3 up = hairTangent;
	vec3 right = cross( up, look );
	
	right = normalize( right );
	look = cross( right, up );
	
	right = -right;
		
	vertexPos += sign1 * renderSizeHorizontal * right + sign2 * renderSizeVertical * up;
	
	gl_Position = gl_ModelViewProjectionMatrix * vec4(vertexPos, 1);
	
	//vec4 v = gl_ModelViewMatrix * gl_Vertex;
	//
	//vec3 vertexPos = vec3( gl_Vertex );
	//
	//vec3 a = normalize( vec3( gl_ModelViewMatrix * vec4(axis, 1) ) );
	//vec2 aProj = a.xy;
	//aProj = normalize(aProj);
	//
	//v.x += sign1 * renderSizeHorizontal * (-aProj.y) + sign2 * renderSizeVertical * aProj.x;
	//v.y += sign1 * renderSizeHorizontal * aProj.x + sign2 * renderSizeVertical * aProj.y;
	//
	//v = gl_ProjectionMatrix * v;
	//gl_Position = v;
	//
	//
	//vec3 d_p = eye - vertexPos;
	//float cosTheta = dot( normalize(d_p), normalize( axis ) );
	//float sinTheta = sqrt( 1 - (cosTheta * cosTheta) );
	//opacityFactor = sinTheta;
	
	
	vec3 d_p = eye - vertexPos;
	float cosTheta = dot( normalize(d_p), hairTangent );
	float sinTheta = sqrt( 1 - (cosTheta * cosTheta) );
	opacityFactor = sinTheta;
}