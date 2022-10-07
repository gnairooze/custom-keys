using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomKeys
{
    internal class KeyConfig
    {
        public string Id { get; set; }
        public string Display { get; set; }
        public string KeyPress { get; set; }
        public int BackColor { get; set; }
        public int ForeColor { get; set; }
        public int HoverColor { get; set; }
        public string Tooltip { get; set; }

        public KeyConfig(string id, string display, string keyPress, int backcolor, int forecolor, int hovercolor, string tooltip)
        {
            Id = id;
            Display = display;
            KeyPress = keyPress;
            BackColor = backcolor;
            ForeColor = forecolor;
            HoverColor = hovercolor;
            Tooltip = tooltip;
        }
    }
}
