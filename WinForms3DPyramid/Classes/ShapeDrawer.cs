using System;
using System.Drawing;
using System.Threading.Tasks;

namespace WinForms3DPyramid
{
    public static class ShapeDrawer
    {
        //Константа, определяющая "дистанцию" до фигуры
        private const float ProjectDistance = 600f;
        //Поле, хранящее размер области для рисования
        private static Size drawPanelSize;
        private static Size baseDrawPanelSize;
        public static void SetBaseClientSize(Size size)
        {
            baseDrawPanelSize = size;
        }
        public static void SetClientSize(Size size)
        {
            drawPanelSize = size;
        }

        //Метод для преобразования 3D координат в 2D координаты на плоскости с учетом перспективы
        public static PointF Get2DCoords(float x, float y, float z)
        {
            //Учет перспективного сокращения
            float projectFactor = ProjectDistance / (ProjectDistance + z);
            float widthChangeeFactor = (float)drawPanelSize.Width / baseDrawPanelSize.Width;
            float heightChangeeFactor = (float)drawPanelSize.Height / baseDrawPanelSize.Height;
            //Преобразование координат с учетом проекции и изменения размера формы
            float projectedX = x * projectFactor * widthChangeeFactor;
            float projectedY = -y * projectFactor * heightChangeeFactor;

            return new PointF(projectedX, projectedY);
        }

        //Метод, создающий массив 2D координат вершин пирамиды с помощью метода Get2DCoords
        //Точки устанавливаются относительно центра рисуемой области 
        private static PointF[] GetVertices(Pyramid pyramid)
        {
            PointF[] vertices = new PointF[5];
            float[][] points = pyramid.GetPoints();
            float centerX = drawPanelSize.Width / 2f;
            float centerY = drawPanelSize.Height / 2f;

            for (int i = 0; i < points.Length; i++)
            {
                vertices[i] = Get2DCoords(points[i][0], points[i][1], points[i][2]);
                vertices[i].X += centerX;
                vertices[i].Y += centerY;
            }
            return vertices;
        }

        //Метод, рисующий линии между всеми вершинами пирамиды 
        private static void DrawPyramid(Graphics g, Pyramid pyramid, Pen color)
        {
            PointF[] vertices = GetVertices(pyramid);
            for (int i = 1; i < vertices.Length; i++)
            {
                g.DrawLine(color, vertices[0], vertices[i]);
            }
            g.DrawLine(color, vertices[1], vertices[2]);
            g.DrawLine(color, vertices[2], vertices[3]);
            g.DrawLine(color, vertices[3], vertices[4]);
            g.DrawLine(color, vertices[4], vertices[1]);
        }

        //Метод для соединения соответствующих вершин внутренней и внешней пирамиды
        private static void ConnectVertices(Graphics g, Pyramid pyramidOne, Pyramid pyramidTwo, Pen color)
        {
            PointF[] verticesPyramidOne = GetVertices(pyramidOne);
            PointF[] verticesPyramidTwo = GetVertices(pyramidTwo);
            for (int i = 0; i < 5; i++)
            {
                g.DrawLine(color, verticesPyramidOne[i], verticesPyramidTwo[i]);
            }
        }

        //Public метод для рисования двух пирамид с соединением соответствующих вершин
        public static void DrawPyramids(Graphics g, Pyramid pyramidOne, Pyramid pyramidTwo)
        {
            DrawPyramid(g, pyramidOne, Pens.Black);
            DrawPyramid(g, pyramidTwo, Pens.Red);
            ConnectVertices(g, pyramidOne, pyramidTwo, Pens.BlueViolet);
        }

        //Метод поворота пирамиды по оси X с помощью матрицы поворота
        private static void RotatePyramidX(Pyramid pyramid, float angle)
        {
            float[][] points = pyramid.GetPoints();
            float radians = angle * (float)Math.PI / 180f;
            float cosTheta = (float)Math.Cos(radians);
            float sinTheta = (float)Math.Sin(radians);

            for (int i = 0; i < points.Length; i++)
            {
                float y = points[i][1];
                float z = points[i][2];

                points[i][1] = y * cosTheta - z * sinTheta;
                points[i][2] = y * sinTheta + z * cosTheta;
            }
        }

        //Метод поворота пирамиды по оси Y с помощью матрицы поворота
        private static void RotatePyramidY(Pyramid pyramid, float angle)
        {
            float[][] points = pyramid.GetPoints();
            float radians = angle * (float)Math.PI / 180f;
            float cosTheta = (float)Math.Cos(radians);
            float sinTheta = (float)Math.Sin(radians);

            for (int i = 0; i < points.Length; i++)
            {
                float x = points[i][0];
                float z = points[i][2];

                points[i][0] = x * cosTheta + z * sinTheta;
                points[i][2] = -x * sinTheta + z * cosTheta;
            }
        }

        //Метод поворота пирамиды по оси Z с помощью матрицы поворота
        private static void RotatePyramidZ(Pyramid pyramid, float angle)
        {
            float[][] points = pyramid.GetPoints();
            float radians = angle * (float)Math.PI / 180f;
            float cosTheta = (float)Math.Cos(radians);
            float sinTheta = (float)Math.Sin(radians);

            for (int i = 0; i < points.Length; i++)
            {
                float x = points[i][0];
                float y = points[i][1];

                points[i][0] = x * cosTheta - y * sinTheta;
                points[i][1] = x * sinTheta + y * cosTheta;
            }
        }

        //Асинхронный метод для поворота всей фигуры по осям
        public async static Task RotateFigure(Pyramid pyramidOne, Pyramid pyramidTwo, string axis, bool rotateFactor)
        {
            await Task.Run(() =>
            {
                switch (axis)
                {
                    case "X":
                        RotatePyramidX(pyramidOne, rotateFactor ? 1f : -1f);
                        RotatePyramidX(pyramidTwo, rotateFactor ? 1f : -1f);
                        break;
                    case "Y":
                        RotatePyramidY(pyramidOne, rotateFactor ? 1f : -1f);
                        RotatePyramidY(pyramidTwo, rotateFactor ? 1f : -1f);
                        break;
                    case "Z":
                        RotatePyramidZ(pyramidOne, rotateFactor ? 1f : -1f);
                        RotatePyramidZ(pyramidTwo, rotateFactor ? 1f : -1f);
                        break;
                }
            });
        }
    }
}
