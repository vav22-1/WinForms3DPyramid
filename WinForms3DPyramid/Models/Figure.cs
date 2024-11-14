using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms3DPyramid
{
    public class Figure
    {
        private List<Shape> shapes = new List<Shape>();
        private Type shapeType;
        public Figure(ShapeFactory factory, int figureCount = 2, float scale = 1f)
        {
            shapeType = factory.CreateShape(1f).GetType();
            for (int i = 1; i <= figureCount; i++)
            {
                shapes.Add(factory.CreateShape(scale / i));
            }
        }

        public List<Shape> GetShapes()
        {
            return shapes;
        }
        public Type GetShapeType()
        {
            return shapeType;
        }
    }
}
