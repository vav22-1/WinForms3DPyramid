﻿namespace WinForms3DPyramid
{
    public class PyramidFactory : ShapeFactory
    {
        public override Shape CreateShape(float scale)
        {
            return new Pyramid(scale);
        }
    }
}
