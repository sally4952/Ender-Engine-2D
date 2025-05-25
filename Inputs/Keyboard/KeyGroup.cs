using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D.Inputs.Keyboard
{
    internal class KeyGroup
    {
        private Dictionary<Keys, bool> mBooleansValue = new Dictionary<Keys, bool>();

        public KeyGroup()
        {

        }

        public bool this[Keys keys]
        {
            get
            {
                if (mBooleansValue.TryGetValue(keys, out var value))
                {
                    return value;
                }
                return false;
            }
            set
            {
                if (mBooleansValue.ContainsKey(keys))
                {
                    mBooleansValue[keys] = value;
                }
                else
                {
                    mBooleansValue.Add(keys, value);
                }
            }
        }
    }
}
