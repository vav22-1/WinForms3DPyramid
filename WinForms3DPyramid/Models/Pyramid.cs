using System.Collections.Generic;
using System.Drawing;

namespace WinForms3DPyramid
{
    public class Pyramid : Shape
    {
        //Поле, хранящее массив точек вершин пирамиды
        private List<Point3D> points3D;

        public Pyramid(float scale = 1f)
        {
            var basePoints3D = new List<Point3D>
            {
                //Общая вершина пирамиды
                new Point3D(0, 100, 0),  
                //Вершины основания пирамиды
                new Point3D(-100, -100, 100),
                new Point3D(100, -100, 100),
                new Point3D(100, -100, -100),
                new Point3D(-100, -100, -100),
            };
            points3D = new List<Point3D>();


            //Скалирования размера пирамиды
            foreach (var point in basePoints3D)
            {
                points3D.Add(point.ScalePoint(scale));
            }
        }

        public override List<Point3D> GetPoints() => points3D;

        public override int[] GetVerticesConnectionIndices() => new int[] { 1, 2, 3, 4, 1 };
        public override List<int[]> GetFaces()
        {
            return new List<int[]>
        {
            new int[] { 0, 1, 2 },
            new int[] { 0, 2, 3 },
            new int[] { 0, 3, 4 },
            new int[] { 0, 4, 1 },
            new int[] { 1, 2, 3, 4 }
        };
        }
    }
}
