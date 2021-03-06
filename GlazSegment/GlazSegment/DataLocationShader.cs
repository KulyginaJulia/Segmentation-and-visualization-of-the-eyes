﻿using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GlazSegment
{
    public class DataLocationShader
    {
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
        public int Color_loc;
        public int Isovalue_loc;

        public int uniform_x;
        public int uniform_y;
        public int uniform_z;
        public Vector3[] vertdata;
        public int texture;
        public int texture_mask;
        public int texture_color;
        public int texture_isovalue;
        public int uniform_count_of_surfaces;
        public Vector3 cell_size;
        public Data dataControl;
        public Contur contur_mask;
        public DataLocationShader(Data dt)
        {
            dataControl = dt;
        }
        public DataLocationShader(Data dt, Contur cnt)
        {
            dataControl = dt;
            contur_mask = cnt;
        }
        public void Initial(int BasicProgramID, int flag_of_mask, Isosurfaces surf)
        {
            vertdata = new Vector3[] {
                            new Vector3(-1f, -1f, 0f),
                            new Vector3( 1f, -1f, 0f),
                            new Vector3( 1f, 1f, 0f),
                            new Vector3(-1f, 1f, 0f)
           };
            cell_size = new Vector3(dataControl.GetVoxelWidth(), dataControl.GetVoxelHeight(), dataControl.GetVoxelDepth());
            volumeLoc = GL.GetUniformLocation(BasicProgramID, "VolumeTex");
            Color_loc = GL.GetUniformLocation(BasicProgramID, "Color");
            Isovalue_loc = GL.GetUniformLocation(BasicProgramID, "iso_value");
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
            uniform_count_of_surfaces = GL.GetUniformLocation(BasicProgramID, "CountOfIsosurfaces");

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
            GL.TexImage3D(TextureTarget.Texture3D, 0, PixelInternalFormat.R32f, dataControl.GetWidth(), dataControl.GetHeight(), dataControl.GetDepth(), 0, OpenTK.Graphics.OpenGL.PixelFormat.Red, PixelType.Float, dataControl.GetData());

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
                GL.TexImage3D(TextureTarget.Texture3D, 0, PixelInternalFormat.R32f, dataControl.GetWidth(), dataControl.GetHeight(), dataControl.GetDepth(), 0, OpenTK.Graphics.OpenGL.PixelFormat.Red, PixelType.Float, contur_mask.GetConturForShader());
            }

            texture_color = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture1D, texture_color);
            GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
           // GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
            GL.TexImage1D(TextureTarget.Texture1D, 0, PixelInternalFormat.R32f, surf.GetArrayColorIsosurf().Length, 0, PixelFormat.Red, PixelType.Float, surf.GetArrayColorIsosurf());

            texture_isovalue = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture1D, texture_isovalue);
            GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            //GL.TexParameter(TextureTarget.Texture1D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
            GL.TexImage1D(TextureTarget.Texture1D, 0, PixelInternalFormat.R32f, surf.isoValue.Count, 0, OpenTK.Graphics.OpenGL.PixelFormat.Red, PixelType.Float, surf.GetArrayIsovalueIsosurf());

        }
        public void Update(int BasicProgramID, int flag_of_mask, Camera cam, Isosurfaces surf)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);

            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.UseProgram(BasicProgramID);
            GL.Uniform1(volumeLoc, 0);
            GL.Uniform1(Color_loc, 1);
            GL.Uniform1(Isovalue_loc, 2);
            if (flag_of_mask == 2)
                GL.Uniform1(mask_Loc, 3);

            GL.Uniform3(uniform_pos, cam.camera_pos);
            GL.Uniform3(uniform_side, cam.camera_side);
            GL.Uniform3(uniform_up, cam.camera_up);
            GL.Uniform3(uniform_view, cam.camera_view);
            GL.Uniform3(uniform_cell_size, cell_size);
           
            GL.Uniform1(uniform_x, (float)dataControl.GetWidth());
            GL.Uniform1(uniform_y, (float)dataControl.GetHeight());
            GL.Uniform1(uniform_z, (float)dataControl.GetDepth());
            GL.Uniform1(uniform_count_of_surfaces, (float)surf.isoValue.Count);

            Console.WriteLine("iso = {0} ", surf.isoValue[surf.isoValue.Count - 1]);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
