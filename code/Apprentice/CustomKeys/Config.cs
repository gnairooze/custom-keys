using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomKeys
{
    internal class Config
    {
        public bool TopMost { get; set; }
        public double Opacity { get; set; }
        public bool Trace { get; set; }

        public List<KeyConfig> KeyConfigs { get; private set; }

        public Config()
        {
            KeyConfigs = new List<KeyConfig>();
        }
    }
}
