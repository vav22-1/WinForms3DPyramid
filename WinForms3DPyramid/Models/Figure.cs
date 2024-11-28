using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms3DPyramid
{
    public class Figure
    {
        private List<Shape> shapes = new List<Shape>();
        private Type shapeType;
        private FigureColor figureColor;
        public Figure(ShapeFactory factory, int figureCount = 2, float scale = 1f)
        {
            figureColor = new FigureColor(Color.Black, Color.Black, Color.Black, Color.FromArgb(0, 255, 255, 255));
            shapeType = factory.CreateShape(1f).GetType();
            for (int i = 1; i <= figureCount; i++)
            {
                shapes.Add(factory.CreateShape(scale / i));
            }
        }

        public List<Shape> GetShapes() => shapes;
        public Type GetShapeType() => shapeType;
        public FigureColor GetFigureColor() => figureColor;
    }
}
