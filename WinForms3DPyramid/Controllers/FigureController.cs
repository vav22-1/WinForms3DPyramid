using System;
using System.Drawing;
using System.Windows.Forms;
using WinForms3DPyramid.Controllers;

namespace WinForms3DPyramid
{
    internal class FigureController
    {
        private Figure figure;
        private Form form;
        private DoubleBufferedPanel drawPanel;
        private PyramidDrawer pyramidDrawer;

        //Переменная коэффициента приближения
        private float zoomFactor = 1f;

        //Переменная, показывающая, зажал ли пользователь клавишу мыши внутри элемента drawPyramidPanel 
        private bool isMouseMovePyramid = false;

        //Переменная, хранящая последнее положение мыши в момент, когда пользователь нажал клавишу мыши внутри drawPyramidPanel
        private PointF lastMousePosition;

        //Переменная, хранящая изменение положения мыши внутри элемента drawPyramidPanel
        private PointF newMousePosition;

        //Переменные, определяющие, вращается ли фигура по оси положительно или отрицательно
        private bool xRotateFactor;
        private bool yRotateFactor;
        private bool zRotateFactor;

        //Таймеры вращения фигуры по каждой оси
        private Timer xRotationTimer;
        private Timer yRotationTimer;
        private Timer zRotationTimer;

        //Логическая переменная, хранящее значение, вращается ли фигура на данный момент
        private bool isFigureRotate = false;

        //Переменная, хранящая интервал таймеров вращения или же скорость вращения
        private int rotationSpeed = 10;

        //Переменные для поворота фигуры с помощью правой кнопки мыши
        private bool isRightMouseButtonPressed = false;

        //Переменная для изменения скорости вращения фигуры с помощью мыши
        private float mouseRotationSpeed = 0.01f;
        public FigureController(Figure mainFigure, Form mainForm, DoubleBufferedPanel drawPanel)
        {
            figure = mainFigure;
            form = mainForm;
            this.drawPanel = drawPanel;
            pyramidDrawer = new PyramidDrawer();
            pyramidDrawer.SetBaseClientSize(drawPanel.ClientSize);
            form.KeyPreview = true;

            //Определение таймеров вращения фигуры по осям
            xRotationTimer = new Timer();
            xRotationTimer.Tick += (s, e) =>
            {
                pyramidDrawer.RotateFigure(figure, 'X', xRotateFactor);
                drawPanel.Invalidate();
            };

            yRotationTimer = new Timer();
            yRotationTimer.Tick += (s, e) =>
            {
                pyramidDrawer.RotateFigure(figure, 'Y', yRotateFactor);
                drawPanel.Invalidate();
            };

            zRotationTimer = new Timer();
            zRotationTimer.Tick += (s, e) =>
            {
                pyramidDrawer.RotateFigure(figure, 'Z', zRotateFactor);
                drawPanel.Invalidate();
            };
            SetTimers();

            form.KeyDown += FormKeyDown;
            form.KeyUp += FormKeyUp;

            //Создание обработчика события прокрутки колёсика мыши
            drawPanel.MouseWheel += MouseWheel;
            drawPanel.MouseDown += MouseDown;
            drawPanel.MouseUp += MouseUp;
            drawPanel.MouseMove += MouseMove;
            drawPanel.Resize += Resize;
            drawPanel.Paint += OnPanelPaint;
            drawPanel.Invalidate();
        }

