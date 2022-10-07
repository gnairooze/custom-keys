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
        public MainForm RelatedForm { get; set; }
        private readonly Config _Config;

        public Settings(MainForm relatedForm, Config config)
        {
            InitializeComponent();
            this.Width = 890;
            this.Height = 870;

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
            int col8 = 600;
            int col9 = 700;
            int col10 = 750;

            for (int i = 1; i <= 30; i++)
            {
                var config = _Config.KeyConfigs.Where(c => c.Id == $"button{i}").FirstOrDefault();

                if (config == null || config.Id == String.Empty)
                {
                    config = new KeyConfig($"button{i}", $"{i}", string.Empty, Color.LightGray.ToArgb(), Color.Black.ToArgb(), Color.DarkGray.ToArgb(), string.Empty);

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

                //add back color button
                var btnBackColor = CreateButton("BackColor", padTop + (i * height) - 4, col6, $"btnBackColor{i}", config, Color.FromArgb(config.BackColor), Color.Black, Color.DarkGray);
                btnBackColor.Click += BackColor_Click;

                //add fore color button
                var btnForeColor = CreateButton("ForeColor", padTop + (i * height) - 4, col7, $"btnForeColor{i}", config, Color.FromArgb(config.ForeColor), Color.White, Color.DarkGray);
                btnForeColor.Click += ForeColor_Click;

                //add hover color button
                var btnHoverColor = CreateButton("HoverColor", padTop + (i * height) - 4, col8, $"btnHoverColor{i}", config, Color.FromArgb(config.HoverColor), Color.White, Color.Black);
                btnHoverColor.Click += HoverColor_Click;

                //add tooltip label
                this.Controls.Add(new Label()
                {
                    Top = padTop + (i * height),
                    Left = col9,
                    Text = "ToolTip",
                    AutoSize = true
                });


                //add tooltip text
                var txtTooltip = new TextBox()
                {
                    Top = padTop + (i * height) - 4,
                    Left = col10,
                    Name = $"txtTooltip{i}",
                    Text = config.Tooltip,
                    Tag = config
                };
                txtTooltip.TextChanged += TooltipTextChanged;
                this.Controls.Add(txtTooltip);
            }
        }

        private Button CreateButton(string text, int top, int left, string name, object tag, Color backcolor, Color forecolor, Color hovercolor)
        {
            var btn = new Button()
            {
                AutoSize = true,
                Text = text,
                Top = top,
                Left = left,
                Name = name,
                Tag = tag,
                BackColor = backcolor,
                ForeColor = forecolor,
                FlatStyle = FlatStyle.Flat
            };
            
            btn.FlatAppearance.MouseOverBackColor = hovercolor;

            this.Controls.Add(btn);
            return btn;
        }
        private void BackColor_Click(object? sender, EventArgs e)
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
                config.BackColor = dialog.Color.ToArgb();
                btn.BackColor = dialog.Color;
            }
        }
        private void ForeColor_Click(object? sender, EventArgs e)
        {
            if (sender == null) return;

            var btn = (Button)sender;
            if (btn.Tag is not KeyConfig config) return;

            ColorDialog dialog = new()
            {
                ShowHelp = true,
                Color = Color.Black
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                config.ForeColor = dialog.Color.ToArgb();
                btn.BackColor = dialog.Color;
            }
        }
        private void HoverColor_Click(object? sender, EventArgs e)
        {
            if (sender == null) return;

            var btn = (Button)sender;
            if (btn.Tag is not KeyConfig config) return;

            ColorDialog dialog = new()
            {
                ShowHelp = true,
                Color = Color.DarkGray
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                config.HoverColor = dialog.Color.ToArgb();
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

            chkTrace.Checked = _Config.Trace;
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

        private void TooltipTextChanged(object? sender, EventArgs e)
        {
            if (sender == null) return;

            var txt = (TextBox)sender;

            if (txt.Tag is not KeyConfig config) return;
            config.Tooltip = txt.Text;
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

        private void Trace_CheckedChanged(object sender, EventArgs e)
        {
            _Config.Trace = chkTrace.Checked;
            
            if (!chkTrace.Checked)return;
            
            _Config.Trace = chkTrace.Checked;

            RelatedForm.TraceForm = new Trace();
            RelatedForm.TraceForm.Show();
        }
    }
}
