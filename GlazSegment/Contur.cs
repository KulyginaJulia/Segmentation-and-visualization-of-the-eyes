using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlazSegment
{
    public class Contur
    {
        public List<Point> Points;
        private List<Point> PointsOfContur;
        private int HeightControl;
        private int WidthControl;
        private string fileName;
        private float[] ConturForShader;

        public Contur(int HControl, int WControl, string filename)
        {
            Points = new List<Point>();
            PointsOfContur = new List<Point>();
            SetHeight(HControl);
            SetWidth(WControl);
            SetFilename(filename);
        }
        public Contur(Contur cnt)
        {
            Points = new List<Point>(cnt.Points);
            PointsOfContur = new List<Point>(cnt.GetPointsOfContur());
            SetHeight(cnt.GetHeight());
            SetWidth(cnt.GetWidth());
            SetFilename(cnt.GetFilename());
        }
        public void SetHeight(int Height)
        {
            this.HeightControl = Height;
        }
        public void SetWidth(int Width)
        {
            this.WidthControl = Width;
        }
        public int GetHeight()
        {
            return this.HeightControl;
        }
        public int GetWidth()
        {
            return this.WidthControl;
        }
        public void SetFilename(string filename)
        {
            this.fileName = filename;
        }
        public string GetFilename()
        {
            return this.fileName;
        }
        public List<Point> GetPointsOfContur() {
            return this.PointsOfContur;
        }
        public void CalculateContur(Bitmap tmp)
        {
            Bitmap mp = new Bitmap(tmp);
            for (int x = 0; x < mp.Width; x++)
                for (int y = 0; y < mp.Height; y++)
                {
                    if (mp.GetPixel(x, y).G > mp.GetPixel(x, y).R)
                    {
                        PointsOfContur.Add(new Point(x, y));
                    }
                }
        }

        public void ConturToArray(int dataWidth, int dataHeight, int dataDepth)
        {
            int length = dataWidth * dataHeight * dataDepth;

            ConturForShader = new float[length];
            float[,,] Contur_middle = new float[dataWidth, dataHeight, dataDepth];
            for (int i = 0; i < dataWidth; i++)
                for (int j = 0; j < dataHeight; j++)
                    for (int k = 0; k < dataDepth; k++)
                        Contur_middle[i, j, k] = 0;
            for (int i = 0; i < length; i++)
            {
                ConturForShader[i] = 0;
            }
            foreach (Point p in PointsOfContur)
            {
                int X_new = p.X * dataWidth / GetWidth();
                int Y_new = p.Y * dataHeight / GetHeight();
                for (int k = 0; k < dataDepth; k++)
                {
                    Contur_middle[X_new, Y_new, k] = 1;
                }
            }
            // Заполняем внутренность
            int cou = 0;
            for (int z = 0; z < dataDepth; z++)
                for (int j = 0; j < dataHeight; j++)
                    for (int i = 0; i < dataWidth; i++)
                    {
                        if (Contur_middle[i, j, z] != 0)
                        {
                            if (Contur_middle[i + 1, j, z] != 0)
                            {
                                i += 2;
                            }
                            else i++;
                            cou = i;
                            while ((i < dataWidth))
                            {
                                if ((Contur_middle[i, j, z] == 0))
                                    i++;
                                else break;
                            }
                            if (i == dataWidth)
                            {
                                continue;
                            }
                            else
                            {
                                for (int k = cou; k < i; k++)
                                {
                                    Contur_middle[k, j, z] = 1;
                                }
                            }
                        }
                    }
            // 3d -> 1d
            for (int k = 0; k < dataDepth; k++)
                for (int j = 0; j < dataHeight; j++)
                    for (int i = 0; i < dataWidth; i++)
                    {
                        ConturForShader[i + j * dataWidth + k * dataWidth * dataHeight] = Contur_middle[i, j, k];

                    }
        }
 public float[] GetConturForShader() {
            return this.ConturForShader;
        }
    }
}
