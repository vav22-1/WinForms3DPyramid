using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms3DPyramid
{
    internal class Pyramid
    {
        private PointF[] vertices;
        private float[][] points3D;
        private float distance;

        public Pyramid(float distance)
        {
            this.distance = distance;
            points3D = new float[5][] {
                new float[] { 0, 100, 100 },
                new float[] { -100, -100, 100 },
                new float[] { 100, -100, 100 },
                new float[] { 100, -100, -100 },
                new float[] { -100, -100, -100 }
            };

            vertices = new PointF[5];
        }

        private PointF Get2DCoords(float x, float y, float z)
        {
            float factor = distance / (distance - z);
            return new PointF(x * factor, -y * factor);
        }

        public void Draw(Graphics g, Size clientSize)
        {
            for (int i = 0; i < points3D.Length; i++)
            {
                vertices[i] = Get2DCoords(points3D[i][0], points3D[i][1], points3D[i][2]);
                vertices[i].X += clientSize.Width / 2;
                vertices[i].Y += clientSize.Height / 2;
            }

            g.DrawLine(Pens.Black, vertices[0], vertices[1]);
            g.DrawLine(Pens.Black, vertices[0], vertices[2]);
            g.DrawLine(Pens.Black, vertices[0], vertices[3]);
            g.DrawLine(Pens.Black, vertices[0], vertices[4]);
            g.DrawLine(Pens.Black, vertices[1], vertices[2]);
            g.DrawLine(Pens.Black, vertices[2], vertices[3]);
            g.DrawLine(Pens.Black, vertices[3], vertices[4]);
            g.DrawLine(Pens.Black, vertices[4], vertices[1]);
        }
    }
}
