using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms3DPyramid
{
    public partial class MainForm : Form
    {
        private Size baseFormSize;
        private Dictionary<Control, Padding> baseElementMargins;
        private FigureController figureController;

        public MainForm()
        {
            InitializeComponent();
            baseFormSize = Size;
            rotationSpeedTrackBar.KeyDown += new KeyEventHandler(trackBar_KeyDown);

            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem shapeMenuItem = new ToolStripMenuItem("Фигуры");
            ToolStripMenuItem cubeMenuItem = new ToolStripMenuItem("Куб");
            ToolStripMenuItem pyramidMenuItem = new ToolStripMenuItem("Пирамида");
            cubeMenuItem.Click += (sender, args) => SwitchFactory(new CubeFactory());
            pyramidMenuItem.Click += (sender, args) => SwitchFactory(new PyramidFactory());
            shapeMenuItem.DropDownItems.Add(cubeMenuItem);
            shapeMenuItem.DropDownItems.Add(pyramidMenuItem);
            menuStrip.Items.Add(shapeMenuItem);
            this.Controls.Add(menuStrip);

            //Словарь для всех элементов формы, который хранит их отступы
            baseElementMargins = new Dictionary<Control, Padding>();
            foreach (Control control in tableLayoutPanel.Controls)
            {
                baseElementMargins[control] = control.Margin;
            }
            ShapeFactory factory = new PyramidFactory();
            figureController = new FigureController(new Figure(factory), this, drawFigurePanel, rotationSpeedTrackBar);
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            //Рассчёт масштабирующего коэффициента для ширины и высоты
            float widthScale = (float)this.Width / baseFormSize.Width;
            float heightScale = (float)this.Height / baseFormSize.Height;

            //Масштабирование отступов в зависимости от текущего размера формы
            foreach (Control control in tableLayoutPanel.Controls)
            {
                Padding baseMargin = baseElementMargins[control];

                int newLeft = (int)(baseMargin.Left * widthScale);
                int newTop = (int)(baseMargin.Top * heightScale);
                int newRight = (int)(baseMargin.Right * widthScale);
                int newBottom = (int)(baseMargin.Bottom * heightScale);
                control.Margin = new Padding(newLeft, newTop, newRight, newBottom);
            }
        }
        //Метод, меняющий фабрику при выборе другой фигуры
        private void SwitchFactory(ShapeFactory factory)
        {
            var newFigure = new Figure(factory);
            figureController.SetFigure(newFigure);
            drawFigurePanel.Invalidate();
        }
        private void trackBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
        }

        private void fasterRotateButton_Click(object sender, EventArgs e)
        {
            rotationSpeedTrackBar.Value = Math.Min(rotationSpeedTrackBar.Value + 10, rotationSpeedTrackBar.Maximum);
        }

        private void slowerRotateButton_Click(object sender, EventArgs e)
        {
            rotationSpeedTrackBar.Value = Math.Min(Math.Max(rotationSpeedTrackBar.Value + 10, rotationSpeedTrackBar.Minimum), rotationSpeedTrackBar.Maximum);
        }

        private void InvertXButton_Click(object sender, EventArgs e)
        {
            figureController.InverseShape("X");
        }

        private void InvertYButton_Click(object sender, EventArgs e)
        {
            figureController.InverseShape("Y");
        }

        private void InvertZButton_Click(object sender, EventArgs e)
        {
            figureController.InverseShape("Z");
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            figureController.StartStopRotate(startStopButton);
        }
    }
}