﻿#version 440

in vec3 			EntryPoint;
uniform sampler3D   VolumeTex;
uniform float       Z;
uniform float		max_den;
uniform float		min_den;

out vec4 FragColor;

vec4 TransferFunction(float density)
{
	float unnorm_den = (density * (max_den - min_den)) + min_den;
	if ((unnorm_den >= -986.0) && ( unnorm_den < -600.0))
		return vec4(1.0, 1.0, 0.0, density); // Fat
		
	if ((unnorm_den >= 2.0) && (unnorm_den < 30.0))
		return vec4(0.0, 0.0, 1.0, density); // Brain
		
	if ((unnorm_den >= -400) && (unnorm_den < 0.0))
		return vec4(1.0, 0.0, 0.0, density); // Blood
		
	if ((unnorm_den >= 150.0) && (density < 1000))//)
		return vec4(1.0, 1.0, 1.0, density); // Bones
	return vec4(density);
}

void main()
{	
	vec3 pos = vec3((EntryPoint.xy+1)/2, 0.0);
	vec3 exitPoint = vec3(0.5, 0.5, 10.0/Z);
    vec3 dir = exitPoint - pos;
    vec3 deltaDir = vec3(0, 0, 1.0/Z);
    float deltaDirLen = 1.0/Z;
    vec3 voxelCoord = pos;
    vec4 colorAcum = vec4(0.0);
    float density;
    float lengthAcum = 0.0;
    vec4 colorSample;
    vec4 bgColor = vec4(1.0, 1.0, 1.0, 1.0);
	
    for(int i = 0; i < int(Z); i++)
    {
    	density = texture3D(VolumeTex, voxelCoord).a;
		colorSample = TransferFunction(density);
		voxelCoord += deltaDir;
	
    	if (colorSample.a > 0.0) {
    	    colorAcum.rgb += (1.0 - colorAcum.a) * colorSample.rgb * colorSample.a;
    	    colorAcum.a += (1.0 - colorAcum.a) * colorSample.a;
    	}
		
    	lengthAcum += deltaDirLen;
    	if (lengthAcum == 1.0 )
    	{	
    	    colorAcum.rgb = colorAcum.rgb*colorAcum.a + (1 - colorAcum.a)*bgColor.rgb;		
    	    break;
    	}	
    	else if (colorAcum.a > 1.0)
    	{
    	    colorAcum.a = 1.0;
			break;
    	}
		
    }
	FragColor = colorAcum;
}
