#pragma warning disable CS8500

using EnderEngine2D.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Attributes
{
    /// <summary>
    /// 确定这个Object是作为什么加入物理引擎的。
    /// </summary>
    enum JoinGameAs
    {
        /// <summary>
        /// 不加入物理引擎。
        /// </summary>
        None,
        /// <summary>
        /// 作为刚体加入。
        /// </summary>
        RigidBody,
        /// <summary>
        /// 作为静态体加入。
        /// </summary>
        StaticBody,
    }

    /// <summary>
    /// 如果这个Object要继承GameObjectBase来加入游戏，那么必须添加此特性，不然不会加入游戏。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class RealGameObjectAttribute : Attribute
    {
        /// <summary>
        /// 确定这个Object以何种方式加入物理引擎。
        /// </summary>
        public JoinGameAs JoinType { get; }
        /// <summary>
        /// 确定这个Object默认加入哪个Level。
        /// </summary>
        public unsafe Level* DefaultLevel { get; }
        /// <summary>
        /// 确定这个Object在加入Level时的名称。
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 这个特性的必须的构造方法。
        /// </summary>
        /// <param name="joinType">确定这个Object以何种方式加入物理引擎。</param>
        public RealGameObjectAttribute(JoinGameAs joinType) { JoinType = joinType; }
        public unsafe RealGameObjectAttribute(JoinGameAs joinType, Level* defaultLevel, string name) : this(joinType) { DefaultLevel = defaultLevel; Name = name; }
    }
}
