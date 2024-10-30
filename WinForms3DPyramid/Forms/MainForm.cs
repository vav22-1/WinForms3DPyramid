﻿using System;
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

        //Событие нажатия клавиши клавиатуры для управления вращением фигуры как по отдельным осям, так и по всем трем одновременно
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.X:
                    if (!xRotationTimer.Enabled) xRotationTimer.Start();
                    break;
                case Keys.Y:
                    if (!yRotationTimer.Enabled) yRotationTimer.Start();
                    break;
                case Keys.Z:
                    if (!zRotationTimer.Enabled) zRotationTimer.Start();
                    break;
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
                    rotationSpeed = Math.Max(1, Math.Min(rotationSpeed + 10, 100));
                    SetTimers(rotationSpeed);
                    break;
                case Keys.Down:
                    rotationSpeed = Math.Max(1, Math.Min(rotationSpeed - 10, 100));
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