using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D.Inputs.Mouse
{
    /// <summary>
    /// 表示一个鼠标输入的监听器。
    /// </summary>
    internal class MouseInput
    {
        /// <summary>
        /// 获取鼠标中键是否已经按下。
        /// </summary>
        public bool IsMiddleButtonDown { get; private set; }
        /// <summary>
        /// 获取鼠标左键是否已经按下。
        /// </summary>
        public bool IsLeftButtonDown { get; private set; }
        /// <summary>
        /// 获取鼠标右键是否已经按下。
        /// </summary>
        public bool IsRightButtonDown { get; private set; }
        /// <summary>
        /// 鼠标单击事件传入的参数。
        /// </summary>
        /// <param name="whichButtonDown">确定是哪个鼠标按键被按下。</param>
        public delegate void MouseClickEventArgs(MouseButtons whichButtonDown);
        /// <summary>
        /// 鼠标双击事件传入的参数。
        /// </summary>
        /// <param name="whichButtonDown">确定是哪个鼠标按键被按下。</param>
        public delegate void MouseDoubleClickedEventArgs(MouseButtons whichButtonDown);
        /// <summary>
        /// 当左键单击时调用的事件。
        /// </summary>
        public event MouseClickEventArgs OnLeftButtonDown;
        /// <summary>
        /// 当右键单击时调用的事件。
        /// </summary>
        public event MouseClickEventArgs OnRightButtonDown;
        /// <summary>
        /// 当中键单击时调用的事件。
        /// </summary>
        public event MouseClickEventArgs OnMiddleButtonDown;
        /// <summary>
        /// 当左键双击时调用的事件。
        /// </summary>
        public event MouseDoubleClickedEventArgs OnLeftButtonDoubleClicked;
        /// <summary>
        /// 当右键双击时调用的事件。
        /// </summary>
        public event MouseDoubleClickedEventArgs OnRightButtonDoubleClicked;
        /// <summary>
        /// 当中键双击时调用的事件。
        /// </summary>
        public event MouseDoubleClickedEventArgs OnMiddleButtonDoubleClicked;
        private Control mListeningControl;
        /// <summary>
        /// 初始化一个鼠标监听器。
        /// </summary>
        /// <param name="listeningControl">要监听的控件。</param>
        public MouseInput(Control listeningControl)
        {
            mListeningControl = listeningControl;
            mListeningControl.MouseClick += ListeningControl_MouseClick;
            mListeningControl.MouseDoubleClick += MListeningControl_MouseDoubleClick;
            mListeningControl.MouseDown += MListeningControl_MouseDown;
            mListeningControl.MouseUp += MListeningControl_MouseUp;
        }

        private void MListeningControl_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    IsLeftButtonDown = false; break;
                case MouseButtons.Right:
                    IsRightButtonDown = false; break;
                case MouseButtons.Middle:
                    IsMiddleButtonDown = false; break;
            }
        }

        private void MListeningControl_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    IsLeftButtonDown = true; break;
                case MouseButtons.Right:
                    IsRightButtonDown = true; break;
                case MouseButtons.Middle:
                    IsMiddleButtonDown = true; break;
            }
        }

        private void MListeningControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    OnLeftButtonDoubleClicked?.Invoke(e.Button);
                    break;
                case MouseButtons.Right:
                    OnRightButtonDoubleClicked?.Invoke(e.Button);
                    break;
                case MouseButtons.Middle:
                    OnMiddleButtonDoubleClicked?.Invoke(e.Button);
                    break;
            }
        }

        private void ListeningControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        OnLeftButtonDown?.Invoke(e.Button);
                        break;
                    case MouseButtons.Right:
                        OnRightButtonDown?.Invoke(e.Button);
                        break;
                    case MouseButtons.Middle:
                        OnMiddleButtonDown?.Invoke(e.Button);
                        break;
                }
            }
        }
    }
}
