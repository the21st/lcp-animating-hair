uniform vec3 axis;
uniform vec3 eye;
attribute float sign1, sign2;

uniform float renderSizeHorizontal;
uniform float renderSizeVertical;

uniform mat4 lightModelViewMatrix;

varying float opacityFactor;

void main()
{
	gl_TexCoord[0] = gl_MultiTexCoord0;
	
	vec3 vertexPos = vec3( gl_ModelViewMatrix * gl_Vertex );
	vec3 hairTangent = gl_NormalMatrix * axis;
	hairTangent = normalize( hairTangent );
	vec3 eyePos = vec3( lightModelViewMatrix * vec4( eye, 1 ) );
	
	vec3 look = eyePos - vertexPos;
	look = normalize( look );
	vec3 up = hairTangent;
	vec3 right = cross( up, look );
	right = normalize( right );
	right = -right;
		
	vertexPos += sign1 * renderSizeHorizontal * right + sign2 * renderSizeVertical * up;
	
	gl_Position = gl_ProjectionMatrix * vec4( vertexPos, 1 );
	
	
	
	float cosTheta = dot( look, hairTangent );
	float sinTheta = sqrt( 1 - (cosTheta * cosTheta) );
	opacityFactor = sinTheta;
}