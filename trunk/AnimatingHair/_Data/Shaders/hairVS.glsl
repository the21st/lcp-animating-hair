uniform vec3 axis;
uniform vec3 eye;
uniform vec3 light;
attribute float sign1, sign2;

uniform float renderSizeHorizontal;
uniform float renderSizeVertical;

varying vec3 vertexPos;
varying vec3 lightPos;
varying vec3 eyePos;
varying vec3 hairTangent;
varying float opacityFactor;
varying vec4 shadowCoord;

void main()
{
	gl_TexCoord[0] = gl_MultiTexCoord0;
	//shadowCoord = gl_TextureMatrix[7] * gl_Vertex;

	vertexPos = vec3( gl_Vertex );
	hairTangent = normalize( axis );
	lightPos = light;
	eyePos = eye;
	
	vec3 look = eye - vertexPos;
	look = normalize( look );
	vec3 up = hairTangent;
	vec3 right = cross( up, look );
	
	right = normalize( right );
	//up = normalize( up ); // nemalo by byt potrebne
	look = cross( right, up );
	
	right = -right;
		
	vertexPos += sign1 * renderSizeHorizontal * right + sign2 * renderSizeVertical * up;
	
	shadowCoord = gl_TextureMatrix[7] * vec4( vertexPos, 1 );
	
	gl_Position = gl_ModelViewProjectionMatrix * vec4(vertexPos, 1);
	
	
	
	//vec4 v = gl_ModelViewMatrix * gl_Vertex;
	//
	//lightPos = light;
	//eyePos = eye;
	//vertexPos = vec3( gl_Vertex );
	//
	//hairTangent = normalize( axis );
	//vec2 aProj2 = hairTangent.xy;
	//aProj2 = normalize(aProj2);
	//
	//vertexPos.x += sign1 * renderSizeHorizontal * (-aProj2.y) + sign2 * renderSizeVertical * aProj2.x;
	//vertexPos.y += sign1 * renderSizeHorizontal * aProj2.x + sign2 * renderSizeVertical * aProj2.y;
	//shadowCoord = gl_TextureMatrix[7] * vec4( vertexPos, 1 );
	//
	//vec3 a = normalize( vec3( gl_ModelViewMatrix * vec4( axis, 1 ) ) );
	//vec2 aProj = a.xy;
	//aProj = normalize(aProj);
	//
	//v.x += sign1 * renderSizeHorizontal * (-aProj.y) + sign2 * renderSizeVertical * aProj.x;
	//v.y += sign1 * renderSizeHorizontal * aProj.x + sign2 * renderSizeVertical * aProj.y;
	//
	//v = gl_ProjectionMatrix * v;
	//gl_Position = v;
	
	
	
	vec3 d_p = eye - vertexPos;
	float cosTheta = dot( normalize( d_p ), hairTangent );
	float sinTheta = sqrt( 1 - (cosTheta * cosTheta) );
	opacityFactor = sinTheta;
}