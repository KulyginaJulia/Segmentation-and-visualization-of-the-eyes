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
        public int mask_Loc;
        public int vbo_position;
        public int attribute_vpos;
        public int uniform_pos;
        public int uniform_view;
        public int uniform_up;
        public int uniform_side;
        public int uniform_max;
        public int uniform_min;
        public int uniform_cell_size;
        public int uniform_color;
        public int uniform_iso_value;

        public int uniform_x;
        public int uniform_y;
        public int uniform_z;
        public float maxDen = 0f, minDen = 0f;
        public Vector3[] vertdata;
        public int X1, Y1, Z1;
        public float VX, VY, VZ;
        public float[] array;
        public float[] mask;
        public int texture;
        public int texture_mask;
        public float iso_value;

        public Vector3 cell_size;

        public int getSize()
        {
            return array.Length;
        }

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
                Console.WriteLine("{0} {1} {2}", VX, VY, VZ);

                int arraySize = X1 * Y1 * Z1;
                array = new float[arraySize];
                for (int i = 0; i < arraySize; ++i)
                {
                    array[i] = (float)Convert.ChangeType(reader.ReadInt16(), typeof(float));
                }
                maxDen = array.Max();
                minDen = array.Min();
                Console.WriteLine("Read");
                Console.WriteLine("min max");
                Console.WriteLine("{0} {1}", minDen, maxDen);
                reader.Close();
            }
            else
                Console.WriteLine("File was not found");

        }
        public void loadVolumeData(float[] arr)
        {
            array = arr;
            Console.WriteLine("Read");
        }
        public void loadVolumeMask(float[] arr)
        {
            mask = arr;
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
        public void InitShaders(Camera cam, float iso_value, Vector3 color1, int flag_of_mask, string filepathtofragshader)
        {
            vertdata = new Vector3[] {
                            new Vector3(-1f, -1f, 0f),
                            new Vector3( 1f, -1f, 0f),
                            new Vector3( 1f, 1f, 0f),
                            new Vector3(-1f, 1f, 0f)
           };
            cell_size = new Vector3(VX, VY, VZ);
            BasicProgramID = GL.CreateProgram();
            loadShader("..//..//ray_casting.vert", ShaderType.VertexShader, BasicProgramID, out BasicVertexShader);
            loadShader(filepathtofragshader, ShaderType.FragmentShader, BasicProgramID, out BasicFragmentShader);

            //Компановка программы
            GL.LinkProgram(BasicProgramID);
            // Проверить успех компановки
            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine("InfoLog:");
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));

            volumeLoc = GL.GetUniformLocation(BasicProgramID, "VolumeTex");
            if (flag_of_mask == 2)
            {
                mask_Loc = GL.GetUniformLocation(BasicProgramID, "MaskTex");
            }
            attribute_vpos = GL.GetAttribLocation(BasicProgramID, "vPosition");
            uniform_x = GL.GetUniformLocation(BasicProgramID, "X");
            uniform_y = GL.GetUniformLocation(BasicProgramID, "Y");
            uniform_z = GL.GetUniformLocation(BasicProgramID, "Z");
            uniform_pos = GL.GetUniformLocation(BasicProgramID, "campos");
            uniform_view = GL.GetUniformLocation(BasicProgramID, "view");
            uniform_up = GL.GetUniformLocation(BasicProgramID, "up");
            uniform_side = GL.GetUniformLocation(BasicProgramID, "side");
            uniform_cell_size = GL.GetUniformLocation(BasicProgramID, "cell_size");
            uniform_color = GL.GetUniformLocation(BasicProgramID, "color");
            uniform_iso_value = GL.GetUniformLocation(BasicProgramID, "iso_value");
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
            GL.TexImage3D(TextureTarget.Texture3D, 0, PixelInternalFormat.R32f, X1, Y1, Z1, 0, OpenTK.Graphics.OpenGL.PixelFormat.Red, PixelType.Float, array);

            if (flag_of_mask == 2)
            {
                texture_mask = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture3D, texture_mask);
                GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture3D, TextureParameterName.TextureWrapR, (int)TextureWrapMode.Repeat);

                GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
                GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
                GL.TexImage3D(TextureTarget.Texture3D, 0, PixelInternalFormat.R32f, X1, Y1, Z1, 0, OpenTK.Graphics.OpenGL.PixelFormat.Red, PixelType.Float, mask);
            }

            //update
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);

            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.UseProgram(BasicProgramID);
            GL.Uniform1(volumeLoc, 0);
            if (flag_of_mask == 2)
                GL.Uniform1(mask_Loc, 1);

            GL.Uniform3(uniform_pos, cam.camera_pos);
            GL.Uniform3(uniform_side, cam.camera_side);
            GL.Uniform3(uniform_up, cam.camera_up);
            GL.Uniform3(uniform_view, cam.camera_view);
            GL.Uniform3(uniform_cell_size, cell_size);
            GL.Uniform3(uniform_color, color1);

            GL.Uniform1(uniform_x, (float)X1);
            GL.Uniform1(uniform_y, (float)Y1);
            GL.Uniform1(uniform_z, (float)Z1);

            Console.WriteLine("iso = {0} ", iso_value);
            
            GL.Uniform1(uniform_iso_value, iso_value);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        }
    }
}
