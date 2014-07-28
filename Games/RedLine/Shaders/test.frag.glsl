#version 330

// Data coming from the vertex shader
in vec3 fragColor;

// Output color, automatically gets picked up by OpenGL
out vec3 gl_FragColor;

void main()
{
	gl_FragColor = fragColor;
}