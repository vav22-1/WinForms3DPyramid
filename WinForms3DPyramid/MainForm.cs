using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void DrawPyramidPanel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            pyramid.Draw(e.Graphics, DrawPyramidPanel.ClientSize);
            smallPyramid.Draw(e.Graphics, DrawPyramidPanel.ClientSize);
            pyramid.ConnectVertices(e.Graphics, smallPyramid);
        }
    }
}
