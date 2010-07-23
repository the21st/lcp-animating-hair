varying vec4 diffuse, ambientGlobal, ambient;
varying vec3 normal, lightDir, halfVector;
varying float dist;

varying vec4 shadowCoord;

uniform mat4 lightModelViewMatrix;
uniform mat4 lightProjectionMatrix;

void main()
{	
	vec4 ecPos;
	vec3 aux;
	
	normal = normalize( gl_NormalMatrix * gl_Normal );
	
	/* these are the lines of code to compute the light's direction */
	ecPos = gl_ModelViewMatrix * gl_Vertex;
	aux = vec3(gl_LightSource[0].position-ecPos);
	lightDir = normalize(aux);
	dist = length(aux);

	halfVector = normalize(gl_LightSource[0].halfVector.xyz);
	
	
	// abuses texture coordinate (the bust is not textured) to apply dark texture in the area where hair 'grows'
	vec4 color = gl_FrontMaterial.diffuse;
	float value = gl_MultiTexCoord0.s;
	value = 1 - value;
	value = (value - 0.37) * 2.2;
	value = clamp( value, 0.0, 0.25 );
	color.rgb -= vec3( value );
	
	
	/* Compute the diffuse, ambient and globalAmbient terms */
	diffuse = color * gl_LightSource[0].diffuse;
	
	/* The ambient terms have been separated since one of them */
	/* suffers attenuation */
	ambient = color * gl_LightSource[0].ambient;
	ambientGlobal = color * gl_FrontMaterial.ambient;
	
	shadowCoord = lightProjectionMatrix * lightModelViewMatrix * gl_Vertex;
	gl_Position = ftransform();
} 
