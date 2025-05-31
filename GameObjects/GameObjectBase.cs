using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    /// <summary>
    /// 游戏中所有Object的基类。
    /// </summary>
    internal abstract class GameObjectBase
    {
        /// <summary>
        /// 此对象的X轴。
        /// </summary>
        public virtual float X { get; set; }
        /// <summary>
        /// 此对象的Y轴。
        /// </summary>
        public virtual float Y { get; set; }
        /// <summary>
        /// 此对象所在的位置。
        /// </summary>
        public Vector2 Position
        {
            get => new Vector2(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
    }
}
