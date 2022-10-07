namespace CustomKeys
{
    partial class Trace
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
            this.lstTrace = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstTrace
            // 
            this.lstTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTrace.FormattingEnabled = true;
            this.lstTrace.HorizontalScrollbar = true;
            this.lstTrace.ItemHeight = 15;
            this.lstTrace.Location = new System.Drawing.Point(0, 0);
            this.lstTrace.Name = "lstTrace";
            this.lstTrace.Size = new System.Drawing.Size(800, 450);
            this.lstTrace.TabIndex = 0;
            // 
            // Trace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstTrace);
            this.Name = "Trace";
            this.Text = "Trace";
            this.ResumeLayout(false);

        }

        #endregion

        internal ListBox lstTrace;
    }
}