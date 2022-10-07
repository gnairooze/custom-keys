using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text.Json.Serialization;

namespace CustomKeys
{
    public partial class MainForm : Form
    {
        private const string CONFIG_FILE = "custom-keys.json";
        private const int OPACITY_MIN = 25;
        private const int OPACITY_MAX = 100;

        private Config _Config = new();

        public Trace? TraceForm { get; set; }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x08000000;

                return cp;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Text = $"Custom Keys - {GetVersion()}";
            if (_Config.Trace)
            {
                this.TraceForm = new Trace();
                this.TraceForm.Show();
            }
        }

        private string GetVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion != null?fvi.FileVersion:string.Empty;
        }

        private void LoadConfig()
        {
            string json = File.ReadAllText(CONFIG_FILE, System.Text.Encoding.UTF8);

            var config = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(json);

            if (config != null) _Config = config;
        }

        private void ApplyConfig()
        {
            ApplyGeneralSettings();
            ApplyKeySettings();
        }

        private void ApplyKeySettings()
        {
            foreach (var keyConfig in _Config.KeyConfigs)
            {
                //skip if config object is empty
                if (keyConfig.Id == String.Empty || keyConfig.Display == String.Empty) continue;

                //skip if config Id not found in form controls
                var controls = this.Controls.Find(keyConfig.Id, false);
                if (controls == null || controls.Length == 0) continue;

                //skip if found control is not a button
                var control = this.Controls.Find(keyConfig.Id, false).First();
                if (control.GetType() != typeof(Button)) continue;

                Button btn = (Button)control;

                btn.Text = keyConfig.Display;
                btn.Tag = keyConfig.KeyPress;
                btn.BackColor = Color.FromArgb(keyConfig.BackColor);
                btn.ForeColor = Color.FromArgb(keyConfig.ForeColor);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(keyConfig.HoverColor);

                toolTipKeys.SetToolTip(btn, keyConfig.Tooltip);
            }
        }

        private void ApplyGeneralSettings()
        {
            this.TopMost = _Config.TopMost;
            if (_Config.Opacity * 100 >= OPACITY_MIN && _Config.Opacity * 100 <= OPACITY_MAX)
            {
                this.Opacity = _Config.Opacity;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            CreateKeyControls();
            LoadConfig();
            ApplyConfig();
        }

        private void CreateKeyControls()
        {
            int counter = 0;
            for (int i = 1; i <= 10; i++)
            {
                counter++;
                int x = 12 + ((i - 1) * 60);
                int y = 40;
                CreateKeyButton(x, y, $"button{counter}", 33 + counter, $"{counter}");
                CreateKeyLabel(x + 10, y + 26, $"label{i}", $"Key {i}");
            }

            for (int i = 1; i <= 10; i++)
            {
                counter++;
                int x = 12 + ((i - 1) * 60);
                int y = 90;
                CreateKeyButton(x, y, $"button{counter}", 33 + counter, $"{counter}");
                CreateKeyLabel(x + 10, y + 26, $"label{counter}", $"Key {counter}");
            }

            for (int i = 1; i <= 10; i++)
            {
                counter++;
                int x = 12 + ((i - 1) * 60);
                int y = 140;
                CreateKeyButton(x, y, $"button{counter}", 33 + counter, $"{counter}");
                CreateKeyLabel(x + 10, y + 26, $"label{counter}", $"Key {counter}");
            }
        }

        private Button CreateKeyButton(int x, int y, string name, int index, string text)
        {
            Button btn = new()
            {
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point),

                Location = new System.Drawing.Point(x, y),
                Name = name,
                Size = new System.Drawing.Size(57, 23),
                TabIndex = index,
                Text = text,
                UseVisualStyleBackColor = true,
                FlatStyle = FlatStyle.Flat
            };
            btn.Click += new System.EventHandler(this.Key_Click);

            this.Controls.Add(btn);

            return btn;
        }

        private Label CreateKeyLabel(int x, int y, string name, string text)
        {
            Label lbl = new()
            {
                AutoSize = true,
                Location = new System.Drawing.Point(x, y),
                Name = name,
                Text = text
            };

            this.Controls.Add(lbl);

            return lbl;
        }

        private void Key_Click(object? sender, EventArgs e)
        {
            if (sender == null) return;
            var btn = (Button)sender;

            if (btn.Tag == null || btn.Tag.ToString() == string.Empty) return;

            SendKeys.Send(btn.Tag.ToString());
        }

        private void SaveConfig()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(_Config);
            File.Copy(CONFIG_FILE, $"{CONFIG_FILE}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}");
            File.WriteAllText(CONFIG_FILE, json, System.Text.Encoding.UTF8);
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Settings form = new(this, _Config);
            form.TopMost = true;
            form.ShowDialog();

            SaveConfig();
            ApplyKeySettings();
        }

        private void toolTipKeys_Draw(object sender, DrawToolTipEventArgs e)
        {
            if (this.TraceForm == null) return;

            this.TraceForm.lstTrace.Items.Insert(0, $"tooltip draw - {e.ToolTipText}");
        }

        private void toolTipKeys_Popup(object sender, PopupEventArgs e)
        {
            if (this.TraceForm == null) return;

            this.TraceForm.lstTrace.Items.Insert(0, $"tooltip draw - {e.AssociatedWindow.Handle}");

        }
    }
}