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
            this.drawPyramidPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // drawPyramidPanel
            // 
            this.drawPyramidPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawPyramidPanel.Location = new System.Drawing.Point(12, 12);
            this.drawPyramidPanel.Name = "drawPyramidPanel";
            this.drawPyramidPanel.Size = new System.Drawing.Size(810, 470);
            this.drawPyramidPanel.TabIndex = 0;
            this.drawPyramidPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPyramidPanel_Paint);
            this.drawPyramidPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawPyramidPanel_MouseDown);
            this.drawPyramidPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawPyramidPanel_MouseMove);
            this.drawPyramidPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawPyramidPanel_MouseUp);
            this.drawPyramidPanel.Resize += new System.EventHandler(this.DrawPyramidPanel_Resize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 561);
            this.Controls.Add(this.drawPyramidPanel);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel drawPyramidPanel;
    }
}

