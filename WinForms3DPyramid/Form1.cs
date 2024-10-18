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
    public partial class Form1 : Form
    {
        private Pyramid pyramid;

        public Form1()
        {
            InitializeComponent();
            pyramid = new Pyramid(600);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            pyramid.Draw(e.Graphics, this.ClientSize);
        }
    }
}
