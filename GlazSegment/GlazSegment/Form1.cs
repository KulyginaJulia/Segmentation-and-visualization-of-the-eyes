using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlazSegment
{
    public partial class Form1 : Form
    {
        Volume mLVolume, mRVolume;
        Control mControl = new Control();


        Color[] colorBox = new Color[4];
        int plane = 0;
        public Form1()
        {
            colorBox[0] = Color.Red;
            colorBox[1] = Color.Green;
            colorBox[2] = Color.Blue;
            colorBox[3] = Color.Yellow;
            InitializeComponent();
            cForm.SelectedIndex = 0;
            cColor.SelectedIndex = 0;
            cSize.SelectedIndex = 0;
        }
        private void rePaint()
        {
            if ((radiobutton_x_y.Checked) && (!radioButton_x_z.Checked) && (!radioButton_y_z.Checked))
            {
                plane = 1;
            }
            else if ((!radiobutton_x_y.Checked) && (!radioButton_x_z.Checked) && (radioButton_y_z.Checked))
            {
                plane = 2;
            }
            else
            {
                plane = 3;
            }
            mLVolume.LoadTexture(currentlayer.Value, int.Parse(window_max.Text), int.Parse(window_min.Text), plane);
            glControl1.Refresh();

            mRVolume.LoadTexture(currentlayer.Value, int.Parse(window_max.Text), int.Parse(window_min.Text), plane);
            glControl2.Refresh();

            glControl1.Refresh();
            glControl2.Refresh();
        }
        private void glControl1_Load(object sender, EventArgs e)
        {
            mLVolume = new Volume();
            mControl.lVolume = mLVolume;
            this.DoubleBuffered = true;
        }
        private void glControl2_Load(object sender, EventArgs e)
        {
            mRVolume = new Volume();
            mControl.rVolume = mRVolume;
            this.DoubleBuffered = true;
        }
        private void openandloadmem(string side)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.Filter = "bin files (*.bin)|*.bin|bin files (*.bin)|*.bin";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName.ToString()))
                {
                    if (side == "left")
                    {
                        if (mLVolume.LoadData(openFileDialog1.FileName.ToString()))
                            PathLeft.Text = openFileDialog1.FileName.ToString();
                    }
                    else
                    {
                        if (mRVolume.LoadData(openFileDialog1.FileName.ToString()))
                            PathRight.Text = openFileDialog1.FileName.ToString();
                    }

                    if (PathLeft.Text.Length > 0 && PathRight.Text.Length > 0)
                    {
                        currentlayer.Enabled = true;
                        Tabs.Enabled = true;

                        window_max.Text = Math.Max(mLVolume.mMax, mRVolume.mMax).ToString();
                        window_min.Text = Math.Min(mLVolume.mMin, mRVolume.mMin).ToString();

                        rePaint();
                    }


                    openFileDialog1.Dispose();
                }
            }
        }

        private void currentlayer_Scroll(object sender, ScrollEventArgs e)
        {
            if ((radiobutton_x_y.Checked) && (!radioButton_x_z.Checked) && (!radioButton_y_z.Checked))
            {
                plane = 1;
            }
            else if ((!radiobutton_x_y.Checked) && (!radioButton_x_z.Checked) && (radioButton_y_z.Checked))
            {
                plane = 2;
            }
            else
            {
                plane = 3;
            }
            mLVolume.LoadTexture(currentlayer.Value, int.Parse(window_max.Text), int.Parse(window_min.Text), plane);
            glControl1.Refresh();
            mRVolume.LoadTexture(currentlayer.Value, int.Parse(window_max.Text), int.Parse(window_min.Text), plane);
            glControl2.Refresh();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            mRVolume.drawViaShaders();
            glControl1.MakeCurrent();
            glControl1.SwapBuffers();
        }

        private void glControl2_Paint(object sender, PaintEventArgs e)
        {
            mLVolume.drawViaShaders();
            glControl2.MakeCurrent();
            glControl2.SwapBuffers();
        }

        private void левыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openandloadmem("left");
        }

        private void правыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openandloadmem("right");
        }

        private void rbLeft_MouseClick(object sender, MouseEventArgs e)
        {
            rbRight.Checked = false;
            rbLeft.Checked = true;
        }

        private void rbRight_MouseClick(object sender, MouseEventArgs e)
        {
            rbLeft.Checked = false;
            rbRight.Checked = true;
        }

        private void bMin_Click(object sender, EventArgs e)
        {
            if (isCurrent.Checked)
            {
                int currentLayerIdx = mLVolume.clamp(currentlayer.Value, 0, mLVolume.mDepth);
                int layerLength = mLVolume.mWidth * mLVolume.mHeight;
                if (rbLeft.Checked)
                    MessageBox.Show(mLVolume.mData.Skip(currentLayerIdx * layerLength).Take(layerLength).Min().ToString(), "Min", MessageBoxButtons.OK);
                else
                    MessageBox.Show(mRVolume.mData.Skip(currentLayerIdx * layerLength).Take(layerLength).Min().ToString(), "Min", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(mLVolume.mMin.ToString(), "Max", MessageBoxButtons.OK);
            }
        }

        private void bMax_Click(object sender, EventArgs e)
        {
            if (isCurrent.Checked)
            {
                int currentLayerIdx = mLVolume.clamp(currentlayer.Value, 0, mLVolume.mDepth);
                int layerLength = mLVolume.mWidth * mLVolume.mHeight;
                if (rbLeft.Checked)
                    MessageBox.Show(mLVolume.mData.Skip(currentLayerIdx * layerLength).Take(layerLength).Max().ToString(), "Max", MessageBoxButtons.OK);
                else
                    MessageBox.Show(mRVolume.mData.Skip(currentLayerIdx * layerLength).Take(layerLength).Max().ToString(), "Max", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(mLVolume.mMax.ToString(), "Max", MessageBoxButtons.OK);
            }
        }

        private void MaskInfo_Click(object sender, EventArgs e)
        {
            if (masks.SelectedIndex > -1)
            {
                mLVolume.GetMaskinfo(currentlayer.Value, mControl.mBitmapList[masks.SelectedIndex], colorBox[cColor.SelectedIndex]);
            }
        }

        private void AddMask_Click(object sender, EventArgs e)
        {
            if (mControl.countLayers < 10)
            {
                mControl.mBitmapList[mControl.countLayers] =
                new Bitmap(mLVolume.mWidth, mLVolume.mHeight);
                mControl.maskManager[mControl.countLayers, 0] = mControl.countLayers;
                mControl.maskManager[mControl.countLayers, 1] = currentlayer.Value;
                masks.Items.Add("layer " + currentlayer.Value.ToString());
                mControl.countLayers++;
            }
        }

        private void voldp_Click(object sender, EventArgs e)
        {
            if (rbLeft.Checked)
                MessageBox.Show(mRVolume.GetVol(int.Parse(volMin.Text), int.Parse(volMax.Text), "left").ToString());
            else
                MessageBox.Show(mLVolume.GetVol(int.Parse(volMin.Text), int.Parse(volMax.Text), "right").ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (masks.SelectedIndex > -1)
            {
                MessageBox.Show(mRVolume.GetVolFm(mControl.mBitmapList[masks.SelectedIndex], colorBox[cColor.SelectedIndex]).ToString());
            }
        }

        private void LoadMask_Click(object sender, EventArgs e)
        {
            if (masks.SelectedIndex > -1)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog1.Filter = "images (*.png)|*.png|png files (*.png)|*.png";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(openFileDialog1.FileName.ToString()))
                    {
                        mLVolume.mBitmapList[masks.SelectedIndex] = new Bitmap(openFileDialog1.FileName.ToString());
                        mLVolume.ChangeMask(masks.SelectedIndex);
                        rePaint();
                    }
                }
            }
        }

        private void rPen_MouseClick(object sender, MouseEventArgs e)
        {
            rEraser.Checked = false;
            rWandPen.Checked = false;
            wpOffset.Enabled = false;
            rPen.Checked = true;
        }

        private void rEraser_MouseClick(object sender, MouseEventArgs e)
        {
            rEraser.Checked = true;
            rPen.Checked = false;
            wpOffset.Enabled = false;
            rWandPen.Checked = false;
        }
        private void rWandPen_MouseClick(object sender, MouseEventArgs e)
        {
            rWandPen.Checked = true;
            rPen.Checked = false;
            wpOffset.Enabled = true;
            rEraser.Checked = false;
        }

        private void glControl1_Click(object sender, EventArgs e)
        {
            if (masks.SelectedIndex > -1)
            {
                Point P = PointToScreen(new Point(glControl1.Bounds.Left, glControl1.Bounds.Top));
                this.Cursor = new Cursor(Cursor.Current.Handle);

                double Ox = (double)(mControl.mBitmapList[masks.SelectedIndex].Width / 350.0),
                       Oy = (double)(mControl.mBitmapList[masks.SelectedIndex].Height / 350.0),
                       x = (Cursor.Position.X - P.X) * Ox,
                       y = (Cursor.Position.Y - P.Y) * Oy;


                if (!rWandPen.Checked) // Если это не волшебная палочка, тогда
                {
                    switch (cForm.SelectedIndex) // Вероятно это кисть или ластик, определяем форму кисти
                    {
                        case 0:
                            {
                                for (int i = 0; i < cSize.SelectedIndex + 1; i++)
                                    for (int j = 0; j < cSize.SelectedIndex + 1; j++)
                                        if ((int)x - (cSize.SelectedIndex + 1) / 2 + i >= 0)
                                            if ((int)x - (cSize.SelectedIndex + 1) / 2 + i < mControl.mBitmapList[masks.SelectedIndex].Width)
                                                if ((int)y - (cSize.SelectedIndex + 1) / 2 + j >= 0)
                                                    if ((int)y - (cSize.SelectedIndex + 1) / 2 + j < mControl.mBitmapList[masks.SelectedIndex].Height)
                                                        if (rPen.Checked) // Если это все таки кисть, то рисуем выбранным цветом, если нет, то белым - ластик 
                                                            mControl.mBitmapList[masks.SelectedIndex].SetPixel((int)x - (cSize.SelectedIndex + 1) / 2 + i, (int)y - (cSize.SelectedIndex + 1) / 2 + j, colorBox[cColor.SelectedIndex]);
                                                        else
                                                            mControl.mBitmapList[masks.SelectedIndex].SetPixel((int)x - (cSize.SelectedIndex + 1) / 2 + i, (int)y - (cSize.SelectedIndex + 1) / 2 + j, Color.White);
                                break;
                            }
                        case 1:
                            {
                                for (int i = 0; i < cSize.SelectedIndex + 1; i++)
                                    for (int k = 0; k < 360; k++)
                                        if ((int)x + (int)(Math.Cos(k) * i) >= 0)
                                            if ((int)x + (int)(Math.Cos(k) * i) < mControl.mBitmapList[masks.SelectedIndex].Width)
                                                if ((int)y + (int)(Math.Sin(k) * i) > 0)
                                                    if ((int)y + (int)(Math.Sin(k) * i) < mControl.mBitmapList[masks.SelectedIndex].Height)
                                                        if (rPen.Checked)
                                                            mControl.mBitmapList[masks.SelectedIndex].SetPixel((int)x + (int)(Math.Cos(k) * i), (int)y + (int)(Math.Sin(k) * i), colorBox[cColor.SelectedIndex]);
                                                        else
                                                            mControl.mBitmapList[masks.SelectedIndex].SetPixel((int)x + (int)(Math.Cos(k) * i), (int)y + (int)(Math.Sin(k) * i), Color.White);
                                break;
                            }

                    }
                }
                else
                {
                    int z = mLVolume.mWidth * mLVolume.mHeight * currentlayer.Value,
                        offset = int.Parse(wpOffset.Text),
                        t = z + (int)y * mLVolume.mWidth + (int)x;

                    int h = mControl.mBitmapList[masks.SelectedIndex].Width,
                        k = mControl.mBitmapList[masks.SelectedIndex].Height;

                    for (int j = 0; j < mControl.mBitmapList[masks.SelectedIndex].Height; j++)
                        for (int i = 0; i < mControl.mBitmapList[masks.SelectedIndex].Width; i++)
                        {
                            if (mLVolume.mData[z + i + j * mControl.mBitmapList[masks.SelectedIndex].Width] <= mLVolume.mData[t] + offset)
                                if (mLVolume.mData[z + i + j * mControl.mBitmapList[masks.SelectedIndex].Width] >= mLVolume.mData[t] - offset)
                                {
                                    mControl.mBitmapList[masks.SelectedIndex].SetPixel(i, j, colorBox[cColor.SelectedIndex]);
                                }
                        }


                }
                mControl.mBitmapList[masks.SelectedIndex].MakeTransparent(Color.White);
                rePaint();
            }
        }
        public int filen;


        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Stream myStream;
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "txt files (*.txt)|*.txt|All files (*.*|*.*";
            savefile.FilterIndex = 1;
            savefile.RestoreDirectory = true;

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = savefile.OpenFile()) != null)
                {
                    myStream.Close();
                    System.IO.StreamWriter file = new System.IO.StreamWriter(savefile.FileName);
                    file.WriteLine("NAME : " + masks.SelectedItem);
                    file.WriteLine();
                    file.WriteLine("LAYERS'S COUNT: " + currentlayer.Maximum.ToString());
                    file.WriteLine();
                    file.WriteLine("VOLUME: " + mRVolume.GetVolFm(mControl.mBitmapList[masks.SelectedIndex], colorBox[cColor.SelectedIndex]).ToString());
                    string[] scol = new string[4];
                    double[] mincol = new double[4],
                             maxcol = new double[4],
                             avgcol = new double[4],
                             devcol = new double[4];

                    scol[0] = "RED";
                    scol[1] = "GREEN";
                    scol[2] = "BLUE";
                    scol[3] = "YELLOW";

                    for (int j = 0; j < 4; j++)
                    {
                        mincol[j] = 10000;
                        file.WriteLine(scol[j].ToString());
                        file.WriteLine();
                        file.WriteLine("TEMPLATE:");
                        file.WriteLine("LAYER N\t:\t|\tMIN\t|\tMAX\t|\tAVG\t|\tSTANDARTDEVIATION");
                        file.WriteLine();
                        int ii = 0;
                        for (int i = currentlayer.Minimum; i <= currentlayer.Maximum; i++)
                        {
                            double min = mLVolume.getMaskMin(i, mControl.mBitmapList[masks.SelectedIndex], colorBox[j]),
                                   max = mLVolume.getMaskMax(i, mControl.mBitmapList[masks.SelectedIndex], colorBox[j]),
                                   avg = mLVolume.getMaskAVG(i, mControl.mBitmapList[masks.SelectedIndex], colorBox[j]),
                                   deviation = mLVolume.getMaskStDeviation(i, mControl.mBitmapList[masks.SelectedIndex], colorBox[j]);

                            if (min < mincol[j])
                                mincol[j] = min;
                            if (max > maxcol[j])
                                maxcol[j] = max;
                            avgcol[j] += avg;

                            ii = i;
                            filen = (j * currentlayer.Maximum + i) / (4 * currentlayer.Maximum) * 100;
                            file.WriteLine(i.ToString() + "\t:\t|\t" + min.ToString() + "\t|\t" + max.ToString() + "\t|\t" + avg.ToString() + "\t|\t" + deviation.ToString());
                            var sync = SynchronizationContext.Current;
                            new Thread(_ =>
                            {
                                sync.Post(__ =>
                                    progressBar1.Value = filen, null);
                            }).Start();

                        }
                        avgcol[j] = avgcol[j] / ii;
                        file.WriteLine();
                        file.WriteLine("-------------------------------------------------------------------------------------------------------------");
                        file.WriteLine();
                        file.WriteLine("TEMPLATE OF RESULTS:");
                        file.WriteLine("COLOR\t:\t|\tMIN\t|\tMAX\t|\tAVG OF AVG");
                        file.WriteLine();
                        file.WriteLine(scol[j].ToString() + "\t:\t|\t" + mincol[j].ToString() + "\t|\t" + maxcol[j] + "\t|\t" + avgcol[j]);
                        file.WriteLine();
                        file.WriteLine("==================================================================");
                        file.WriteLine("==================================================================");
                        file.WriteLine("==================================================================");
                        file.WriteLine();
                    }
                    file.Close();
                }
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (masks.SelectedIndex > -1)
            {
                mLVolume.GetMaskinfo(currentlayer.Value, mControl.mBitmapList[masks.SelectedIndex], colorBox[cColor.SelectedIndex]);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Could not find executable", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void открытьСправкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В разработке... =)", "", MessageBoxButtons.OK);
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Версия: 1.0.0.0.0\nДата: 07.05.2017\nСоздатель: Николаев Денис\nСвязаться: auxlander.1000@gmail.com", "Информация", MessageBoxButtons.OK);
        }

        private void rotright_Click(object sender, EventArgs e)
        {
            if (rrleft.Checked)
            {
                mRVolume.rotation++;
                if (mRVolume.rotation == 4)
                    mRVolume.rotation = 0;
            }
            else
            {
                mLVolume.rotation++;
                if (mLVolume.rotation == 4)
                    mLVolume.rotation = 0;
            }
            rePaint();
        }

        private void rotleft_Click(object sender, EventArgs e)
        {
            if (rrleft.Checked)
            {
                mRVolume.rotation--;
                if (mRVolume.rotation == -1)
                    mRVolume.rotation = 3;
            }
            else
            {
                mLVolume.rotation--;
                if (mLVolume.rotation == -1)
                    mLVolume.rotation = 3;
            }
            rePaint();
        }

        private void rrleft_MouseClick(object sender, MouseEventArgs e)
        {
            rrleft.Checked = true;
            rrright.Checked = false;
        }

        private void rrright_MouseClick(object sender, MouseEventArgs e)
        {
            rrleft.Checked = false;
            rrright.Checked = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void dИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 tempDialog = new Form2();
            tempDialog.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mLVolume.MirroringMap();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            mLVolume.MirroringMap();
        }

        private void radioButton_knife_MouseClick(object sender, MouseEventArgs e)
        {
            rEraser.Checked = false;
            rWandPen.Checked = false;
            wpOffset.Enabled = false;
            rPen.Checked = false;
            radioButton_knife.Checked = true;
        }
        public List<Point> Points = new List<Point>();
        public Pen mypen = new Pen(Color.Green, 1);
        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            int count = 0;
            Point Ps = new Point();
            Point Pf = new Point();

            if ((radioButton_knife.Checked) && (masks.SelectedIndex > -1))
            {
                Point P = PointToScreen(new Point(glControl1.Bounds.Left, glControl1.Bounds.Top));
                double Ox = (double)(mControl.mBitmapList[masks.SelectedIndex].Width / 350.0),
                       Oy = (double)(mControl.mBitmapList[masks.SelectedIndex].Height / 350.0);


                if (e.Button == MouseButtons.Left)
                {

                    Pen mypen_ = new Pen(Color.Blue, 1);
                    Brush fillBrush = new SolidBrush(Color.Blue);
                    Graphics g2 = Graphics.FromImage(mControl.mBitmapList[masks.SelectedIndex]);
                    g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    int size = 1;
                    Point startPoint = new Point();
                    startPoint.X = Convert.ToInt32((Cursor.Position.X - P.X) * Ox);
                    startPoint.Y = Convert.ToInt32((Cursor.Position.Y - P.Y) * Oy);

                    Points.Add(startPoint);
                    Rectangle rectangle = new Rectangle(startPoint.X - (size / 2), startPoint.Y - (size / 2), size, size);
                    if (count == 0) { Ps = startPoint; }
                    count++;
                    g2.FillEllipse(fillBrush, rectangle);
                    g2.DrawEllipse(mypen_, rectangle);
                    g2.Dispose();
                    rePaint();
                    Pf = startPoint;
                }
                else if (Points.Count > 2)
                {
                    Graphics g2 = Graphics.FromImage(mControl.mBitmapList[masks.SelectedIndex]);
                    g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g2.DrawClosedCurve(mypen, Points.ToArray());

                    g2.Dispose();
                    rePaint();


                }
            }

        }
        public List<Point> Contur_green = new List<Point>();
        public void CalculateContur(Color colorPen)
        {
            if (masks.SelectedIndex > -1)
            {
                Bitmap mp = new Bitmap(mControl.mBitmapList[masks.SelectedIndex]);
                //mp =  mControl.mBitmapList[masks.SelectedIndex];
                for (int x = 0; x < mp.Width; x++)
                    for (int y = 0; y < mp.Height; y++)
                    {
                        if (mp.GetPixel(x, y).G > mp.GetPixel(x, y).R)
                        {
                            Contur_green.Add(new Point(x, y));
                        }
                    }
            }
        }
        private void button_to3D_Click(object sender, EventArgs e)
        {
            if (!radioButton_knife.Checked) { }
            CalculateContur(mypen.Color);
            int Size_contur = Contur_green.Count;//Points.Count;
            string filename = PathLeft.Text;
            Form3 tempDialog = new Form3(Contur_green, filename, glControl1.Width, glControl1.Height);
            tempDialog.ShowDialog();
        }

        private void button_repaint_Click(object sender, EventArgs e)
        {
            if ((radiobutton_x_y.Checked) && (!radioButton_x_z.Checked) && (!radioButton_y_z.Checked))
            {
                plane = 1;
            }
            else if ((!radiobutton_x_y.Checked) && (!radioButton_x_z.Checked) && (radioButton_y_z.Checked))
            {
                plane = 2;
            }
            else
            {
                plane = 3;
            }
            rePaint();
        }

        private void button_to3d_plane_Click(object sender, EventArgs e)
        {
            if (!radioButton_knife.Checked) { }
            //CalculateContur(mypen.Color);
            string filename = PathLeft.Text;
            Form3 tempDialog = new Form3(Points, filename, glControl1.Width, glControl1.Height);
            tempDialog.ShowDialog();
        }

        private void masks_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            mControl.ChangeMask(masks.SelectedIndex);
            rePaint();
        }
    }
}
