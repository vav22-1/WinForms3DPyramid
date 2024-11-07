using System.Collections.Generic;

namespace WinForms3DPyramid
{
    public class Figure
    {
        private List<Shape> shapes = new List<Shape>();

        public Figure(ShapeFactory factory, int figureCount = 2, float scale = 1f)
        {
            for (int i = 1; i <= figureCount; i++)
            {
                shapes.Add(factory.CreateShape(scale / i));
            }
        }

        public List<Shape> GetShapes()
        {
            return shapes;
        }
    }
}
