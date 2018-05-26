#version 430 core
in vec3 vPosition;
out vec3 EntryPoint;

void main()
{
    EntryPoint = vPosition;
	gl_Position = vec4(vPosition, 1.0);
}