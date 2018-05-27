#version 430 core

#define K_A 0.6
#define K_D 1.0
#define K_S 0.3
#define P 100.0

in vec3 			EntryPoint;
uniform sampler3D   VolumeTex;
uniform sampler3D   MaskTex;
uniform float		X;
uniform float		Y;
uniform float       Z;
uniform vec3 campos;
uniform vec3 view;
uniform vec3 up;
uniform vec3 side;
uniform vec3 cell_size;
uniform vec3 color;
uniform float iso_value;
uniform float flag_mask;

out vec4 FragColor;

struct SCamera
{    
	vec3 Position;
	vec3 View;    
	vec3 Up;     
	vec3 Side;    
	vec2 Scale; 
};
  
struct SRay 
{     
	vec3 Origin;  
	vec3 Direction; 
}; 

SRay GenerateRay (SCamera uCamera)
{
	vec2 coords = ((EntryPoint.xy+1)/2.0 )* uCamera.Scale;  
	vec3 direction = uCamera.View + uCamera.Side * coords.x + uCamera.Up * coords.y;  
	return SRay ( uCamera.Position, normalize(direction));
}
  SCamera initializeDefaultCamera() 
{    
	//** CAMERA **//     
	SCamera camera;
	camera.Position = campos;
    camera.View = view;  
	camera.Up = up;
	camera.Side = side;
	camera.Scale = vec2(1.0); 
	return camera;
}
vec3 Phong2 ( vec3 point, vec3 normal, vec3 color, vec3 LightPosition, vec3 CameraPosition)
{
	vec3 LightDir = -normalize(point-LightPosition);
	float diffuse = max( dot ( LightDir, normal ), 0.0);
	vec3 reflect = reflect ( -LightDir, normal );
	float specular = pow ( max (0.0, dot ( reflect, normalize(CameraPosition-point) ) ), P );
	vec3 result = K_A * color;
	result+= diffuse * ( K_D * color + K_S * specular * vec3(1.0) );
	return result;
}
vec3 Phong (vec3 CameraPosition, vec3 point, vec3 normal, vec3 colorAcum, vec3 LightPosition)
{
	vec3 LightDir = -normalize(point - LightPosition);
	float diffuse = max( dot ( LightDir, normal ), 0.0);
	vec3 view_2 = normalize(CameraPosition - point); 
	vec3 reflected = reflect ( -view_2, normal );
	float specular = pow ( max (0.0, dot ( reflected, LightDir) ), P);
	float dist = length(LightDir);
	float attenuation = 1.0 / (0.5 + 0.0 * dist + 0.02 * dist * dist);
	vec3 result = K_A * colorAcum;
	result+= diffuse * K_D * colorAcum * attenuation;
	result+= K_S * specular * vec3(1.0) * attenuation ;
	return result;
}

bool IntersectBox ( SRay ray, out float start, out float final )
{
	vec3 minimum = vec3(0.0);
	vec3 maximum = vec3(X*cell_size.x, Y*cell_size.y, Z*cell_size.z);
	maximum.x = clamp(maximum.x , 0.0, 1.0);
	maximum.y = clamp(maximum.y , 0.0, 1.0);
	maximum.z = clamp(maximum.z , 0.0, 1.0);
	vec3 OMAX = ( minimum - ray.Origin ) / ray.Direction;
	vec3 OMIN = ( maximum - ray.Origin ) / ray.Direction;
	vec3 MAX = max ( OMAX, OMIN );
	vec3 MIN = min ( OMAX, OMIN );
	final = min ( MAX.x, min ( MAX.y, MAX.z ) );
	start = max ( max ( MIN.x, 0.0), max ( MIN.y, MIN.z ) );	
	return final > start;
}
vec3 IsoNormal(in vec3 arg)
{
	vec3 res;
	vec3 cell = vec3(cell_size.x / X, cell_size.y / Y, cell_size.z / Z);
	res.x = texture(VolumeTex, vec3(arg.x-cell.x,arg.y,arg.z)).x - texture(VolumeTex, vec3(arg.x+cell.x,arg.y,arg.z)).x;
	res.y = texture(VolumeTex, vec3(arg.x,arg.y-cell.y,arg.z)).x - texture(VolumeTex, vec3(arg.x,arg.y+cell.y,arg.z)).x;
	res.z = texture(VolumeTex, vec3(arg.x,arg.y,arg.z-cell.z)).x - texture(VolumeTex, vec3(arg.x,arg.y,arg.z+cell.z)).x;
	return res/cell;
}


const vec3 level_color1 = vec3(1.0);//0.5);	
const vec3 level_color2 = vec3(0.5,0.4,0.4);	
void main()
{		
	SCamera uCamera = initializeDefaultCamera(); 
    SRay ray = GenerateRay(uCamera);  
	vec3 LightPosition = campos;//vec3(0.0, 2.0, -1.0);
	float deltaDirLen = 1.0 / Z;// (cell_size.z * Z);
	
	vec3 norm;
	vec4 colorAcum = vec4(0.0);
	float isoValue =  iso_value;
	vec4 isoColor = vec4(color, 1.0);
	float rightDensityValue = 0.0;
	float final = 100.0;
	float start;
	float leftDensityValue = 0.0;
	vec3 currentPoint = ray.Origin;
	SRay currentRay = SRay(currentPoint, ray.Direction);
	
	if(IntersectBox (currentRay, start, final))
	{
		for (float i = start; i < final; i = i + deltaDirLen)
		{
			currentPoint = ray.Origin + i * ray.Direction;
			if (texture(MaskTex, currentPoint).x == 0)
				continue;
					
			leftDensityValue = texture(VolumeTex, currentPoint).x;

			rightDensityValue = texture(VolumeTex, currentPoint + deltaDirLen * ray.Direction).x;
			if (((leftDensityValue - isoValue) * (rightDensityValue - isoValue))  < 0)
			{
					if((leftDensityValue > isoValue) && (rightDensityValue <= isoValue))
					{
						isoColor.xyz = level_color2;
						currentPoint += deltaDirLen*ray.Direction;
						i++;
						norm = -normalize(IsoNormal(currentPoint));
					}
					else
					{
						isoColor.xyz = level_color1;
						currentPoint += deltaDirLen*ray.Direction;
						i++;					
						norm = normalize(IsoNormal(currentPoint));
					}	
					
					colorAcum.xyz = Phong(uCamera.Position, currentPoint, norm, isoColor.xyz, LightPosition);				
					colorAcum.w = leftDensityValue;
				break;
			}

			}
		}

	FragColor = colorAcum;
}
