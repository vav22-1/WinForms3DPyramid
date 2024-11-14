namespace WinForms3DPyramid
{
    public class CubeFactory : ShapeFactory
    {
        public override Shape CreateShape(float scale)
        {
            return new Cube(scale);
        }
    }
}
