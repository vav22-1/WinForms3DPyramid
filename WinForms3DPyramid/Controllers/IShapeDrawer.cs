﻿using System.Drawing;

namespace WinForms3DPyramid
{
    public interface IShapeDrawer
    {
        void SetBaseClientSize(Size size);
        void SetClientSize(Size size);
        PointF Get2DCoords(Point3D point);

        PointF[] GetVertices(Shape pyramid);
        void DrawPyramid(Graphics g, Shape pyramid, Pen color);

        void ConnectVertices(Graphics g, Shape pyramidOne, Shape pyramidTwo, Pen color);

        void DrawFigure(Graphics g, Figure figure);

        void RotateShape(Shape pyramid, float radians, char axis);

    }
}