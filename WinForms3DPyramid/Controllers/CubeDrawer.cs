using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WinForms3DPyramid
{
    public class CubeDrawer : IShapeDrawer
    {
        //Константа, определяющая "дистанцию" до фигуры
        private const float ProjectDistance = 600f;
        //Поле, хранящее размер области для рисования
        private Size drawPanelSize;
        private Size baseDrawPanelSize;
        private float widthChangeeFactor;
        private float heightChangeeFactor;

        public void SetBaseClientSize(Size size)
        {
            baseDrawPanelSize = size;
        }
        public void SetClientSize(Size size)
        {
            drawPanelSize = size;
            widthChangeeFactor = (float)drawPanelSize.Width / baseDrawPanelSize.Width;
            heightChangeeFactor = (float)drawPanelSize.Height / baseDrawPanelSize.Height;
        }

        //Метод для преобразования 3D координат в 2D координаты на плоскости с учетом перспективы
        public PointF Get2DCoords(Point3D point)
        {
            //Учет перспективного сокращения
            float projectFactor = ProjectDistance / (ProjectDistance + point.GetZ());
            //Преобразование координат с учетом проекции и изменения размера формы
            float projectedX = point.GetX() * projectFactor * widthChangeeFactor;
            float projectedY = -point.GetY() * projectFactor * heightChangeeFactor;

            return new PointF(projectedX, projectedY);
        }

        //Метод, создающий массив 2D координат вершин пирамиды с помощью метода Get2DCoords
        //Точки устанавливаются относительно центра рисуемой области 
        public PointF[] GetVertices(Shape cube)
        {
            List<Point3D> points = cube.GetPoints();
            PointF[] vertices = new PointF[points.Count];
            float centerX = drawPanelSize.Width / 2f;
            float centerY = drawPanelSize.Height / 2f;

            for (int i = 0; i < points.Count; i++)
            {
                vertices[i] = Get2DCoords(points[i]);
                vertices[i].X += centerX;
                vertices[i].Y += centerY;
            }
            return vertices;
        }

        //Метод, рисующий линии между всеми вершинами куба
        public void DrawShape(Graphics g, Shape cube, Pen color)
        {
            int[] verticesIndices = cube.GetVerticesConnectionIndices();
            PointF[] vertices = GetVertices(cube);
            for (int i = 0; i < verticesIndices.Length - 1; i++)
            {
                g.DrawLine(color, vertices[verticesIndices[i]], vertices[verticesIndices[i + 1]]);
                g.DrawLine(color, vertices[verticesIndices[i]+4], vertices[verticesIndices[i + 1]+4]);
            }
            for (int i = 4; i < vertices.Length; i++)
            {
                g.DrawLine(color, vertices[i-4], vertices[i]);
            }
        }

        //Метод для соединения соответствующих вершин внутреннего и внешнего куба
        public void ConnectVertices(Graphics g, Shape cubeOne, Shape cubeTwo, Pen color)
        {
            PointF[] verticesOne = GetVertices(cubeOne);
            PointF[] verticesTwo = GetVertices(cubeTwo);
            for (int i = 0; i < verticesOne.Length; i++)
            {
                g.DrawLine(color, verticesOne[i], verticesTwo[i]);
            }
        }


        //Public метод для рисования двух кубов с соединением соответствующих вершин
        public void DrawFigure(Graphics g, Figure figure)
        {
            foreach (Cube cube in figure.GetShapes().OfType<Cube>())
            {
                DrawShape(g, cube, Pens.Black);
            }
            List<Cube> pyramids = figure.GetShapes().OfType<Cube>().ToList(); ;
            for (int i = 1; i < pyramids.Count; i++)
            {
                ConnectVertices(g, pyramids[i - 1], pyramids[i], Pens.BlueViolet);
            }
        }
    }
}
