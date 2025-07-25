using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.UI
{
    /// <summary>
    /// 表示一个UI控件的容器。
    /// </summary>
    internal struct UIContainer
    {
        /// <summary>
        /// 表示是否显示UI控件。
        /// </summary>
        public static bool IsGaming { get; set; }
        /// <summary>
        /// 获取正在显示的UI容器。
        /// </summary>
        public static UIContainer[] Now { get; set; }
        /// <summary>
        /// （未实现）UI容器的显示层级。
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// UI容器中包含的UI控件。
        /// </summary>
        public Dictionary<string, UIBase> Components;
        /// <summary>
        /// UI容器显示时的背景颜色。
        /// </summary>
        public Color BackgroundColor { get; set; }
        /// <summary>
        /// 初始化UI容器。
        /// </summary>
        /// <param name="index">（未实现）UI容器的显示层级。</param>
        /// <param name="bgColor">UI容器显示时的背景颜色。</param>
        public UIContainer(int index, Color bgColor)
        {
            Index = index;
            Components = new Dictionary<string, UIBase>();
            BackgroundColor = bgColor;
        }
        /// <summary>
        /// 添加UI控件。
        /// </summary>
        /// <param name="name">UI控件在容器中的唯一名称。</param>
        /// <param name="component">要添加的UI控件。</param>
        public void Add(string name, UIBase component)
        {
            if (Components.ContainsKey(name))
            {
                Components[name] = component;
            }
            else
            {
                Components.Add(name, component);
            }
        }
    }
}
