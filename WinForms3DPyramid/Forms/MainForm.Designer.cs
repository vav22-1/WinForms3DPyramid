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
            this.DrawPyramidPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // DrawPyramidPanel
            // 
            this.DrawPyramidPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DrawPyramidPanel.Location = new System.Drawing.Point(200, 30);
            this.DrawPyramidPanel.Name = "DrawPyramidPanel";
            this.DrawPyramidPanel.Size = new System.Drawing.Size(600, 400);
            this.DrawPyramidPanel.TabIndex = 0;
            this.DrawPyramidPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPyramidPanel_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.DrawPyramidPanel);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel DrawPyramidPanel;
    }
}

