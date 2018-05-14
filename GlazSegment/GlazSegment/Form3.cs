using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlazSegment
{
    public partial class Form3 : Form
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
        public string filepathtosh = "..//..//ray_casting-knife_t1.frag";
        public string Filename;

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            glControl1.Enabled = false;
            Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (Form3.ActiveForm != null)
            {
                Application.Idle += Application_Idle;
                glControl1.Enabled = true;
            }
        }

        public Form3(List<Point> P, string filename, int width, int height)
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
            //LoadData(filename);
            ConturToArray();
            flag = true;

            InitializeComponent();
            glControl1.Invalidate();
        }
        public void ListtoContur() {
        
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
            GL.Enable(EnableCap.CullFace);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture3D, m.texture);
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

        private void glControl1_Paint_1(object sender, PaintEventArgs e)
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
        /* public void TransferArray() {
             int length = mWidth * mHeight * mDepth;
             Array_with_mask = new float[length];
             int In = 0, Out = 0;
             for (int i = 0; i < length; i++) {
                 if ()
             }

         }
 */

        public void ConturToArray()
        {
            int count = 0;
            int length = mWidth * mHeight * mDepth;

            Contur = new float[length];
            float[,,] Contur_middle = new float[mWidth, mHeight, mDepth];
            for (int i = 0; i < mWidth; i++)
            {
                for (int j = 0; j < mHeight; j++)
                {
                    for (int k = 0; k < mDepth; k++)
                    {
                        // Contur_middle[i, j, k] =  что нибудь придумать с трансформацией
                    }
                }
            }
            for (int i = 0; i < length; i++)
            {
                Contur[i] = 0;
            }
            for (int i = 0; i < length; i++)
            {
                foreach (Point p in Points)
                {
                    int X_new = p.X * mWidth / width;
                    int Y_new = p.Y * mHeight / height;
                    count = TransferFunctionCount(X_new, Y_new, mWidth); //p.X, p.Y, mWidth);
                    Contur[count] = 1; // p.X * mWidth/width;
                    Contur[count + 1] = 1; // p.Y * mHeight / height;
                    Contur[count + 2] = 1 ;
                    count += 3;
                }
            }
        }
        public int TransferFunctionCount(int X, int Y, int width)
        {
            return X * width + Y;
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

                    mData = new float[length];

                    for (int i = 0; i < length; i++)
                    {
                        mData[i] = reader.ReadInt16();

                    }
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
