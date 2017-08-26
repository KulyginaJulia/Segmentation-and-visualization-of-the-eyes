using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
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

        public int volumeLoc;
        public int vbo_position;
        public int attribute_vpos;

        public Vector3 campos = new Vector3(1.0f, 0, 0);

        // public int uniform_pos;
        public int uniform_z;
        // float size;
        public float maxDen = 0f, minDen = 0f;
        public Vector3[] vertdata;
        public int X1, Y1, Z1;
        public float VX, VY, VZ;
        public float[] array;
        public int texture;
        public int arraySize;

        public Shaders()
        {
            glVersion = GL.GetString(StringName.Version);
            glslVersion = GL.GetString(StringName.ShadingLanguageVersion);

        }
        public void loadVolumeData(string path)
        {
            if (File.Exists(path))
            {
                BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));

                X1 = reader.ReadInt32();
                Y1 = reader.ReadInt32();
                Z1 = reader.ReadInt32();
                Console.WriteLine("{0} {1} {2}", X1, Y1, Z1);

                VX = reader.ReadSingle();
                VY = reader.ReadSingle();
                VZ = reader.ReadSingle();

                arraySize = X1 * Y1 * Z1;
                //array = new short[arraySize];
                array = new float[arraySize];
                for (int i = 0; i < arraySize; ++i)
                {
                    array[i] = reader.ReadInt16(); // 4092 - максимальная плотность в бинарном файле, переводим таким образом плотность в отрезок [0,1]
                    if (array[i] > maxDen)
                        maxDen = array[i];
                    if (array[i] < minDen)
                        minDen = array[i];
                }
                Console.WriteLine("Read");
                reader.Close();

                for (int i = 0; i < arraySize; i++)
                {
                    array[i] = array[i] / (maxDen - minDen);
                }
            }
            else
                Console.WriteLine("File was not found");

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

        public void InitShaders()
        {
            vertdata = new Vector3[] {
                            new Vector3(-1f, -1f, 0f),
                            new Vector3( 1f, -1f, 0f),
                            new Vector3( 1f, 1f, 0f),
                            new Vector3(-1f, 1f, 0f)
           };
            BasicProgramID = GL.CreateProgram();
            loadShader("..//..//ray_casting.vert", ShaderType.VertexShader, BasicProgramID, out BasicVertexShader);
            loadShader("..//..//ray_casting.frag", ShaderType.FragmentShader, BasicProgramID, out BasicFragmentShader);

            //Компановка программы
            GL.LinkProgram(BasicProgramID);
            // Проверить успех компановки
            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine("InfoLog:");
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));

            volumeLoc = GL.GetUniformLocation(BasicProgramID, "VolumeTex");
            attribute_vpos = GL.GetAttribLocation(BasicProgramID, "vPosition");
            uniform_z = GL.GetUniformLocation(BasicProgramID, "Z");

            GL.GenBuffers(1, out vbo_position);


            texture = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture3D, texture);
            GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapR, (int)TextureWrapMode.Repeat);

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
            GL.TexImage3D(TextureTarget.Texture3D, 0, PixelInternalFormat.Intensity, X1, Y1, Z1, 0, OpenTK.Graphics.OpenGL.PixelFormat.Luminance, PixelType.Float, array);
            //update
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);

            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.UseProgram(BasicProgramID);
            //GL.ActiveTexture(TextureUnit.Texture0);
            //GL.BindTexture(TextureTarget.Texture3D, texture);//
            GL.Uniform1(volumeLoc, 0);

            GL.Uniform1(uniform_z, (float)Z1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        }
    }
}
