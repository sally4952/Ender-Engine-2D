using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.UI
{
    /// <summary>
    /// 所有UI控件的基类。
    /// </summary>
    internal abstract class UIBase
    {
        /// <summary>
        /// （无意义）确定有哪些可见控件。
        /// </summary>
        public static List<UIBase> VisibleComponents;
        /// <summary>
        /// （未实现）UI控件的显示层级。
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// （未实现）UI控件的是否可见。
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// UI控件的X轴位置。
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// UI控件的Y轴位置。
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// UI控件默认构造方法。
        /// </summary>
        /// <param name="index">（未实现）UI控件的显示层级。</param>
        /// <param name="x">UI控件的X轴位置。</param>
        /// <param name="y">UI控件的Y轴位置。</param>
        public UIBase(int index, float x, float y)
        {
            Index = index;
            Visible = true;
            X = x; 
            Y = y;
            VisibleComponents = new List<UIBase>();
        }
        public virtual void Draw(Graphics g)
        {
        }
    }
}
