using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Attributes
{
    /// <summary>
    /// 表示一个属性或字段可以从屏幕坐标转换成归一化设备坐标（或者反过来）。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    sealed class AbleToNdcAttribute : Attribute
    {
        /// <summary>
        /// 被标记的属性或字段的类型。
        /// </summary>
        public TypeCode ValueType { get; }
        /// <summary>
        /// 确定转换时的方向是纵向还是横向。
        /// </summary>
        public DirectionType Direction { get; }
        /// <summary>
        /// 初始化AbleToNdcAttribute特性。
        /// </summary>
        /// <param name="valueType">确定这个属性或字段的类型。</param>
        /// <param name="direction">转换时的方向（纵向或横向）。</param>
        public AbleToNdcAttribute(TypeCode valueType, DirectionType direction)
        {
            ValueType = valueType;
            Direction = direction;
        }
    }
}
