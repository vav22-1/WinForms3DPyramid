using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms3DPyramid
{
    public partial class MainForm : Form
    {
        private Size baseFormSize;
        private Dictionary<Control, (int elementWidth, int elementHeight)> elementsToScale;
        private FigureController figureController;

        public MainForm()
        {
            InitializeComponent();
            baseFormSize = Size;

            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem shapeMenuItem = new ToolStripMenuItem("Фигуры");
            ToolStripMenuItem cubeMenuItem = new ToolStripMenuItem("Куб");
            ToolStripMenuItem pyramidMenuItem = new ToolStripMenuItem("Пирамида");
            cubeMenuItem.Click += (sender, args) => SwitchFactory(new CubeFactory());
            pyramidMenuItem.Click += (sender, args) => SwitchFactory(new PyramidFactory());
            shapeMenuItem.DropDownItems.Add(cubeMenuItem);
            shapeMenuItem.DropDownItems.Add(pyramidMenuItem);
            menuStrip.Items.Add(shapeMenuItem);
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            //Установка стиля отрисовки формы для предотвращения мерцания фигуры при использовании Invalidate()
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();


            //Словарь для всех элементов формы, которые должны менять свой размер при изменении размера формы
            elementsToScale = new Dictionary<Control, (int width, int height)>
            {
                {drawFigurePanel, (drawFigurePanel.Width, drawFigurePanel.Height)}
            };
            
            ShapeFactory factory = new PyramidFactory();
            figureController = new FigureController(new Figure(factory), this, drawFigurePanel);
        }

        //Скалирование элементов при изменении размера формы
        private void MainForm_Resize(object sender, EventArgs e)
        {
            ScaleELements();
        }

        //Метод для скалирования размеров элементов формы относительно изменения размеров самой формы
        private void ScaleELements()
        {
            float widthRatio = (float)ClientSize.Width / baseFormSize.Width;
            float heightRatio = (float)ClientSize.Height / baseFormSize.Height;
            foreach (var control in elementsToScale)
            {
                control.Key.Width = (int)(control.Value.elementWidth * widthRatio);
                control.Key.Height = (int)(control.Value.elementHeight * heightRatio);
            }
        }
        //Метод, меняющий фабрику при выборе другой фигуры
        private void SwitchFactory(ShapeFactory factory)
        {
            var newFigure = new Figure(factory);
            figureController.SetFigure(newFigure);
            drawFigurePanel.Invalidate();
        }
    }
}