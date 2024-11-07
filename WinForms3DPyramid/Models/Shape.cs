using System.Collections.Generic;

namespace WinForms3DPyramid
{
    public abstract class Shape
    {
        public abstract List<Point3D> GetPoints();
        public abstract int[] GetVerticesConnectionIndices();
    }
}
