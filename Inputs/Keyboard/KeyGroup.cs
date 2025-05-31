using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D.Inputs.Keyboard
{
    /// <summary>
    /// 键盘监听到的信息。
    /// </summary>
    internal class KeyGroup
    {
        private Dictionary<Keys, bool> mBooleansValue = new Dictionary<Keys, bool>();

        public KeyGroup()
        {

        }
        /// <summary>
        /// 获取或设置某个键的状态。
        /// </summary>
        /// <param name="keys">目标按键。</param>
        /// <returns>是否已被按下。</returns>
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
