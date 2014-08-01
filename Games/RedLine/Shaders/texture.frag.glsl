#version 330

// Texture Sampler
uniform sampler2D TextureSampler;

// Data coming from the vertex shader
smooth in vec2 fragUV;

// Output color, automatically gets picked up by OpenGL
out vec3 gl_FragColor;

void main()
{
	gl_FragColor = texture(TextureSampler, fragUV).rgb;
}