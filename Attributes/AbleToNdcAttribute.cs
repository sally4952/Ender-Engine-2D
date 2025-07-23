using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    sealed class AbleToNdcAttribute : Attribute
    {
        public TypeCode ValueType { get; }
        public DirectionType Direction { get; }
        public AbleToNdcAttribute(TypeCode valueType, DirectionType direction)
        {
            ValueType = valueType;
            Direction = direction;
        }
    }
}
