using System.Drawing;
using System.Windows.Forms;

namespace WinForms3DPyramid
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.rotationSpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.drawFigurePanel = new WinForms3DPyramid.DoubleBufferedPanel();
            this.fasterRotateButton = new System.Windows.Forms.Button();
            this.slowerRotateButton = new System.Windows.Forms.Button();
            this.startStopButton = new System.Windows.Forms.Button();
            this.zInvertCheckBox = new System.Windows.Forms.CheckBox();
            this.yInvertCheckBox = new System.Windows.Forms.CheckBox();
            this.xInvertCheckBox = new System.Windows.Forms.CheckBox();
            this.rotateXAxisCheckBox = new System.Windows.Forms.CheckBox();
            this.rotateYAxisCheckBox = new System.Windows.Forms.CheckBox();
            this.rotateZAxisCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.rotationSpeedTrackBar)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rotationSpeedTrackBar
            // 
            this.rotationSpeedTrackBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rotationSpeedTrackBar.Location = new System.Drawing.Point(739, 77);
            this.rotationSpeedTrackBar.Maximum = 300;
            this.rotationSpeedTrackBar.Minimum = 10;
            this.rotationSpeedTrackBar.Name = "rotationSpeedTrackBar";
            this.rotationSpeedTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.rotationSpeedTrackBar.Size = new System.Drawing.Size(45, 341);
            this.rotationSpeedTrackBar.TabIndex = 0;
            this.rotationSpeedTrackBar.TickFrequency = 10;
            this.rotationSpeedTrackBar.Value = 100;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.6692F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.54891F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.78951F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.99239F));
            this.tableLayoutPanel.Controls.Add(this.xInvertCheckBox, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.drawFigurePanel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.rotationSpeedTrackBar, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.fasterRotateButton, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.slowerRotateButton, 3, 2);
            this.tableLayoutPanel.Controls.Add(this.startStopButton, 3, 3);
            this.tableLayoutPanel.Controls.Add(this.zInvertCheckBox, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.yInvertCheckBox, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.rotateXAxisCheckBox, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.rotateYAxisCheckBox, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.rotateZAxisCheckBox, 2, 4);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.625669F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.16221F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.982175F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.525846F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.525846F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(834, 561);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // drawFigurePanel
            // 
            this.drawFigurePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel.SetColumnSpan(this.drawFigurePanel, 3);
            this.drawFigurePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawFigurePanel.Location = new System.Drawing.Point(0, 0);
            this.drawFigurePanel.Margin = new System.Windows.Forms.Padding(0);
            this.drawFigurePanel.Name = "drawFigurePanel";
            this.tableLayoutPanel.SetRowSpan(this.drawFigurePanel, 3);
            this.drawFigurePanel.Size = new System.Drawing.Size(690, 498);
            this.drawFigurePanel.TabIndex = 0;
            // 
            // fasterRotateButton
            // 
            this.fasterRotateButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fasterRotateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.fasterRotateButton.Location = new System.Drawing.Point(705, 3);
            this.fasterRotateButton.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.fasterRotateButton.Name = "fasterRotateButton";
            this.fasterRotateButton.Size = new System.Drawing.Size(114, 48);
            this.fasterRotateButton.TabIndex = 1;
            this.fasterRotateButton.Text = "Ускорить вращение (Клавиша Вверх)";
            this.fasterRotateButton.UseVisualStyleBackColor = true;
            this.fasterRotateButton.Click += new System.EventHandler(this.FasterRotateButton_Click);
            // 
            // slowerRotateButton
            // 
            this.slowerRotateButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slowerRotateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.slowerRotateButton.Location = new System.Drawing.Point(705, 445);
            this.slowerRotateButton.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.slowerRotateButton.Name = "slowerRotateButton";
            this.slowerRotateButton.Size = new System.Drawing.Size(114, 50);
            this.slowerRotateButton.TabIndex = 2;
            this.slowerRotateButton.Text = "Замедлить вращение (Клавиша Вниз)";
            this.slowerRotateButton.UseVisualStyleBackColor = true;
            this.slowerRotateButton.Click += new System.EventHandler(this.SlowerRotateButton_Click);
            // 
            // startStopButton
            // 
            this.startStopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startStopButton.Location = new System.Drawing.Point(705, 508);
            this.startStopButton.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.startStopButton.Name = "startStopButton";
            this.tableLayoutPanel.SetRowSpan(this.startStopButton, 2);
            this.startStopButton.Size = new System.Drawing.Size(114, 43);
            this.startStopButton.TabIndex = 3;
            this.startStopButton.Text = "Старт (Клавиша Пробел)";
            this.startStopButton.UseVisualStyleBackColor = true;
            // 
            // zInvertCheckBox
            // 
            this.zInvertCheckBox.AutoSize = true;
            this.zInvertCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zInvertCheckBox.Location = new System.Drawing.Point(489, 501);
            this.zInvertCheckBox.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.zInvertCheckBox.Name = "zInvertCheckBox";
            this.zInvertCheckBox.Size = new System.Drawing.Size(198, 25);
            this.zInvertCheckBox.TabIndex = 4;
            this.zInvertCheckBox.Text = "Инвертировать Z";
            this.zInvertCheckBox.UseVisualStyleBackColor = true;
            this.zInvertCheckBox.CheckedChanged += new System.EventHandler(this.ZInvertCheckBox_CheckedChanged);
            // 
            // yInvertCheckBox
            // 
            this.yInvertCheckBox.AutoSize = true;
            this.yInvertCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yInvertCheckBox.Location = new System.Drawing.Point(260, 501);
            this.yInvertCheckBox.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.yInvertCheckBox.Name = "yInvertCheckBox";
            this.yInvertCheckBox.Size = new System.Drawing.Size(196, 25);
            this.yInvertCheckBox.TabIndex = 5;
            this.yInvertCheckBox.Text = "Инвертировать Y";
            this.yInvertCheckBox.UseVisualStyleBackColor = true;
            this.yInvertCheckBox.CheckedChanged += new System.EventHandler(this.YInvertCheckBox_CheckedChanged);
            // 
            // xInvertCheckBox
            // 
            this.xInvertCheckBox.AutoSize = true;
            this.xInvertCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xInvertCheckBox.Location = new System.Drawing.Point(30, 501);
            this.xInvertCheckBox.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.xInvertCheckBox.Name = "xInvertCheckBox";
            this.xInvertCheckBox.Size = new System.Drawing.Size(197, 25);
            this.xInvertCheckBox.TabIndex = 6;
            this.xInvertCheckBox.Text = "Инвертировать X";
            this.xInvertCheckBox.UseVisualStyleBackColor = true;
            this.xInvertCheckBox.CheckedChanged += new System.EventHandler(this.XInvertCheckBox_CheckedChanged);
            // 
            // rotateXAxisCheckBox
            // 
            this.rotateXAxisCheckBox.AutoSize = true;
            this.rotateXAxisCheckBox.Checked = true;
            this.rotateXAxisCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rotateXAxisCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rotateXAxisCheckBox.Location = new System.Drawing.Point(30, 532);
            this.rotateXAxisCheckBox.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.rotateXAxisCheckBox.Name = "rotateXAxisCheckBox";
            this.rotateXAxisCheckBox.Size = new System.Drawing.Size(197, 26);
            this.rotateXAxisCheckBox.TabIndex = 7;
            this.rotateXAxisCheckBox.Text = "Вращать по оси X";
            this.rotateXAxisCheckBox.UseVisualStyleBackColor = true;
            this.rotateXAxisCheckBox.CheckedChanged += new System.EventHandler(this.RotateXAxisCheckBox_CheckedChanged);
            // 
            // rotateYAxisCheckBox
            // 
            this.rotateYAxisCheckBox.AutoSize = true;
            this.rotateYAxisCheckBox.Checked = true;
            this.rotateYAxisCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rotateYAxisCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rotateYAxisCheckBox.Location = new System.Drawing.Point(260, 532);
            this.rotateYAxisCheckBox.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.rotateYAxisCheckBox.Name = "rotateYAxisCheckBox";
            this.rotateYAxisCheckBox.Size = new System.Drawing.Size(196, 26);
            this.rotateYAxisCheckBox.TabIndex = 8;
            this.rotateYAxisCheckBox.Text = "Вращать по оси Y";
            this.rotateYAxisCheckBox.UseVisualStyleBackColor = true;
            this.rotateYAxisCheckBox.CheckedChanged += new System.EventHandler(this.RotateYAxisCheckBox_CheckedChanged);
            // 
            // rotateZAxisCheckBox
            // 
            this.rotateZAxisCheckBox.AutoSize = true;
            this.rotateZAxisCheckBox.Checked = true;
            this.rotateZAxisCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rotateZAxisCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rotateZAxisCheckBox.Location = new System.Drawing.Point(489, 532);
            this.rotateZAxisCheckBox.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.rotateZAxisCheckBox.Name = "rotateZAxisCheckBox";
            this.rotateZAxisCheckBox.Size = new System.Drawing.Size(198, 26);
            this.rotateZAxisCheckBox.TabIndex = 9;
            this.rotateZAxisCheckBox.Text = "Вращать по оси Z";
            this.rotateZAxisCheckBox.UseVisualStyleBackColor = true;
            this.rotateZAxisCheckBox.CheckedChanged += new System.EventHandler(this.RotateZAxisCheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 561);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.rotationSpeedTrackBar)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TrackBar rotationSpeedTrackBar;
        private DoubleBufferedPanel drawFigurePanel;
        private TableLayoutPanel tableLayoutPanel;
        private Button fasterRotateButton;
        private Button slowerRotateButton;
        private Button startStopButton;
        private CheckBox zInvertCheckBox;
        private CheckBox yInvertCheckBox;
        private CheckBox xInvertCheckBox;
        private CheckBox rotateXAxisCheckBox;
        private CheckBox rotateYAxisCheckBox;
        private CheckBox rotateZAxisCheckBox;
    }
}

