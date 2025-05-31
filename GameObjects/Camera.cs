using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    /// <summary>
    /// 游戏中唯一的相机。
    /// </summary>
    internal static class Camera
    {
        /// <summary>
        /// 相机所在的X轴。
        /// </summary>
        public static float X = 10;
        /// <summary>
        /// 相机所在的Y轴。
        /// </summary>
        public static float Y = 10;
        /// <summary>
        /// 相机与2D平面的距离（目前没有任何作用）。
        /// </summary>
        public static float Deepth = 1;
    }
}
