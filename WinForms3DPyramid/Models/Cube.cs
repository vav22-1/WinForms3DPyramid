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

        public override List<Point3D> GetPoints() => points3D;

        public override int[] GetVerticesConnectionIndices() => new int[] {0,1,2,3,0};
    }
}
