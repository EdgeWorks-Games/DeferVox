#version 330

// Texture Sampler
uniform sampler2D TextureSampler;

// Data coming from the vertex shader
smooth in vec2 fragUV;

// Output color, automatically gets picked up by OpenGL
out vec4 dv_FragColor;

void main()
{
	dv_FragColor = texture(TextureSampler, fragUV);
}