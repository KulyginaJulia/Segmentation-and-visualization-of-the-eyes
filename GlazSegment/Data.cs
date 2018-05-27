using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlazSegment
{
    public class Data
    {
        private float[] data;
        private int DataHeight;
        private int DataWidth;
        private int DataDepth;
        private int DataLength;
        private float VoxelHeight;
        private float VoxelWidth;
        private float VoxelDepth;
        private float maxDensity;
        private float minDensity;
        private float[] array_differenceData;
        private int[] frequency;
        private int indexDifference;
        public double MathWait;
        public double Dispers;
        public double G;
        private int norma;
public Data() { }
        public Data(string filename)
        {
            if (File.Exists(filename))
            {
                BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open));

                DataWidth = reader.ReadInt32();
                DataHeight = reader.ReadInt32();
                DataDepth = reader.ReadInt32();

                VoxelWidth = reader.ReadSingle();
                VoxelHeight = reader.ReadSingle();
                VoxelDepth = reader.ReadSingle();

                DataLength = DataWidth * DataHeight * DataDepth;
                data = new float[DataLength];
                for (int i = 0; i < DataLength; ++i)
                {
                    data[i] = (float)Convert.ChangeType(reader.ReadInt16(), typeof(float));
                }
                maxDensity = data.Max();
                minDensity = data.Min();
                reader.Close();
            }
            else
                Console.WriteLine("File was not found");
        }
        public Data(Data dt)
        {
            data = dt.GetData();
            DataHeight = dt.GetHeight();
            DataWidth = dt.GetWidth();
            DataDepth = dt.GetDepth();
            DataLength = dt.DataLength;
            VoxelHeight = dt.VoxelHeight;
            VoxelWidth = dt.VoxelWidth;
            VoxelDepth = dt.VoxelDepth;
            maxDensity = dt.maxDensity;
            minDensity = dt.minDensity;
        }
        public void SetHeight(int Height)
        {
            this.DataHeight = Height;
        }
        public void SetWidth(int Width)
        {
            this.DataWidth = Width;
        }
        public void SetDepth(int Depth)
        {
            this.DataDepth = Depth;
        }
        public int GetHeight()
        {
            return this.DataHeight;
        }
        public float[] GetData()
        {
            return this.data;
        }
        public int GetWidth()
        {
            return this.DataWidth;
        }
        public int GetDepth()
        {
            return this.DataDepth;
        }
        public int GetLength()
        {
            return this.DataLength;
        }
        public float GetVoxelHeight()
        {
            return this.VoxelHeight;
        }
        public float GetVoxelWidth()
        {
            return this.VoxelWidth;
        }
        public float GetVoxelDepth()
        {
            return this.VoxelDepth;
        }
        public List<Point_v> calculate_frequency(List<Point_v> fi)
        {
            int i = 0, f = 1;
            float[] arraySortData = new float[GetLength()];
            int length = GetLength();
            arraySortData = this.data;
            Array.Sort(arraySortData);

            if (length > 1)
            {
                for (i = 0; i < length - 1; i++)
                {
                    if (data[i] != data[i + 1])
                        f++;
                }
                if (data[length - 2] != data[length - 1])
                    f++;

                float[] data_result1 = new float[f];
                int[] data_result2 = new int[f];
                f = -1; int idx;
                for (int j = 0; j < length - 1; j++)
                {
                    f++;
                    idx = 1;
                    while ((j < length - 1) && (data[j] == data[j + 1]))
                    {
                        idx++;
                        j++;
                    }
                    data_result1[f] = data[j];
                    data_result2[f] = idx;
                }
                array_differenceData = new float[f];
                frequency = new int[f];
                indexDifference = f;

                for (int j = 0; j < f; j++)
                {
                    fi.Add(new Point_v(data_result1[j], data_result2[j]));
                    array_differenceData[j] = data_result1[j];
                    frequency[j] = data_result2[j];
                }

            }
            return fi;
        }

        public void CalculateStatData()
        {
            MathWait = 0.0;
            for (int j = 0; j < indexDifference; j++)
                norma += frequency[j];
            for (int i = 0; i < indexDifference; i++)
                MathWait += array_differenceData[i] * frequency[i] / (double)norma; // Нормальная частота от 0 до 1 встречаемости, поэтому и делим
            Dispers = 0;
            for (int i = 0; i < indexDifference; i++)
                Dispers += (frequency[i] / (double)norma) * Math.Pow((array_differenceData[i] - MathWait), 2);
            Dispers -= MathWait;
            G = Math.Pow(Dispers, 0.5);
        }
    }
}
