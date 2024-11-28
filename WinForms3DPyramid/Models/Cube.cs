using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms3DPyramid
{
    public class Cube : Shape
    {
        //Поле, хранящее массив точек вершин куба
        private List<Point3D> points3D;

        public Cube(float scale = 1f)
        {
            var basePoints3D = new List<Point3D>
            {
                //Вершины куба
                new Point3D(-100, 100, 100),
                new Point3D(100, 100, 100),
                new Point3D(100, 100, -100),
                new Point3D(-100, 100, -100),
                new Point3D(-100, -100, 100),
                new Point3D(100, -100, 100),
                new Point3D(100, -100, -100),
                new Point3D(-100, -100, -100),
            };
            points3D = new List<Point3D>();
            //Скалирования размера куба
            foreach (var point in basePoints3D)
            {
                points3D.Add(point.ScalePoint(scale));
            }
        }

        public override List<int[]> GetFaces()
        {
            return new List<int[]>
    {
        new int[] { 0, 1, 2, 3 },
        new int[] { 4, 5, 6, 7 },
        new int[] { 0, 1, 5, 4 },
        new int[] { 1, 2, 6, 5 },
        new int[] { 2, 3, 7, 6 },
        new int[] { 3, 0, 4, 7 }
    };
        }


        public override List<Point3D> GetPoints() => points3D;

        public override int[] GetVerticesConnectionIndices() => new int[] {0,1,2,3,0};
    }
}
