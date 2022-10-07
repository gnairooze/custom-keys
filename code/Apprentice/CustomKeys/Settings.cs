using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace CustomKeys
{
    internal partial class Settings : Form
    {
        private const string HELP_FILE = "help.txt";
        public Form RelatedForm { get; set; }
        private readonly Config _Config;

        public Settings(Form relatedForm, Config config)
        {
            InitializeComponent();
            RelatedForm = relatedForm;
            _Config = config;
            CreateKeyConfigsControls();
        }

        private void CreateKeyConfigsControls()
        {
            int padTop = 40;
            int height = 25;
            int col1 = 20;
            int col2 = 80;
            int col3 = 130;
            int col4 = 250;
            int col5 = 312;
            int col6 = 440;
            int col7 = 520;

            for (int i = 1; i <= 30; i++)
            {
                var config = _Config.KeyConfigs.Where(c => c.Id == $"button{i}").FirstOrDefault();

                if (config == null || config.Id == String.Empty)
                {
                    config = new KeyConfig($"button{i}", $"{i}", string.Empty, Color.LightGray.ToArgb());

                    _Config.KeyConfigs.Add(config);
                }

                //add key label
                this.Controls.Add(new Label()
                {
                    Top = padTop + (i * height),
                    Left = col1,
                    Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point),
                    Text = $"Key {i}",
                    AutoSize = true
                });

                //add display label
                this.Controls.Add(new Label()
                {
                    Top = padTop + (i * height),
                    Left = col2,
                    Text = "Dislpay",
                    AutoSize = true
                });

                //add display text
                var txtDisplay = new TextBox()
                {
                    Top = padTop + (i * height) - 4,
                    Left = col3,
                    Name = $"txtDisplay{i}",
                    MaxLength = 6,
                    Text = config.Display,
                    Tag = config
                };
                txtDisplay.TextChanged += DisplayTextChanged;
                this.Controls.Add(txtDisplay);

                //add key press label
                this.Controls.Add(new Label()
                {
                    Top = padTop + (i * height),
                    Left = col4,
                    Text = "Key Press",
                    AutoSize = true
                });

                //add key press text
                var txtKeyPress = new TextBox()
                {
                    Top = padTop + (i * height) - 4,
                    Left = col5,
                    Name = $"txtKeyPress{i}",
                    Text = config.KeyPress,
                    Tag = config
                };
                txtKeyPress.TextChanged += KeyPressTextChanged;
                this.Controls.Add(txtKeyPress);

                //add color button
                var btnColor = new Button()
                { 
                    Text = "Color",
                    Top = padTop + (i * height) - 4,
                    Left = col6,
                    Name = $"btnColor{i}",
                    Tag = config,
                    BackColor = Color.FromArgb(config.Color)
                };
                btnColor.Click += BtnColor_Click;
                this.Controls.Add(btnColor);
            }
        }

        private void BtnColor_Click(object? sender, EventArgs e)
        {
            if (sender == null) return;

            var btn = (Button)sender;
            if (btn.Tag is not KeyConfig config) return;

            ColorDialog dialog = new ()
            {
                ShowHelp = true,
                Color = Color.LightGray
            };

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                config.Color = dialog.Color.ToArgb();
                btn.BackColor = dialog.Color;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyConfig();
        }

        private void ApplyConfig()
        {
            chkTop.Checked = _Config.TopMost;

            if ((decimal)(_Config.Opacity * 100) >= numOpacity.Minimum
                && (decimal)(_Config.Opacity * 100) <= numOpacity.Maximum)
            {
                numOpacity.Value = (decimal)_Config.Opacity * 100;
            }
        }

        private void TopmostCheckedChanged(object sender, EventArgs e)
        {
            RelatedForm.TopMost = chkTop.Checked;
            _Config.TopMost = RelatedForm.TopMost;
        }

        private void DisplayTextChanged(object? sender, EventArgs e)
        {
            if (sender == null) return;

            var txt = (TextBox)sender;

            if (txt.Tag is not KeyConfig config) return;
            config.Display = txt.Text;
        }

        private void KeyPressTextChanged(object? sender, EventArgs e)
        {
            if (sender == null) return;

            var txt = (TextBox)sender;

            if (txt.Tag is not KeyConfig config) return;
            config.KeyPress = txt.Text;
        }

        private void OpacityValueChanged(object sender, EventArgs e)
        {
            RelatedForm.Opacity = (double)numOpacity.Value / 100;
            _Config.Opacity = RelatedForm.Opacity;
        }

        private void Help_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", Path.Combine(Environment.CurrentDirectory,HELP_FILE));
        }
    }
}
