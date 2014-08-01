#version 330

// Transformation Matrices
uniform mat4 MvpMatrix;

// Data coming into the vertex shader
layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec2 vertexUV;

// Data going to the fragment shader
smooth out vec2 fragUV;

void main()
{
	// Pass the texture UV over to the fragment shader, this will be interpolated
	fragUV = vertexUV;

	gl_Position = MvpMatrix * vec4(vertexPosition, 1.0);
}