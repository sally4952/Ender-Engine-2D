#pragma warning disable CS8500

using EnderEngine2D.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    /// <summary>
    /// 游戏中所有Object的基类。
    /// </summary>
    internal abstract class GameObjectBase : IDrawable
    {
        public virtual RigidBody RigidBody { get; set; }
        public virtual StaticBody StaticBody { get; set; }
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

        void IDrawable.Draw(System.Drawing.Graphics g)
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public static void Init()
        {
            var objects = new List<GameObjectBase>();
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var key = 0;
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(GameObjectBase)))
                {
                    var attr = (RealGameObjectAttribute)type.GetCustomAttribute(typeof(RealGameObjectAttribute));
                    if (attr == null)
                    {
                        continue;
                    }
                    var gob = (GameObjectBase)Activator.CreateInstance(type);
                    objects.Add(gob);
                    switch (attr.JoinType)
                    {
                        case JoinGameAs.RigidBody:
                            Program.PhysicalEngine.RigidBodies.Add(key, gob.RigidBody);
                            break;
                        case JoinGameAs.StaticBody:
                            Program.PhysicalEngine.StaticBodies.Add(key, gob.StaticBody);
                            break;
                    }
                    gob.Start();
                    key++;
                }
            }
            Task.Run(async () =>
            {
                VisibleObjects.Objects = new IDrawable[objects.Count];
                for (var i = 0; i < objects.Count; i++)
                {
                    var obj = objects[i];
                    VisibleObjects.Objects[i] = obj;
                }
                
                while (true)
                {
                    foreach (var obj in objects)
                    {
                        obj.Update();
                    }
                    await Task.Delay(10);
                }
            });
        }
    }
}
