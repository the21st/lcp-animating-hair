uniform vec3 axis;
uniform vec3 eye;
uniform vec3 light;
attribute float sign1, sign2;

uniform float renderSizeHorizontal;
uniform float renderSizeVertical;

uniform mat4 cameraModelViewMatrix;
uniform mat4 lightModelViewMatrix;
uniform mat4 lightProjectionMatrix;

varying vec3 vertexPos;
varying vec3 lightPos;
varying vec3 eyePos;
varying vec3 hairTangent;

varying float opacityFactor;
varying vec4 shadowCoord;

void main()
{
	gl_TexCoord[0] = gl_MultiTexCoord0;
	
	vertexPos = vec3( gl_ModelViewMatrix * gl_Vertex );
	hairTangent = gl_NormalMatrix * axis;
	hairTangent = normalize( hairTangent );
	lightPos = vec3( cameraModelViewMatrix * vec4( light, 1 ) );
	eyePos = vec3( cameraModelViewMatrix * vec4( eye, 1 ) );
	
	vec3 look = eyePos - vertexPos;
	look = normalize( look );
	vec3 up = hairTangent;
	vec3 right = cross( up, look );
	right = normalize( right );
	right = -right;
		
	vertexPos += sign1 * renderSizeHorizontal * right + sign2 * renderSizeVertical * up;
	
	shadowCoord = lightProjectionMatrix * lightModelViewMatrix * gl_ModelViewMatrixInverse * vec4( vertexPos, 1 );
	gl_Position = gl_ProjectionMatrix * vec4( vertexPos, 1 );
	
	
	float cosTheta = dot( look, hairTangent );
	float sinTheta = sqrt( 1 - (cosTheta * cosTheta) );
	opacityFactor = sinTheta;
}