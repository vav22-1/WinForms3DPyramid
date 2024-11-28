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
            MakeMenus();
            this.MinimumSize = new Size(530, 460);
            baseFormSize = Size;
            this.KeyDown += Form_KeyDown;
            this.KeyUp += Form_KeyUp;
            rotationSpeedTrackBar.KeyDown += new KeyEventHandler(TrackBar_KeyDown);

            //Словарь для всех элементов формы, который хранит их отступы
            baseElementMargins = new Dictionary<Control, Padding>();
            foreach (Control control in tableLayoutPanel.Controls)
            {
                baseElementMargins[control] = control.Margin;
            }
            ShapeFactory factory = new PyramidFactory();
            figureController = new FigureController(new Figure(factory), this, drawFigurePanel, rotationSpeedTrackBar,startStopButton);
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
        private void TrackBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
        }
        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.X:
                    rotateXAxisCheckBox.Checked = !rotateXAxisCheckBox.Checked;
                    break;
                case Keys.Y:
                    rotateYAxisCheckBox.Checked = !rotateYAxisCheckBox.Checked;
                    break;
                case Keys.Z:
                    rotateZAxisCheckBox.Checked = !rotateZAxisCheckBox.Checked;
                    break;
            }
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        private void FasterRotateButton_Click(object sender, EventArgs e)
        {
            rotationSpeedTrackBar.Value = Math.Min(rotationSpeedTrackBar.Value + 10, rotationSpeedTrackBar.Maximum);
        }

        private void SlowerRotateButton_Click(object sender, EventArgs e)
        {
            rotationSpeedTrackBar.Value = Math.Max(rotationSpeedTrackBar.Value - 10, rotationSpeedTrackBar.Minimum);
        }

        private void XInvertCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            figureController.InverseShape("X");
        }
        private void YInvertCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            figureController.InverseShape("Y");
        }

        private void ZInvertCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            figureController.InverseShape("Z");
        }

        private void RotateXAxisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            figureController.StartStopRotateShape("X");
            xInvertCheckBox.Enabled = !xInvertCheckBox.Enabled;
        }

        private void RotateYAxisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            figureController.StartStopRotateShape("Y");
            yInvertCheckBox.Enabled = !yInvertCheckBox.Enabled;
        }

        private void RotateZAxisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            figureController.StartStopRotateShape("Z");
            zInvertCheckBox.Enabled = !zInvertCheckBox.Enabled;
        }
        public void MakeMenus()
        {
            MenuStrip menuStrip = new MenuStrip();

            //Меню "Фигуры"
            ToolStripMenuItem shapeMenuItem = new ToolStripMenuItem("Фигуры");
            ToolStripMenuItem cubeMenuItem = new ToolStripMenuItem("Куб");
            ToolStripMenuItem pyramidMenuItem = new ToolStripMenuItem("Пирамида");
            cubeMenuItem.Click += (sender, args) => SwitchFactory(new CubeFactory());
            pyramidMenuItem.Click += (sender, args) => SwitchFactory(new PyramidFactory());
            shapeMenuItem.DropDownItems.Add(cubeMenuItem);
            shapeMenuItem.DropDownItems.Add(pyramidMenuItem);
            menuStrip.Items.Add(shapeMenuItem);

            //Меню "Цвета"
            ToolStripMenuItem colorMenuItem = new ToolStripMenuItem("Цвета");
            ToolStripMenuItem linesColorMenuItem = new ToolStripMenuItem("Цвет линий фигуры");
            ToolStripMenuItem connectionsColorMenuItem = new ToolStripMenuItem("Цвет соединений фигуры");
            ToolStripMenuItem verticesColorMenuItem = new ToolStripMenuItem("Цвет вершин фигуры");
            ToolStripMenuItem facesColorMenuItem = new ToolStripMenuItem("Цвет граней фигуры");
            linesColorMenuItem.Click += (sender, args) => SetColor("lines");
            connectionsColorMenuItem.Click += (sender, args) => SetColor("connections");
            verticesColorMenuItem.Click += (sender, args) => SetColor("vertices");
            facesColorMenuItem.Click += (sender, args) => SetColor("faces");
            colorMenuItem.DropDownItems.Add(linesColorMenuItem);
            colorMenuItem.DropDownItems.Add(connectionsColorMenuItem);
            colorMenuItem.DropDownItems.Add(verticesColorMenuItem);
            colorMenuItem.DropDownItems.Add(facesColorMenuItem);
            menuStrip.Items.Add(colorMenuItem);

            this.Controls.Add(menuStrip);
        }

        //Метод, меняющий фабрику при выборе другой фигуры
        private void SwitchFactory(ShapeFactory factory)
        {
            var newFigure = new Figure(factory);
            figureController.SetFigure(newFigure);
            drawFigurePanel.Invalidate();
        }
        private void SetColor(string colorType)
        {
            figureController.ChangeFigureColor(colorType);
        }
    }
}