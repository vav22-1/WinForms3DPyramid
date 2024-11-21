using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms3DPyramid.Controllers;

namespace WinForms3DPyramid
{
    internal class FigureController
    {
        private Figure figure;
        private Form form;
        private DoubleBufferedPanel drawPanel;
        private IShapeDrawer figureDrawer;

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

        //Логическая переменная, хранящее значение, вращается ли фигура на данный момент
        private bool isFigureRotate = false;

        //Переменная, хранящая скорость вращения
        private float rotationSpeed = 1f;

        //Переменные для поворота фигуры с помощью правой кнопки мыши
        private bool isRightMouseButtonPressed = false;

        private TrackBar rotationSpeedTrackBar;
        private bool isChangingByKeyboard = false;

        public FigureController(Figure mainFigure, Form mainForm, DoubleBufferedPanel drawPanel, TrackBar trackBar)
        {
            figure = mainFigure;
            form = mainForm;
            this.drawPanel = drawPanel;
            rotationSpeedTrackBar = trackBar;
            switch (mainFigure.GetShapeType().Name)
            {
                case nameof(Pyramid):
                    figureDrawer = new PyramidDrawer();
                    break;
                case nameof(Cube):
                    figureDrawer = new CubeDrawer();
                    break;

            }
            figureDrawer.SetBaseClientSize(drawPanel.ClientSize);
            form.KeyPreview = true;
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

            rotationSpeedTrackBar.Scroll += RotationSpeedTrackBar_Scroll;
            rotationSpeedTrackBar.ValueChanged += RotationSpeedTrackBar_ValueChanged;
        }

        public void SetFigure(Figure newFigure)
        {
            figure = newFigure;

            switch (newFigure.GetShapeType().Name)
            {
                case nameof(Pyramid):
                    figureDrawer = new PyramidDrawer();
                    break;
                case nameof(Cube):
                    figureDrawer = new CubeDrawer();
                    break;
            }

            figureDrawer.SetBaseClientSize(drawPanel.ClientSize);
            drawPanel.Invalidate();
        }

        //Событие нажатия клавиши клавиатуры для управления вращением фигуры как по отдельным осям, так и по всем трем одновременно
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    isFigureRotate = !isFigureRotate;
                    if (isFigureRotate)
                    {
                        _ = RotateFigureAsync();
                    }
                    break;
            }
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
                    rotationSpeed = Math.Min(rotationSpeed + 0.1f, 3f);
                    isChangingByKeyboard = true;
                    rotationSpeedTrackBar.Value = (int)(rotationSpeed*100);
                    break;
                case Keys.Down:
                    rotationSpeed = Math.Max(0.1f, rotationSpeed - 0.1f);
                    isChangingByKeyboard = true;
                    rotationSpeedTrackBar.Value = (int)(rotationSpeed * 100);
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
                //Получение значений перемещения мыши по осям X и Y
                newMousePosition.X += e.Location.X - lastMousePosition.X;
                newMousePosition.Y += e.Location.Y - lastMousePosition.Y;

                lastMousePosition = e.Location;

                //Ограничение перемещения, чтобы фигура не выходила за пределы панели
                float maxOffsetX = (float)drawPanel.ClientSize.Width / 2;
                float maxOffsetY = (float)drawPanel.ClientSize.Height / 2;
                newMousePosition.X = Math.Min(Math.Max(newMousePosition.X, -maxOffsetX), maxOffsetX);
                newMousePosition.Y = Math.Min(Math.Max(newMousePosition.Y, -maxOffsetY), maxOffsetY);

