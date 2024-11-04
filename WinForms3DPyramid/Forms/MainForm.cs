using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        //Логическая переменная, хранящее значение, вращается ли фигура на данный момент
        private bool isFigureRotate = false;

        //Переменная, хранящая интервал таймеров вращения или же скорость вращения
        private int rotationSpeed = 10;

        //Переменные, определяющие, вращается ли фигура по оси положительно или отрицательно
        private bool xRotateFactor;
        private bool yRotateFactor;
        private bool zRotateFactor;

        //Таймеры вращения фигуры по каждой оси
        private Timer xRotationTimer;
        private Timer yRotationTimer;
        private Timer zRotationTimer;

        //Переменные для поворота фигуры с помощью правой кнопки мыши
        private bool isRightMouseButtonPressed = false;

        //Переменная для изменения скорости вращения фигуры с помощью мыши
        private float mouseRotationSpeed = 0.1f;
        public MainForm()
        {
            InitializeComponent();
            baseFormSize = this.Size;
            this.KeyPreview = true;

            //Установка стиля отрисовки формы для предотвращения мерцания фигуры при использовании Invalidate()
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            //Определение таймеров вращения фигуры по осям
            xRotationTimer = new Timer();
            xRotationTimer.Tick += (s, e) =>
            {
                ShapeDrawer.RotateFigure(pyramid, smallPyramid, "X", xRotateFactor);
                drawPyramidPanel.Invalidate();
            };

            yRotationTimer = new Timer();
            yRotationTimer.Tick += (s, e) =>
            {
                ShapeDrawer.RotateFigure(pyramid, smallPyramid, "Y", yRotateFactor);
                drawPyramidPanel.Invalidate();
            };

            zRotationTimer = new Timer();
            zRotationTimer.Tick += (s, e) =>
            {
                ShapeDrawer.RotateFigure(pyramid, smallPyramid, "Z", zRotateFactor);
                drawPyramidPanel.Invalidate();
            };
            SetTimers();

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
            this.KeyUp += MainForm_KeyUp;

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
            if(e.Button == MouseButtons.Left)
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

        private void DrawPyramidPanel_MouseUp(object sender, MouseEventArgs e)
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
        private void DrawPyramidPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseMovePyramid)
            {
                //Получение значений перемещения мыши по осям x и y
                newMousePosition.X += e.Location.X - lastMousePosition.X;
                newMousePosition.Y += e.Location.Y - lastMousePosition.Y;

                lastMousePosition = e.Location;

                //Ограничение перемещения, чтобы пользователь не мог переместиться слишком далеко от фигуры и потерять её
                float maxOffsetX = (float)drawPyramidPanel.ClientSize.Width / 2;
                float maxOffsetY = (float)drawPyramidPanel.ClientSize.Height / 2;
                float moveFactor = 1f + (1f - zoomFactor);

                newMousePosition.X = Math.Max(Math.Min(newMousePosition.X, maxOffsetX * moveFactor), -maxOffsetX * zoomFactor);
                newMousePosition.Y = Math.Max(Math.Min(newMousePosition.Y, maxOffsetY * moveFactor), -maxOffsetY * zoomFactor);
                drawPyramidPanel.Invalidate();
            }
            else if (isRightMouseButtonPressed)
            {
                //Вычисление смещения мыши по осям X и Y
                float deltaX = e.Location.X - lastMousePosition.X;
                float deltaY = e.Location.Y - lastMousePosition.Y;

                //Поворот фигуры в зависимости от смещения мыши
                ShapeDrawer.RotatePyramidX(pyramid, deltaY * mouseRotationSpeed);
                ShapeDrawer.RotatePyramidY(pyramid, deltaX * mouseRotationSpeed);
                ShapeDrawer.RotatePyramidX(smallPyramid, deltaY * mouseRotationSpeed);
                ShapeDrawer.RotatePyramidY(smallPyramid, deltaX * mouseRotationSpeed);

                lastMousePosition = e.Location;
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

        //Событие нажатия клавиши клавиатуры для управления вращением фигуры как по отдельным осям, так и по всем трем одновременно
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
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
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
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
                    rotationSpeed = Math.Min(rotationSpeed + 10, 100);
                    mouseRotationSpeed = Math.Min(mouseRotationSpeed + 0.05f, 0.5f);
                    SetTimers(rotationSpeed);
                    break;
                case Keys.Down:
                    rotationSpeed = Math.Max(1, rotationSpeed - 10);
                    mouseRotationSpeed = Math.Max(0.01f, mouseRotationSpeed - 0.05f);
                    SetTimers(rotationSpeed);
                    break;
            }
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

        //Метод для установления скорости вращения по осям
        private void SetTimers(int speed = 10)
        {
            xRotationTimer.Interval = speed;
            yRotationTimer.Interval = speed;
            zRotationTimer.Interval = speed;
        }
    }
}