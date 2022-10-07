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

        public KeyConfig(string id, string display, string keyPress)
        {
            Id = id;
            Display = display;
            KeyPress = keyPress;
        }
    }
}
