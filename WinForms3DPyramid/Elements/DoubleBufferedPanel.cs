using System.Windows.Forms;

namespace WinForms3DPyramid
{
    //Класс, наследующий элемент Panel с установленным параметром DoubleBuffered для включения двойной буферизации и предотвращения мерцания фигуры при использовании Invalidate()
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }
    }
}
