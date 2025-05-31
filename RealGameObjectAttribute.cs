using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D
{
    enum JoinGameAs
    {
        None,
        RigidBody,
        StaticBody,
    }

    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class RealGameObjectAttribute : Attribute
    {
        public JoinGameAs JoinType { get; }
        public RealGameObjectAttribute(JoinGameAs joinType) { JoinType = joinType; }
    }
}
