namespace CustomKeys
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnSettings = new System.Windows.Forms.Button();
            this.toolTipKeys = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(553, 12);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(60, 23);
            this.btnSettings.TabIndex = 33;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // toolTipKeys
            // 
            this.toolTipKeys.ShowAlways = true;
            this.toolTipKeys.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTipKeys_Draw);
            this.toolTipKeys.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTipKeys_Popup);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 212);
            this.Controls.Add(this.btnSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Custom Keys - 1.0";
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnSettings;
        private ToolTip toolTipKeys;
    }
}