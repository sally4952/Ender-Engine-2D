#pragma warning disable CS8500

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D.Inputs.Keyboard
{
    internal class Keyboard : IDisposable
    {
        private unsafe Control* mListenningControl;
        private bool mIsAbandoned = false;
        private KeyGroup mKeyGroup;

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

        public bool GetKeyDown(Keys key)
        {
            if (mIsAbandoned)
            {
                throw new InvalidOperationException("此Keyboard已废弃。");
            }
            return mKeyGroup[key];
        }

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
            if (*mListenningControl != null)
            {
                mListenningControl->KeyDown -= KeyDownListenningEvent;
                mListenningControl->KeyUp -= KeyUpListernningEvent;
                mListenningControl = null;
            }
            mIsAbandoned = true;
        }
    }
}
