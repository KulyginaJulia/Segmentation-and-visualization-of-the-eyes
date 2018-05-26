using System;
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
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace GlazSegment
{
    public partial class Form4 : Form
    {
        public int mWidth, mHeight, mDepth;
        public float mXScale, mYScale, mZScale;
        public float[] mData;
        public float[] Contur;
        public float[] Array_with_mask;
        public List<Point> Points = new List<Point>();
        public int width, height;
        DateTime NextFPSUpdate = DateTime.Now.AddSeconds(1);
        int FrameCount;
        public Shaders m;
        bool flag = false;
        public static Camera cam;
        public static Vector2 interval_1;
        public static Vector3 color1;
        public string filepathtosh = "..//..//ray_casting-knife_t2.frag";
        public string Filename;
        public Form4(List<Point> P, string filename, int width, int height)
        {
            cam = new Camera();
            cam.camera_pos = new Vector3(0, 0, -1);
            cam.camera_view = new Vector3(0, 0, 1);
            cam.camera_up = new Vector3(0, 1, 0);
            cam.camera_side = new Vector3(1, 0, 0);
            interval_1 = new Vector2(1592, 2175);
            color1 = new Vector3(1, 1, 1);
            Points = P;
            this.width = width;
            this.height = height;
            Filename = filename;
            LoadData(filename);
           // ConturToArray();
            flag = true;

            InitializeComponent();
            glControl1.Invalidate();
        }
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
            m.InitShaders(cam, interval_1, color1, true, filepathtosh);

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.CullFace);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture3D, m.texture);
            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture3D, m.texture_mask);
            GL.EnableVertexAttribArray(m.attribute_vpos);

            GL.DrawArrays(PrimitiveType.Quads, 0, 4);

            GL.DisableVertexAttribArray(m.attribute_vpos);

            GL.UseProgram(0);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            m = new Shaders();
            this.DoubleBuffered = true;
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (flag == true)
            {
                // m.loadVolumeData(mData);
                m.loadVolumeData(Filename);
                m.loadVolumeMask(Contur);
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
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            glControl1.Enabled = false;
            Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (Form4.ActiveForm != null)
            {
                Application.Idle += Application_Idle;
                glControl1.Enabled = true;
            }
        }
        public bool LoadData(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open));

                    mWidth = reader.ReadInt32();
                    mHeight = reader.ReadInt32();
                    mDepth = reader.ReadInt32();

                    mXScale = reader.ReadSingle();
                    mYScale = reader.ReadSingle();
                    mZScale = reader.ReadSingle();

                    int length = mWidth * mHeight * mDepth;
                    reader.Close();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("File was not reader" + fileName);
                return false;
            }

        }
    }
}
