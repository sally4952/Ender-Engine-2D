using EnderEngine2D.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    /// <summary>
    /// 表示一个Level。
    /// </summary>
    internal struct Level
    {
        /// <summary>
        /// 确定这个Level中有哪些Object。
        /// </summary>
        public Dictionary<string, GameObjectBase> Objects;
        /// <summary>
        /// 确定这个Level的背景颜色是什么。
        /// </summary>
        public Color BackgroundColor;
        /// <summary>
        /// 获取当前正在使用的Level。
        /// </summary>
        internal static Level Now = new Level();

        /// <summary>
        /// 初始化一个Level结构体。
        /// </summary>
        /// <param name="name">第一个加入Level的Object在此Level中的名称。</param>
        /// <param name="obj">第一个加入Level的Object。</param>
        public Level(string name, GameObjectBase obj)
        {
            Objects = new Dictionary<string, GameObjectBase>();
            Objects.Add(name, obj);
            BackgroundColor = Color.Black;
        }

        /// <summary>
        /// 初始化一个Level结构体，并确定这个Level的背景颜色。
        /// </summary>
        /// <param name="name">第一个加入Level的Object在此Level中的名称。</param>
        /// <param name="obj">第一个加入Level的Object。</param>
        /// <param name="bgColor">这个Level的背景颜色。</param>
        public Level(string name, GameObjectBase obj, Color bgColor) : this(name, obj)
        {
            BackgroundColor = bgColor;
        }

        /// <summary>
        /// 加载一个Level。
        /// </summary>
        /// <param name="level">要加载进游戏的Level。</param>
        public static void LoadLevel(Level level)
        {
            var count = 0;
            Now = level;
            foreach (var obj in level.Objects)
            {
                var type = obj.Value.GetType();
                var attr = type.GetCustomAttribute<RealGameObjectAttribute>();
                if (attr == null)
                {
                    continue;
                }
                switch (attr.JoinType)
                {
                    case JoinGameAs.RigidBody:
                        Program.PhysicalEngine.RigidBodies.Add(count, obj.Value.RigidBody);
                        break;
                    case JoinGameAs.StaticBody:
                        Program.PhysicalEngine.StaticBodies.Add(count, obj.Value.StaticBody);
                        break;
                }
                obj.Value.OnLoadingLevel(level);
                count++;
            }
            Task.Run(() =>
            {
                var key = 0;
                VisibleObjects.Objects = new IDrawable[level.Objects.Count];
                foreach (var name in level.Objects.Keys)
                {
                    VisibleObjects.Objects[key] = level.Objects[name];
                    key++;
                }
            });
        }
    }
}
