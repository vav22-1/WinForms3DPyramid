using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms3DPyramid.Controllers
{
    public class PyramidDrawer : IShapeDrawer
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
        public PointF[] GetVertices(Shape pyramid)
        {
            List<Point3D> points = pyramid.GetPoints();
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

        //Метод, рисующий линии между всеми вершинами пирамиды 
        public void DrawShape(Graphics g, Shape pyramid, Pen color, Brush verticesColor)
        {
            float pointSize = 5f;
            PointF[] vertices = GetVertices(pyramid);
            for (int i = 1; i < vertices.Length; i++)
            {
                g.DrawLine(color, vertices[0], vertices[i]);
            }
            int[] verticesIndices = pyramid.GetVerticesConnectionIndices();
            for (int i = 0; i < verticesIndices.Length - 1; i++)
            {
                g.DrawLine(color, vertices[verticesIndices[i]], vertices[verticesIndices[i + 1]]);
            }
            foreach (var vertex in vertices)
            {
                g.FillEllipse(verticesColor, vertex.X - pointSize / 2, vertex.Y - pointSize / 2, pointSize, pointSize);
            }
        }

        //Метод для соединения соответствующих вершин внутренней и внешней пирамиды
        public void ConnectVertices(Graphics g, Shape pyramidOne, Shape pyramidTwo, Pen color)
        {
            PointF[] verticesOne = GetVertices(pyramidOne);
            PointF[] verticesTwo = GetVertices(pyramidTwo);
            for (int i = 0; i < verticesOne.Length; i++)
            {
                g.DrawLine(color, verticesOne[i], verticesTwo[i]);
            }
        }
        
        //Рисование граней
        public void DrawFaces(Graphics g, Shape pyramid, Brush facesBrush)
        {
            List<int[]> faces = pyramid.GetFaces();
            PointF[] vertices = GetVertices(pyramid);

            foreach (var face in faces)
            {
                PointF[] faceVertices = new PointF[face.Length];
                for (int i = 0; i < face.Length; i++)
                {
                    faceVertices[i] = vertices[face[i]];
                }
                g.FillPolygon(facesBrush, faceVertices);
            }
        }


        //Public метод для рисования двух пирамид с соединением соответствующих вершин
        public void DrawFigure(Graphics g, Figure figure)
        {
            foreach (Pyramid pyramid in figure.GetShapes().OfType<Pyramid>())
            {
                DrawShape(g, pyramid, new Pen(figure.GetFigureColor().GetLinesColor()), new SolidBrush(figure.GetFigureColor().GetVerticesColor()));
                DrawFaces(g, pyramid, new SolidBrush(figure.GetFigureColor().GetFacesColor()));
            }

            List<Pyramid> pyramids = figure.GetShapes().OfType<Pyramid>().ToList();
            for (int i = 1; i < pyramids.Count; i++)
            {
                ConnectVertices(g, pyramids[i - 1], pyramids[i], new Pen(figure.GetFigureColor().GetConnectionsColor()));
            }

        }
    }
}
