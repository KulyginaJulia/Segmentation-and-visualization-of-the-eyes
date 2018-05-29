using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace GlazSegment
{
    public class Shaders
    {
        public string glVersion;
        public string glslVersion;
        public int BasicProgramID;
        public int BasicVertexShader;
        public int BasicFragmentShader;
  
        public Contur mask;
        public DataLocationShader dataLocation;
        public Isosurfaces Surfaces;

        public Shaders(Data dt)
        {
            glVersion = GL.GetString(StringName.Version);
            glslVersion = GL.GetString(StringName.ShadingLanguageVersion);
            dataLocation = new DataLocationShader(dt);
            Surfaces = new Isosurfaces();

        }
        public void loadVolumeMask(Contur cnt)
        {
            mask = cnt;
            Console.WriteLine("Read");
        }
        public void loadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (System.IO.StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }
        public void InitShaders(Camera cam, int flag_of_mask, string filepathtofragshader, Isosurfaces surf)
        {
            BasicProgramID = GL.CreateProgram();
            loadShader("..//..//ray_casting.vert", ShaderType.VertexShader, BasicProgramID, out BasicVertexShader);
            loadShader(filepathtofragshader, ShaderType.FragmentShader, BasicProgramID, out BasicFragmentShader);
            GL.LinkProgram(BasicProgramID);

            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine("InfoLog:");
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
            dataLocation.Initial(BasicProgramID, flag_of_mask, surf);
            dataLocation.Update(BasicProgramID, flag_of_mask, cam, surf);
        }
    }
}
