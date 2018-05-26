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
        //  private string FileName;
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
    }
}
