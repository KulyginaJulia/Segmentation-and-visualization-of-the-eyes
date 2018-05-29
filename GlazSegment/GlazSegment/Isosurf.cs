using OpenTK;

namespace GlazSegment
{
    public class Isosurf
    {
        public float iso_value;
        public Vector4 Color;
        public Isosurf()
        {

        }
        public Isosurf(float _isovalue, Vector4 _Color)
        {
            iso_value = _isovalue;
            Color = _Color;
        }
public void Clear() {
            iso_value = 0f;
            Color = new Vector4(0f, 0f, 0f, 0f);
        }
public bool isEmpty() {
            Vector4 tmp = new Vector4(0f, 0f, 0f, 0f);
            if (Color == tmp) return true;
            return false;
        }

    }
}
