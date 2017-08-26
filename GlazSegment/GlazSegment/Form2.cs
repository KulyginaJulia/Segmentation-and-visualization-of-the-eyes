﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;


namespace GlazSegment
{
    public partial class Form2 : Form
    {
        //**Data**//

        public float[] array_frequency;
        public int[] result;
        public int index_difference;
        int FrameCount;
        public float Volume;
        DateTime NextFPSUpdate = DateTime.Now.AddSeconds(1);
        public Shaders m; 
        public float[] array2;
        public int arraySize;
        bool flag = false;
        public string binaryData;

        //**Functions**//

        void displayFPS()
        {
            if (DateTime.Now >= NextFPSUpdate)
            {
                this.Text = String.Format("CT Visualizer (fps = {0})", FrameCount);
                NextFPSUpdate = DateTime.Now.AddSeconds(1);
                FrameCount = 0;
            }
            FrameCount++;
        }

        public void Draw()
        {
            Console.WriteLine(m.glslVersion);
            Console.WriteLine(m.glVersion);
            m.InitShaders();

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture3D, m.texture);
            GL.EnableVertexAttribArray(m.attribute_vpos);

            Console.WriteLine("OK");

            GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            Console.WriteLine("OK");

            GL.DisableVertexAttribArray(m.attribute_vpos);
            Console.WriteLine("OK");

            GL.UseProgram(0);
        }
        
        //**Calls in the form2**//
        public Form2()
        {
            InitializeComponent();
            glControl1.Invalidate();
        }

        private void button_build_Click(object sender, EventArgs e)
        {
            Volume = m.VX * m.VY * m.VZ;
            Console.WriteLine(Volume);
            array2 = new float[m.arraySize];
            Array.Sort(m.array);
            arraySize = m.arraySize;
            Array.Copy(m.array, array2, arraySize);
//
            calculate_frequency(array2, arraySize);
            chart1.Series.Clear();
            Series SeriesOfPoints_Fat = new Series("Fat");
            SeriesOfPoints_Fat.ChartType = SeriesChartType.Line;
            SeriesOfPoints_Fat.Points.AddXY(0, 0);
            SeriesOfPoints_Fat.Color = Color.Yellow;
            SeriesOfPoints_Fat.BorderWidth = 2;
            for (int i = 0; i < index_difference; i++)
            {
                SeriesOfPoints_Fat.Points.AddXY(array_frequency[i], result[i]);
            }
            chart1.Series.Add(SeriesOfPoints_Fat); //* Volume

        }

        public void calculate_frequency(float[] data, int count)
        {
            int i = 0, f = 1;
            if (count > 1)
            {
                for (i = 0; i < count - 1; i++)
                {
                    if (data[i] != data[i + 1])
                        f++;
                }
                if (data[count - 2] != data[count - 1])
                    f++;

                float[] data_result1 = new float[f];
                int[] data_result2 = new int[f];
                f = -1; int idx;
                for (int j = 0; j < count - 1; j++)
                {
                    f++;
                    idx = 1;
                    while ((j < count - 1) && (data[j] == data[j + 1]))
                    {
                        idx++;
                        j++;
                    }
                    data_result1[f] = data[j];
                    data_result2[f] = idx;
                }
                array_frequency = new float[f];
                result = new int[f];
                index_difference = f;
                for (int j = 0; j < f; j++)
                {
                    array_frequency[j] = data_result1[j];
                    result[j] = data_result2[j];
                }
            }
        }
        
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (flag == true)
            {
                m.loadVolumeData(binaryData);
                Draw();
                glControl1.MakeCurrent();
                glControl1.SwapBuffers();
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (glControl1.Enabled == true)
                while (glControl1.IsIdle)
            {
                displayFPS();
                glControl1.Invalidate();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (Form2.ActiveForm != null)
            {
                Application.Idle += Application_Idle;
                glControl1.Enabled = true;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            glControl1.Enabled = false;
            Hide();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "binary files| *.bin";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                binaryData = dialog.FileName;
                flag = true;
            }
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            m = new Shaders();
            this.DoubleBuffered = true;
        }
    }
}