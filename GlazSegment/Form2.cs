using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace GlazSegment
{
    public struct Camera
    {
        public Vector3 camera_pos;
        public Vector3 camera_side;
        public Vector3 camera_up;
        public Vector3 camera_view;
    }
    public struct Point_v
    {
        public float x;
        public int y;

        public Point_v(float v1, int v2) : this()
        {
            this.x = v1;
            this.y = v2;
        }
    }

    public partial class Form2 : Form
    {
        //**Data**//
        int FrameCount;
        DateTime NextFPSUpdate = DateTime.Now.AddSeconds(1);
        public Shaders m;
        bool flag = false;

        public static Camera cam;
        public static Vector2 interval_1;
        public static Vector3 color1;

       // public static int count;
        public List<Point_v> fi = new List<Point_v>();
        public string filepathtosh_1 = "..//..//ray_casting.frag";
        public int sh = 0;
        public string filepathtosh_2 = "..//..//ray_casting-knife_t1.frag";
        float iso_value = 0; 
        public Contur contur;
        public Data dataControl;
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
            if (sh == 1)
                m.InitShaders(cam, iso_value, color1, 1, filepathtosh_1);
            else
                m.InitShaders(cam, iso_value, color1, 2, filepathtosh_2);

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture3D, m.dataLocation.texture);
            if (sh == 2)
            {
                GL.ActiveTexture(TextureUnit.Texture1);
                GL.BindTexture(TextureTarget.Texture3D, m.dataLocation.texture_mask);
            }
            GL.EnableVertexAttribArray(m.dataLocation.attribute_vpos);
            GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            GL.DisableVertexAttribArray(m.dataLocation.attribute_vpos);
            GL.UseProgram(0);
        }

        //**Calls in the form2**//
        public Form2(int sh)
        {
            cam = new Camera();
            cam.camera_pos = new Vector3(0, 50, -50);
            cam.camera_view = new Vector3(0, -1, 1);
            cam.camera_up = new Vector3(0, 1, 0);
            cam.camera_side = new Vector3(1, 0, 0);
            interval_1 = new Vector2(1592, 2175);
            color1 = new Vector3(1, 1, 1);

            InitializeComponent();
            glControl1.Invalidate();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Количество пикселей",
            });
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Плотность"
            });
            cartesianChart1.Zoom = ZoomingOptions.Xy;
            this.sh = sh;
        }

        public Form2(Contur contur, int sh)
        {
            cam = new Camera();
            cam.camera_pos = new Vector3(0, 0, -1);
            cam.camera_view = new Vector3(0, 0, 1);
            cam.camera_up = new Vector3(0, 1, 0);
            cam.camera_side = new Vector3(1, 0, 0);
            interval_1 = new Vector2(1592, 2175);
            color1 = new Vector3(1, 1, 1);
            this.contur = new Contur(contur);          
            dataControl = new GlazSegment.Data(contur.GetFilename());
            m = new Shaders(dataControl);
            this.contur.ConturToArray(dataControl.GetWidth(), dataControl.GetHeight(), dataControl.GetDepth());
            m.loadVolumeMask(contur);
            this.sh = sh;
            flag = true;
            InitializeComponent();
            glControl1.Invalidate();
        }
       
        private void button_build_Click(object sender, EventArgs e)
        {
            fi = dataControl.calculate_frequency(fi);

            cartesianChart1.Series.Add(new LineSeries
            {
                Title = "" + contur.GetFilename(),
                Values = new ChartValues<ObservablePoint>(fi.Select(_ => new ObservablePoint(_.x, _.y))),
                PointGeometry = DefaultGeometries.Square,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(107, 185, 69)),
                PointGeometrySize = 5
            });
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (flag == true)
            {  
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
                string binaryData = dialog.FileName;
                dataControl = new GlazSegment.Data(binaryData);
                m = new Shaders(dataControl);

                label_min.Text = dataControl.GetData().Min().ToString();
                label_max.Text = dataControl.GetData().Max().ToString();
                label_isoVal1.Text = ((dataControl.GetData().Min() + dataControl.GetData().Max()) / 2).ToString();
                hScrollBar1.Minimum = int.Parse(label_min.Text);
                hScrollBar1.Maximum = int.Parse(label_max.Text);
                flag = true;
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            iso_value = hScrollBar1.Value;
            label_isoVal1.Text = iso_value.ToString();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void button_char_Click(object sender, EventArgs e)
        {
            dataControl.CalculateStatData();
            label1.Text = "Математическое ожидание = " + dataControl.MathWait;
            label2.Text = "Дисперсия = " + dataControl.Dispers;
            label3.Text = "Среднеквадратичное отклонение = " + dataControl.G;
        }

        private void cartesianChart1_DataClick(object sender, LiveCharts.ChartPoint p)
        {
            //count++;
            //var asPixels = cartesianChart1.Base.ConvertToPixels(p.AsPoint());
            //if (count == 1)
            //{
            //    textBox1.Text = Convert.ToString(p.X);
            //    interval_1.X = Convert.ToInt16(p.X);
            //}
            //if (count == 2)
            //{
            //    textBox2.Text = Convert.ToString(p.X);
            //    interval_1.Y = Convert.ToInt16(p.X);
            //}
            //color1.X = textBox3.BackColor.R / 255;
            //color1.Y = textBox3.BackColor.G / 255;
            //color1.Z = textBox3.BackColor.B / 255;
        }

        private void glControl1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'a':
                    {
                        cam.camera_pos -= Vector3.Multiply(cam.camera_side, 0.1f);
                        break;
                    }
                case 'd':
                    {
                        cam.camera_pos += Vector3.Multiply(cam.camera_side, 0.1f);
                        break;
                    }
                case 'w':
                    {
                        cam.camera_pos += Vector3.Multiply(cam.camera_view, 0.1f);
                        break;
                    }
                case 's':
                    {
                        cam.camera_pos -= Vector3.Multiply(cam.camera_view, 0.1f);
                        break;
                    }
                case 'r':
                    {
                        cam.camera_pos += Vector3.Multiply(cam.camera_up, 0.1f);
                        break;
                    }
                case 'f':
                    {
                        cam.camera_pos -= Vector3.Multiply(cam.camera_up, 0.1f);
                        break;
                    }
                case 'u':
                    {
                        System.Environment.Exit(-1);
                        break;
                    }
            }
        }

        private void button_color_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = label4.BackColor; //textBox3.BackColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
                label4.BackColor = MyDialog.Color;
            color1.X = label4.BackColor.R / 255;
            color1.Y = label4.BackColor.G / 255;
            color1.Z = label4.BackColor.B / 255;
           // count = 0;
            
        }
    }
}
