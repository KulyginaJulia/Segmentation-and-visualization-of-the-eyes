#version 430 core
in vec2 TexCoord;

out vec4 outColor;

// Texture samplers
uniform sampler2D uTextureSlice;
uniform sampler2D uTextureMask;

void main()
{
	// Linearly interpolate between both textures (second texture is only slightly combined)
	// 0.2 - в какой пропорции смешивать картинки
	 outColor = vec4(mix(texture(uTextureSlice, TexCoord), texture(uTextureMask, TexCoord), 0.2).rgb , 1);
}