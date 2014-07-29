﻿#version 330

// Transformation Matrices
uniform mat4 MvpMatrix;

// Data coming into the vertex shader
layout(location = 0) in vec3 vertexPosition;
layout(location = 1) in vec3 vertexColor;

// Data going to the fragment shader
flat out vec3 fragColor;

void main()
{
	// Pass the color over to the fragment shader, this will be interpolated
	fragColor = vertexColor;

	gl_Position = MvpMatrix * vec4(vertexPosition, 1.0);
}