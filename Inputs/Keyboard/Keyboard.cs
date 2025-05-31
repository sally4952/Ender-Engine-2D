#pragma warning disable CS8500

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D.Inputs.Keyboard
{
    /// <summary>
    /// 获取关于键盘的输入。
    /// </summary>
    internal class Keyboard : IDisposable
    {
        /// <summary>
        /// 需要监听的控件。
        /// </summary>
        private unsafe Control* mListenningControl;
        /// <summary>
        /// 是否已被废弃。
        /// </summary>
        private bool mIsAbandoned = false;
        /// <summary>
        /// 键盘输入的信息。
        /// </summary>
        private KeyGroup mKeyGroup;
        /// <summary>
        /// 初始化一个键盘输入监听类。
        /// </summary>
        /// <param name="control"></param>
        public unsafe Keyboard(Control* control)
        {
            mListenningControl = control;
            control->KeyDown += KeyDownListenningEvent;
            control->KeyUp += KeyUpListernningEvent;
            mKeyGroup = new KeyGroup();
        }

        private void KeyUpListernningEvent(object sender, KeyEventArgs e)
        {
            mKeyGroup[e.KeyCode] = false;
        }

        private void KeyDownListenningEvent(object sender, KeyEventArgs e)
        {
            mKeyGroup[e.KeyCode] = true;
        }
        /// <summary>
        /// 获取某个按键是否被按下。
        /// </summary>
        /// <param name="key">检测的目标按键。</param>
        /// <returns>如果已按下，则为true；否则为false。</returns>
        /// <exception cref="InvalidOperationException">当此类已废弃时，抛出异常。</exception>
        public bool GetKeyDown(Keys key)
        {
            if (mIsAbandoned)
            {
                throw new InvalidOperationException("此Keyboard已废弃。");
            }
            return mKeyGroup[key];
        }
        /// <summary>
        /// 获取某个按键是否抬起。
        /// </summary>
        /// <param name="key">检测的目标按键。</param>
        /// <returns>如果已按抬起，则为true；否则为false。</returns>
        /// <exception cref="InvalidOperationException">当此类已废弃时，抛出异常。</exception>
        public bool GetKeyUp(Keys key)
        {
            if (mIsAbandoned)
            {
                throw new InvalidOperationException("此Keyboard已废弃。");
            }
            return mKeyGroup[key];
        }

        unsafe void IDisposable.Dispose()
        {
            mListenningControl->KeyDown -= KeyDownListenningEvent;
            mListenningControl->KeyUp -= KeyUpListernningEvent;
            mListenningControl = null;
            mIsAbandoned = true;
        }
    }
}
