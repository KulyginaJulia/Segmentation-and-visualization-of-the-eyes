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
using System.Drawing;

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
        //  public static Vector2 interval_1;
        //  public static Vector3 color1;

        public List<Point_v> fi = new List<Point_v>();
        public string filepathtosh_1 = "..//..//ray_casting.frag";
        public int sh = 0;
        public string filepathtosh_2 = "..//..//ray_casting-knife_t1.frag";
        float iso_value = 0;
        public Contur contur;
        public Data dataControl;
        public Isosurfaces Surfaces;
        public Isosurf currentIsosurface;
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
                m.InitShaders(cam, 1, filepathtosh_1, Surfaces);
            else
                m.InitShaders(cam, 2, filepathtosh_2, Surfaces);

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture3D, m.dataLocation.texture);

            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture1D, m.dataLocation.texture_color);
            GL.ActiveTexture(TextureUnit.Texture2);
            GL.BindTexture(TextureTarget.Texture1D, m.dataLocation.texture_isovalue);
            if (sh == 2)
            {
                GL.ActiveTexture(TextureUnit.Texture3);
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
            //   interval_1 = new Vector2(1592, 2175);
            //   color1 = new Vector3(1, 1, 1);

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
            dataGridView1.Rows.Add();
            currentIsosurface = new Isosurf();
            Surfaces = new Isosurfaces();
            FillFirstRow();
        }
        public void FillFirstRow()
        {
            dataGridView1[1, 0].Value = 50;
            dataGridView1[2, 0].Style.BackColor = System.Drawing.Color.White;
            dataGridView1[3, 0].Value = 50;
            Color current = dataGridView1[2, 0].Style.BackColor;
            currentIsosurface.Color = new Vector4(current.R / 255, current.G / 255, current.B / 255, float.Parse(dataGridView1[3, 0].Value.ToString()) / 100);
            currentIsosurface.iso_value = (int)dataGridView1[1, 0].Value;
            Surfaces.Add(currentIsosurface);
            currentIsosurface.Clear();

        }
        public Form2(Contur contur, int sh)
        {
            cam = new Camera();
            cam.camera_pos = new Vector3(0, 0, -1);
            cam.camera_view = new Vector3(0, 0, 1);
            cam.camera_up = new Vector3(0, 1, 0);
            cam.camera_side = new Vector3(1, 0, 0);
            //    interval_1 = new Vector2(1592, 2175);
            //   color1 = new Vector3(1, 1, 1);
            this.contur = new Contur(contur);
            dataControl = new GlazSegment.Data(contur.GetFilename());
            m = new Shaders(dataControl);
            this.contur.ConturToArray(dataControl.GetWidth(), dataControl.GetHeight(), dataControl.GetDepth());
            m.loadVolumeMask(contur);
            this.sh = sh;
            flag = true;
            InitializeComponent();
            dataGridView1.Rows.Add();
            currentIsosurface = new Isosurf();
            Surfaces = new Isosurfaces();
            FillFirstRow();
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
                label_isoVal1.Text = dataGridView1[1, 0].Value.ToString();//((dataControl.GetData().Min() + dataControl.GetData().Max()) / 2).ToString();
                hScrollBar1.Minimum = int.Parse(label_min.Text);
                hScrollBar1.Maximum = int.Parse(label_max.Text);
                flag = true;
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            iso_value = hScrollBar1.Value;
            if (dataGridView1[1, currP.X].Value == null)
            {
                dataGridView1[1, currP.X].Value = iso_value;
                currentIsosurface.iso_value = iso_value;
            }
            else
            {
                float oldvalue = float.Parse(dataGridView1[1, currP.X].Value.ToString());
                dataGridView1[1, currP.X].Value = iso_value;
                int index = Surfaces.isoValue.FindIndex(ind => ind.Equals(oldvalue));
                if (index != -1)
                {
                    Surfaces.isoValue[index] = iso_value;
                }
                else
                {
                    currentIsosurface.iso_value = iso_value;
                }
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        Point currP = new Point();
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            currP.X = e.RowIndex;
            currP.Y = e.ColumnIndex;
            if (currP.Y == 2)
            {
                ColorDialog MyDialog = new ColorDialog();
                MyDialog.AllowFullOpen = false;
                MyDialog.ShowHelp = true;
                MyDialog.Color = dataGridView1[2, e.RowIndex].Style.BackColor;
                if (MyDialog.ShowDialog() == DialogResult.OK)
                    dataGridView1[2, e.RowIndex].Style.BackColor = MyDialog.Color;

                Color current = dataGridView1[2, e.RowIndex].Style.BackColor;
                currentIsosurface.Color = new Vector4(current.R / 255, current.G / 255, current.B / 255, 0f);
            }
            if (currP.Y == 1)
            {
                dataGridView1[1, e.RowIndex].Value = null;
            }
        }


        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                currentIsosurface.Color.W = float.Parse(dataGridView1[3, currP.X].Value.ToString()) / 100;
                Surfaces.Add(currentIsosurface);
            }
        }

        Point CurrentPoint = new Point();
        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            CurrentPoint.X = e.X;
            CurrentPoint.Y = e.Y;
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            Point delta = new Point();
            delta.X = CurrentPoint.X - e.X;
            delta.Y = CurrentPoint.Y - e.Y;

            //  updateMouse(delta);
        }

        void updateMouse(Point delta)
        {
            float cur_x, cur_y;
            float a1 = 0f, a2 = 0f;
            float radius = 1;

            cur_x = delta.X;
            cur_y = delta.Y;

            a2 -= (float)Math.Atan2(cur_y, radius);

            a1 += (float)Math.Atan2(cur_x, radius);
            Console.WriteLine("Rotate");
            CurrentPoint.X = (int)(3 * glControl1.Width / 2.0f);
            CurrentPoint.Y = (int)(3 * glControl1.Height / 2.0f);


            Vector3 lookat = new Vector3();

            lookat.X = (float)(Math.Sin(a1) * Math.Cos(a2));
            lookat.Y = (float)(Math.Sin(a2));
            lookat.Z = (float)(Math.Cos(a1) * Math.Cos(a2));

            Matrix4 view = Matrix4.LookAt(cam.camera_pos, cam.camera_pos + lookat, Vector3.UnitY);

            cam.camera_side = -view.Column0.Xyz;
            cam.camera_up = view.Column1.Xyz;
            cam.camera_view = -view.Column2.Xyz;
        }

    }
}
