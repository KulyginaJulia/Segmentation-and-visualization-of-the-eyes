using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlazSegment
{
    public class Isosurfaces : Isosurf
    {
        public List<Vector4> IsoColor;
        public List<int> isoValue;
        public Isosurfaces()
        {
            IsoColor = new List<Vector4>();
            isoValue = new List<int>();
        }
        public void Add(Isosurf issi)
        {
            IsoColor.Add(issi.Color);
            isoValue.Add(issi.iso_value);
        }
        public float[] GetArrayColorIsosurf()
        {
            Vector4[] tmp = IsoColor.ToArray();
            float[] return_tmp = new float[4 * tmp.Length];
            int i = 0;
            int j = 0;
            while ((i < 4 * tmp.Length) && (j < tmp.Length))
            {
                return_tmp[i] = tmp[j].X;
                return_tmp[i + 1] = tmp[j].X;
                return_tmp[i + 2] = tmp[j].X;
                return_tmp[i + 3] = tmp[j].X;
                i += 4;
                j++;
            }
            return return_tmp;
        }
        public int[] GetArrayIsovalueIsosurf()
        {
            return isoValue.ToArray();
        }

    }
}
