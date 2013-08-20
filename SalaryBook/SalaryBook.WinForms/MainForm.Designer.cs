namespace Pellared.SalaryBook.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.wpfUserControl = new SalaryBook.WinForms.WpfUserControl();
            this.SuspendLayout();
            // 
            // wpfUserControl
            // 
            this.wpfUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpfUserControl.Element = null;
            this.wpfUserControl.Location = new System.Drawing.Point(0, 0);
            this.wpfUserControl.Name = "wpfUserControl";
            this.wpfUserControl.Size = new System.Drawing.Size(652, 525);
            this.wpfUserControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 525);
            this.Controls.Add(this.wpfUserControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Książka zarobków";
            this.ResumeLayout(false);

        }

        #endregion

        private WpfUserControl wpfUserControl;
    }
}

