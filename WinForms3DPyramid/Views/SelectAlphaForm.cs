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
    public partial class SelectAlphaForm : Form
    {
        private Color initialColor;
        private Color selectedColor;
        private Action<Color> onColorSelected;

        public SelectAlphaForm(Color initialColor, Action<Color> onColorSelected)
        {
            InitializeComponent();
            this.initialColor = initialColor;
            this.selectedColor = initialColor;
            this.onColorSelected = onColorSelected;

            colorPreviewPanel.BackColor = initialColor;
            alphaTrackBar.Value = initialColor.A;
        }

        private void alphaTrackBar_ValueChanged(object sender, EventArgs e)
        {
            selectedColor = Color.FromArgb(alphaTrackBar.Value, initialColor.R, initialColor.G, initialColor.B);
            colorPreviewPanel.BackColor = selectedColor;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            onColorSelected(selectedColor);
            this.Close();
        }
    }

}