        //Событие нажатия клавиши клавиатуры для управления вращением фигуры как по отдельным осям, так и по всем трем одновременно
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    TimersControl(!isFigureRotate);
                    break;
            }
            isFigureRotate = xRotationTimer.Enabled || yRotationTimer.Enabled || zRotationTimer.Enabled;
        }

        //Событие нажатия клавиши клавиатуры для управления направлением вращения по отдельным осям, а также для увеличения и уменьшения скорости вращения
        private void FormKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.X:
                    xRotateFactor = !xRotateFactor;
                    break;
                case Keys.Y:
                    yRotateFactor = !yRotateFactor;
                    break;
                case Keys.Z:
                    zRotateFactor = !zRotateFactor;
                    break;
                case Keys.Up:
                    rotationSpeed = Math.Max(1, rotationSpeed - 10);
                    mouseRotationSpeed = Math.Min(mouseRotationSpeed + 0.005f, 0.1f);
                    SetTimers(rotationSpeed);
                    break;
                case Keys.Down:
                    rotationSpeed = Math.Min(rotationSpeed + 10, 100);
                    mouseRotationSpeed = Math.Max(0.001f, mouseRotationSpeed - 0.005f);
                    SetTimers(rotationSpeed);
                    break;
            }
        }
        //Обработка события прокрутки колёсика мыши для изменения зума
        private void MouseWheel(object sender, MouseEventArgs e)
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
            drawPanel.Invalidate();
        }
        private void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseMovePyramid = true;
                lastMousePosition = e.Location;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isRightMouseButtonPressed = true;
                lastMousePosition = e.Location;
            }
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseMovePyramid = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isRightMouseButtonPressed = false;
            }
        }

        //Метод для перемещения и вращение фигуры внутри элемента drawPyramidPanel при помощи мыши
        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseMovePyramid)
            {
                //Получение значений перемещения мыши по осям x и y
                newMousePosition.X += e.Location.X - lastMousePosition.X;
                newMousePosition.Y += e.Location.Y - lastMousePosition.Y;

                lastMousePosition = e.Location;

                //Ограничение перемещения, чтобы пользователь не мог переместиться слишком далеко от фигуры и потерять её
                float maxOffsetX = (float)drawPanel.ClientSize.Width / 2;
                float maxOffsetY = (float)drawPanel.ClientSize.Height / 2;
                float moveFactor = 1f + (1f - zoomFactor);

                newMousePosition.X = Math.Max(Math.Min(newMousePosition.X, maxOffsetX * moveFactor), -maxOffsetX * zoomFactor);
                newMousePosition.Y = Math.Max(Math.Min(newMousePosition.Y, maxOffsetY * moveFactor), -maxOffsetY * zoomFactor);
                drawPanel.Invalidate();
            }
            else if (isRightMouseButtonPressed)
            {
                //Вычисление смещения мыши по осям X и Y
                float deltaX = e.Location.X - lastMousePosition.X;
                float deltaY = e.Location.Y - lastMousePosition.Y;

                //Поворот фигуры в зависимости от смещения мыши
                foreach (Shape shapes in figure.GetShapes())
                {
                    pyramidDrawer.RotateShape(shapes, deltaY * mouseRotationSpeed, 'X');
                    pyramidDrawer.RotateShape(shapes, deltaX * mouseRotationSpeed, 'Y');
                }

                lastMousePosition = e.Location;
                drawPanel.Invalidate();
            }
        }
        //Метод срабатывающий при изменении размера drawPyramidPanel для перерисовки пирамид
        private void Resize(object sender, System.EventArgs e)
        {
            drawPanel.Invalidate();
        }
        //Метод для управления таймерами вращения
        private void TimersControl(bool control)
        {
            if (control)
            {
                if (!xRotationTimer.Enabled) xRotationTimer.Start();
                if (!yRotationTimer.Enabled) yRotationTimer.Start();
                if (!zRotationTimer.Enabled) zRotationTimer.Start();
            }
            else
            {
                xRotationTimer.Stop();
                yRotationTimer.Stop();
                zRotationTimer.Stop();
            }
            isFigureRotate = control;
        }

        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            //Обновление размера окна
            pyramidDrawer.SetClientSize(drawPanel.ClientSize);
            //Перемещение изображения внутри элемента drawPyramidPanel
            e.Graphics.TranslateTransform(newMousePosition.X, newMousePosition.Y);

            //Масштабирование изображения с помощью переменной zoomFactor
            e.Graphics.ScaleTransform(zoomFactor, zoomFactor);

            pyramidDrawer.DrawFigure(e.Graphics, figure);
        }
        //Метод для установления скорости вращения по осям
        private void SetTimers(int speed = 10)
        {
            xRotationTimer.Interval = speed;
            yRotationTimer.Interval = speed;
            zRotationTimer.Interval = speed;
        }
    }
}
