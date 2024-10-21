using System.Windows.Forms;

namespace WinForms3DPyramid
{
    public partial class MainForm : Form
    {
        private Pyramid pyramid;
        private Pyramid smallPyramid;

        public MainForm()
        {
            InitializeComponent();
            pyramid = new Pyramid();
            smallPyramid = new Pyramid(0.5f);
            ShapeDrawer.SetClientSize(DrawPyramidPanel.ClientSize);
        }

        private void DrawPyramidPanel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ShapeDrawer.DrawPyramids(e.Graphics, pyramid, smallPyramid);
        }
    }
}
