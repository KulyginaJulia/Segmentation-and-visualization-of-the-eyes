using OpenTK;
using System.Collections.Generic;

namespace GlazSegment
{
    public class Isosurfaces : Isosurf
    {
        public List<Vector4> IsoColor;
        public List<float> isoValue;
        public Isosurfaces()
        {
            IsoColor = new List<Vector4>();
            isoValue = new List<float>();
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
                return_tmp[i] = (float)tmp[j].X;
                return_tmp[i + 1] = (float)tmp[j].Y;
                return_tmp[i + 2] = (float)tmp[j].Z;
                return_tmp[i + 3] = (float)tmp[j].W;
                i += 4;
                j++;
            }
            return return_tmp;
        }
        public float[] GetArrayIsovalueIsosurf()
        {
            return isoValue.ToArray();
        }

    }
}
