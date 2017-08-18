#version 430 core
layout (location = 0) in vec2 position;
layout (location = 1) in vec2 texCoord;

out vec2 TexCoord;

void main()
{
	gl_Position = vec4(position, 0, 1.0f);
	// We swap the y-axis by substracing our coordinates from 1. 
	// This is done because most images have the top y-axis inversed with OpenGL's top y-axis.
	TexCoord = (gl_Position.xy + vec2(1, 1)) / 2;
	TexCoord.y = 1 - TexCoord.y;
}