                drawPanel.Invalidate();
            }
            else if (isRightMouseButtonPressed)
            {
                //Вычисление смещения мыши по осям X и Y
                float deltaX = (e.Location.X - lastMousePosition.X) / 100f;
                float deltaY = (e.Location.Y - lastMousePosition.Y) / 100f;

                //Поворот фигуры в зависимости от смещения мыши
                foreach (Shape shapes in figure.GetShapes())
                {
                    RotateShape(shapes, deltaY, 'X');
                    RotateShape(shapes, deltaX, 'Y');
                }

                lastMousePosition = e.Location;
                drawPanel.Invalidate();
            }
        }

        //Метод срабатывающий при изменении размера drawPyramidPanel для перерисовки фигуры
        private void Resize(object sender, EventArgs e)
        {
            float maxOffsetX = (float)drawPanel.ClientSize.Width / 2;
            float maxOffsetY = (float)drawPanel.ClientSize.Height / 2;

            newMousePosition.X = Math.Min(Math.Max(newMousePosition.X, -maxOffsetX), maxOffsetX);
            newMousePosition.Y = Math.Min(Math.Max(newMousePosition.Y, -maxOffsetY), maxOffsetY);

            drawPanel.Invalidate();
        }

        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            figureDrawer.SetClientSize(drawPanel.ClientSize);
            float panelCenterX = drawPanel.ClientSize.Width / 2f;
            float panelCenterY = drawPanel.ClientSize.Height / 2f;

            e.Graphics.TranslateTransform(panelCenterX, panelCenterY);

            //Перемещение изображения внутри элемента drawPyramidPanel
            e.Graphics.TranslateTransform(newMousePosition.X, newMousePosition.Y);

            //Масштабирование изображения с помощью переменной zoomFactor
            e.Graphics.ScaleTransform(zoomFactor, zoomFactor);

            //Возврат трансформации обратно к центру панели
            e.Graphics.TranslateTransform(-panelCenterX, -panelCenterY);

            figureDrawer.DrawFigure(e.Graphics, figure);
        }

        public void SetRotationSpeed(float speed)
        {
            rotationSpeed = speed;
        }
        private void RotationSpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            if (isChangingByKeyboard)
            {
                isChangingByKeyboard = false;
                return;
            }
            rotationSpeed = (float)rotationSpeedTrackBar.Value/100f;
        }

        private void RotationSpeedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            rotationSpeed = (float)rotationSpeedTrackBar.Value/100f;
        }

        //Асинхронный метод для поворота всей фигуры с учетом направления по соответствующей оси
        public async Task RotateFigureAsync()
        {
            if (!isFigureRotate) return;
            while (isFigureRotate)
            {
                float deltaX = xRotateFactor ? -rotationSpeed * (float)Math.PI / 180f : rotationSpeed * (float)Math.PI / 180f;
                float deltaY = yRotateFactor ? -rotationSpeed * (float)Math.PI / 180f : rotationSpeed * (float)Math.PI / 180f;
                float deltaZ = zRotateFactor ? -rotationSpeed * (float)Math.PI / 180f : rotationSpeed * (float)Math.PI / 180f;
                foreach (Shape shape in figure.GetShapes())
                {
                    RotateShape(shape, deltaX, 'X');
                    RotateShape(shape, deltaY, 'Y');
                    RotateShape(shape, deltaZ, 'Z');
                }

                drawPanel.Invalidate();
                await Task.Delay(10);
            }
        }
        //Метод поворота трехмерной фигуры по осям с помощью соответствующих матриц поворота
        public void RotateShape(Shape shape, float radians, char axis)
        {
            List<Point3D> points = shape.GetPoints();
            float cosTheta = (float)Math.Cos(radians);
            float sinTheta = (float)Math.Sin(radians);

            for (int i = 0; i < points.Count; i++)
            {
                float x = points[i].GetX();
                float y = points[i].GetY();
                float z = points[i].GetZ();

                switch (axis)
                {
                    case 'X':
                        points[i] = new Point3D(x, y * cosTheta - z * sinTheta, y * sinTheta + z * cosTheta);
                        break;
                    case 'Y':
                        points[i] = new Point3D(x * cosTheta + z * sinTheta, y, -x * sinTheta + z * cosTheta);
                        break;
                    case 'Z':
                        points[i] = new Point3D(x * cosTheta - y * sinTheta, x * sinTheta + y * cosTheta, z);
                        break;
                }
            }
        }
        public void InverseShape(string axis)
        {
            switch(axis)
            {
                case "X":
                    xRotateFactor = !xRotateFactor;
                    break;
                case "Y":
                    yRotateFactor = !yRotateFactor;
                    break;
                case "Z":
                    zRotateFactor = !zRotateFactor;
                    break;
            }
        }
        public void StartStopRotate(Button button)
        {
            isFigureRotate = !isFigureRotate;
            button.Text = "Старт (Клавиша Пробел)";
            if (isFigureRotate)
            {
                button.Text = "Стоп (Клавиша Пробел)";
                _ = RotateFigureAsync();
            }
        }
    }
}