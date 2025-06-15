using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.UI
{
    internal struct UIContainer
    {
        public static bool IsGaming { get; set; }
        public static UIContainer[] Now { get; set; }
        public int Index { get; set; }
        public Dictionary<string, UIBase> Components;
        public Color BackgroundColor { get; set; }
        public UIContainer(int index, Color bgColor)
        {
            Index = index;
            Components = new Dictionary<string, UIBase>();
            BackgroundColor = bgColor;
        }
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
