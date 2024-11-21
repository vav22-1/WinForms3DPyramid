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
            this.invertZButton = new System.Windows.Forms.Button();
            this.invertYButton = new System.Windows.Forms.Button();
            this.invertXButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rotationSpeedTrackBar)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rotationSpeedTrackBar
            // 
            this.rotationSpeedTrackBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rotationSpeedTrackBar.Location = new System.Drawing.Point(739, 69);
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
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.61104F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.491F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.73109F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.95667F));
            this.tableLayoutPanel.Controls.Add(this.drawFigurePanel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.rotationSpeedTrackBar, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.fasterRotateButton, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.slowerRotateButton, 3, 2);
            this.tableLayoutPanel.Controls.Add(this.startStopButton, 3, 3);
            this.tableLayoutPanel.Controls.Add(this.invertZButton, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.invertYButton, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.invertXButton, 0, 3);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.25F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.85714F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.03788F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.34091F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
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
            this.drawFigurePanel.Size = new System.Drawing.Size(690, 473);
            this.drawFigurePanel.TabIndex = 0;
            // 
            // fasterRotateButton
            // 
            this.fasterRotateButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fasterRotateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.fasterRotateButton.Location = new System.Drawing.Point(705, 3);
            this.fasterRotateButton.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.fasterRotateButton.Name = "fasterRotateButton";
            this.fasterRotateButton.Size = new System.Drawing.Size(114, 57);
            this.fasterRotateButton.TabIndex = 1;
            this.fasterRotateButton.Text = "Ускорить вращение (Клавиша Вверх)";
            this.fasterRotateButton.UseVisualStyleBackColor = true;
            this.fasterRotateButton.Click += new System.EventHandler(this.fasterRotateButton_Click);
            // 
            // slowerRotateButton
            // 
            this.slowerRotateButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slowerRotateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.slowerRotateButton.Location = new System.Drawing.Point(705, 420);
            this.slowerRotateButton.Margin = new System.Windows.Forms.Padding(15, 3, 15, 3);
            this.slowerRotateButton.Name = "slowerRotateButton";
            this.slowerRotateButton.Size = new System.Drawing.Size(114, 50);
            this.slowerRotateButton.TabIndex = 2;
            this.slowerRotateButton.Text = "Замедлить вращение (Клавиша Вниз)";
            this.slowerRotateButton.UseVisualStyleBackColor = true;
            this.slowerRotateButton.Click += new System.EventHandler(this.slowerRotateButton_Click);
            // 
            // startStopButton
            // 
            this.startStopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startStopButton.Location = new System.Drawing.Point(705, 483);
            this.startStopButton.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(114, 68);
            this.startStopButton.TabIndex = 3;
            this.startStopButton.Text = "Старт (Клавиша Пробел)";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // invertZButton
            // 
            this.invertZButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.invertZButton.Location = new System.Drawing.Point(479, 483);
            this.invertZButton.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.invertZButton.Name = "invertZButton";
            this.invertZButton.Size = new System.Drawing.Size(191, 68);
            this.invertZButton.TabIndex = 4;
            this.invertZButton.Text = "Инвертировать по оси Z  (Клавиша Z)";
            this.invertZButton.UseVisualStyleBackColor = true;
            this.invertZButton.Click += new System.EventHandler(this.InvertZButton_Click);
            // 
            // invertYButton
            // 
            this.invertYButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.invertYButton.Location = new System.Drawing.Point(250, 483);
            this.invertYButton.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.invertYButton.Name = "invertYButton";
            this.invertYButton.Size = new System.Drawing.Size(189, 68);
            this.invertYButton.TabIndex = 5;
            this.invertYButton.Text = "Инвертировать по оси Y (Клавиша Y)";
            this.invertYButton.UseVisualStyleBackColor = true;
            this.invertYButton.Click += new System.EventHandler(this.InvertYButton_Click);
            // 
            // invertXButton
            // 
            this.invertXButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.invertXButton.Location = new System.Drawing.Point(20, 483);
            this.invertXButton.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.invertXButton.Name = "invertXButton";
            this.invertXButton.Size = new System.Drawing.Size(190, 68);
            this.invertXButton.TabIndex = 6;
            this.invertXButton.Text = "Инвертировать по оси X (Клавиша X)";
            this.invertXButton.UseVisualStyleBackColor = true;
            this.invertXButton.Click += new System.EventHandler(this.InvertXButton_Click);
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
        private Button invertZButton;
        private Button invertYButton;
        private Button invertXButton;
    }
}

