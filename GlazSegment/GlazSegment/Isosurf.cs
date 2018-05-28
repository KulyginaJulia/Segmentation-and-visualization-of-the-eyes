using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlazSegment
{
    public class Isosurf
    {
        public int iso_value;
        public Vector4 Color;
        public Isosurf()
        {

        }
        public Isosurf(int _isovalue, Vector4 _Color)
        {
            iso_value = _isovalue;
            Color = _Color;
        }

    }
}
