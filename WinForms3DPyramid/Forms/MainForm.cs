using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinForms3DPyramid;

namespace WinForms3DPyramid
{
    public partial class MainForm : Form
    {
        private Pyramid pyramid;
        private Pyramid smallPyramid;
        private Size baseFormSize;
        private Dictionary<Control, (int elementWidth, int elementHeight)> elementsToScale;

        //Переменная коэффициента приближения
        private float zoomFactor = 1f;

        //Переменная, показывающая, зажал ли пользователь клавишу мыши внутри элемента drawPyramidPanel 
        private bool isMouseMovePyramid = false;

        //Переменная, хранящая последнее положение мыши в момент, когда пользователь нажал клавишу мыши внутри drawPyramidPanel
        private PointF lastMousePosition;

        //Переменная, хранящая изменение положения мыши внутри элемента drawPyramidPanel
        private PointF newMousePosition;

        public MainForm()
        {
            InitializeComponent();
            baseFormSize = this.Size;
            this.KeyPreview = true;

            //Словарь для всех элементов формы, которые должны менять свой размер при изменении размера формы
            elementsToScale = new Dictionary<Control, (int width, int height)>
            {
                {drawPyramidPanel, (drawPyramidPanel.Width, drawPyramidPanel.Height)}
            };

            //Создание двух объектов пирамид
            pyramid = new Pyramid();
            smallPyramid = new Pyramid(0.5f);

            //Передача в статические класс ShapeDrawer базовый размер элемента Panel под названием drawPyramidPanel
            ShapeDrawer.SetBaseClientSize(drawPyramidPanel.ClientSize);

            //Создание обработчика события прокрутки колёсика мыши
            drawPyramidPanel.MouseWheel += DrawPyramidPanel_MouseWheel;

            this.KeyDown += MainForm_KeyDown;

        }

        private void DrawPyramidPanel_Paint(object sender, PaintEventArgs e)
        {
            //Обновление размера окна
            ShapeDrawer.SetClientSize(drawPyramidPanel.ClientSize);

            //Перемещение изображения внутри элемента drawPyramidPanel
            e.Graphics.TranslateTransform(newMousePosition.X, newMousePosition.Y);

            //Масштабирование изображения с помощью переменной zoomFactor
            e.Graphics.ScaleTransform(zoomFactor, zoomFactor);

            //Рисование пирамид
            ShapeDrawer.DrawPyramids(e.Graphics, pyramid, smallPyramid);
        }

        //Обработка события прокрутки колёсика мыши для изменения зума
        private void DrawPyramidPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                //Ограничение максимального зума
                zoomFactor = Math.Min(2f, zoomFactor + 0.05f);
            }
            else
            {
                //Огранические минимального зума
                zoomFactor = Math.Max(0.05f, zoomFactor - 0.05f);
            }
            drawPyramidPanel.Invalidate();
        }

        //Метод срабатывающий при изменении размера drawPyramidPanel для перерисовки пирамид
        private void DrawPyramidPanel_Resize(object sender, System.EventArgs e)
        {
            drawPyramidPanel.Invalidate();
        }

        private void DrawPyramidPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseMovePyramid = true;
            lastMousePosition = e.Location;
        }

        private void DrawPyramidPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseMovePyramid = false;
        }

        //Метод для перемещения пирамиды внутри элемента drawPyramidPanel
        private void DrawPyramidPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseMovePyramid)
            {
                //Получение значений перемещения мыши по осям x и y
                newMousePosition.X += e.Location.X - lastMousePosition.X;
                newMousePosition.Y += e.Location.Y - lastMousePosition.Y;

                lastMousePosition = e.Location;
                float drawPanelWidth = drawPyramidPanel.ClientSize.Width;
                float drawPanelHeight = drawPyramidPanel.ClientSize.Height;

                //Ограничение перемещения, чтобы пользователь не мог переместиться слишком далеко от пирамиды и потерять её
                float pyramidWidth = 300 * zoomFactor;
                float pyramidHeight = 225 * zoomFactor;
                newMousePosition.X = Math.Max(Math.Min(newMousePosition.X, drawPanelWidth - pyramidWidth), -drawPanelWidth);
                newMousePosition.Y = Math.Max(Math.Min(newMousePosition.Y, drawPanelHeight - pyramidHeight), -drawPanelHeight);

                drawPyramidPanel.Invalidate();
            }
        }

        //Скалирование элементов при изменении размера формы
        private void MainForm_Resize(object sender, EventArgs e)
        {
            ScaleELements();
        }

        //Метод для скалирования размеров элементов формы относительно изменения размеров самой формы
        private void ScaleELements()
        {
            float widthRatio = (float)this.ClientSize.Width / baseFormSize.Width;
            float heightRatio = (float)this.ClientSize.Height / baseFormSize.Height;
            foreach (var control in elementsToScale)
            {
                control.Key.Width = (int)(control.Value.elementWidth * widthRatio);
                control.Key.Height = (int)(control.Value.elementHeight * heightRatio);
            }
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.X:
                    ShapeDrawer.RotatePyramidX(pyramid, 1f);
                    ShapeDrawer.RotatePyramidX(smallPyramid, 1f);
                    break;
                case Keys.Y:
                    ShapeDrawer.RotatePyramidY(pyramid, 1f);
                    ShapeDrawer.RotatePyramidY(smallPyramid, 1f);
                    break;
                case Keys.Z:
                    ShapeDrawer.RotatePyramidZ(pyramid, 1f);
                    ShapeDrawer.RotatePyramidZ(smallPyramid, 1f);
                    break;
            }
            drawPyramidPanel.Invalidate();
        }
    }
}