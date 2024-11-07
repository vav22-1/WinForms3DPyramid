namespace WinForms3DPyramid
{
    public class Point3D
    {
        private float X;
        private float Y;
        private float Z;
        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point3D ScalePoint(float scale)
        {
            return new Point3D(X * scale, Y * scale, Z * scale);
        }
        public float GetX() => X;
        public float GetY() => Y;
        public float GetZ() => Z;
    }
}
