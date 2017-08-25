#version 440

in vec3 			EntryPoint;
uniform sampler3D   VolumeTex;
uniform float       Z;

out vec4 FragColor;

vec4 TransferFunction(float intensity)
{
	if ((intensity >= 0.41) && (intensity < 0.485))
		return vec4(1.0, 1.0, 0.0, intensity); // Fat
		
	if ((intensity >= 0.501) && (intensity < 0.515))
		return vec4(0.0, 0.0, 1.0, intensity); // Brain
		
	if ((intensity >= 0.515)&& (intensity <= 0.54))
		return vec4(1.0, 0.0, 0.0, intensity); // Blood
		
	if ((intensity >= 0.575)) //&& (intensity <= 1.0))// bones
		return vec4(1.0, 1.0, 1.0, intensity);
	return vec4(intensity);
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
    float intensity;
    float lengthAcum = 0.0;
    vec4 colorSample;
    vec4 bgColor = vec4(1.0, 1.0, 1.0, 1.0);
	
    for(int i = 0; i < int(Z); i++)
    {
    	intensity = texture3D(VolumeTex, voxelCoord).r;
		colorSample = TransferFunction(intensity);
		voxelCoord += deltaDir;
	
    	if (colorSample.a > 0.0) {
    	    colorAcum.rgb += (1.0 - colorAcum.a) * colorSample.rgb * colorSample.a;
    	    colorAcum.a += (1.0 - colorAcum.a) * colorSample.a;
    	}
		
    	lengthAcum += deltaDirLen;
    	if (lengthAcum >= 1 )
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
	vec4 invertColorAcum = vec4(1 - colorAcum);
	FragColor = colorAcum;
}
