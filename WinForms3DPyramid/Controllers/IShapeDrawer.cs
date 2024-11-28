using System.Drawing;
using System.Threading.Tasks;

namespace WinForms3DPyramid
{
    public interface IShapeDrawer
    {
        void SetBaseClientSize(Size size);
        void SetClientSize(Size size);
        PointF Get2DCoords(Point3D point);

        PointF[] GetVertices(Shape pyramid);
        void DrawShape(Graphics g, Shape pyramid, Pen color, Brush verticesColor);

        void ConnectVertices(Graphics g, Shape pyramidOne, Shape pyramidTwo, Pen color);

        void DrawFigure(Graphics g, Figure figure);

    }
}
