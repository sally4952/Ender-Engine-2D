#pragma warning disable CS8500

using EnderEngine2D.Physics;
using EnderEngine2D.Attributes;
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
        /// <summary>
        /// 此GameObject默认的刚体属性。
        /// </summary>
        public virtual RigidBody RigidBody { get; set; }
        /// <summary>
        /// 此GameObject默认的静态体属性。
        /// </summary>
        public virtual StaticBody StaticBody { get; set; }
        /// <summary>
        /// 此对象的X轴。
        /// </summary>
        [AbleToNdc(TypeCode.Single, DirectionType.X)]
        public virtual float X { get; set; }
        /// <summary>
        /// 此对象的Y轴。
        /// </summary>
        [AbleToNdc(TypeCode.Single, DirectionType.Y)]
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
        /// <summary>
        /// 定义当需要绘制Object是需要执行什么操作。
        /// </summary>
        /// <param name="g">要绘制的Graphics。</param>
        void IDrawable.Draw(Graphics g)
        {
        }
        /// <summary>
        /// 当游戏开始运行时需要执行的方法。
        /// </summary>
        public virtual void Start()
        {
        }
        /// <summary>
        /// 当游戏运行时需要执行的方法。
        /// </summary>
        public virtual void Update()
        {
        }
        /// <summary>
        /// 当进入某个Level时要执行的方法。
        /// </summary>
        /// <param name="level"></param>
        public virtual void OnLoadingLevel(in Level level)
        {
        }
        /// <summary>
        /// 确定这个GameObject是否正在使用归一化设备坐标。
        /// </summary>
        public bool UseNdc { get; set; }
        /// <summary>
        /// 将所有被被标记的AbleToNdcAttribute特性的属性和字段转换为归一化设备坐标并将UseNdc属性设置为true。
        /// </summary>
        public void ToNdc()
        {
            if (UseNdc)
            {
#if DEBUG
                throw new InvalidOperationException("此组件的坐标已为归一化设备坐标。");
#else
                return;
#endif
            }
            foreach (var value in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attr = value.GetCustomAttribute<AbleToNdcAttribute>();
                if (attr != null)
                {
                    if (attr.ValueType == TypeCode.Single)
                    {
                        value.GetSetMethod()?.Invoke(this, new object[] { GameScreenConvert.ScreenToPercentage((float?)value.GetGetMethod()?.Invoke(this, new object[0]), attr.Direction) });
                    }
                }
            }
            foreach (var value in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attr = value.GetCustomAttribute<AbleToNdcAttribute>();
                if (attr != null)
                {
                    if (attr.ValueType == TypeCode.Single)
                    {
                        value.SetValue(this, GameScreenConvert.ScreenToPercentage((float?)value.GetValue(this), attr.Direction));
                    }
                }
            }
            UseNdc = true;
        }
        /// <summary>
        /// 将所有被被标记的AbleToNdcAttribute特性的属性和字段转换为屏幕坐标并将UseNdc属性设置为false。
        /// </summary>
        public void ToSc()
        {
            if (!UseNdc)
            {
#if DEBUG
                throw new InvalidOperationException("此组件的坐标已为屏幕坐标。");
#else
                return;
#endif
            }
            foreach (var value in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attr = value.GetCustomAttribute<AbleToNdcAttribute>();
                if (attr != null)
                {
                    if (attr.ValueType == TypeCode.Single)
                    {
                        value.GetSetMethod()?.Invoke(this, new object[] { GameScreenConvert.PercentageToScreen((float?)value.GetGetMethod()?.Invoke(this, new object[0]), attr.Direction) });
                    }
                }
            }
            foreach (var value in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attr = value.GetCustomAttribute<AbleToNdcAttribute>();
                if (attr != null)
                {
                    if (attr.ValueType == TypeCode.Single)
                    {
                        value.SetValue(this, GameScreenConvert.PercentageToScreen((float?)value.GetValue(this), attr.Direction));
                    }
                }
            }
            UseNdc = false;
        }
        /// <summary>
        /// 引擎内部使用的方法，用于初始化所有继承于GameObjectBase的Object。
        /// </summary>
        [Obsolete("如果您使用Level系统，请改用GameObjectBase.Init(Level)。")]
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

        /// <summary>
        /// 引擎内部使用的方法，用于通过指定Level来初始化游戏。
        /// </summary>
        /// <param name="level"></param>
        public static void Init(Level level)
        {
            var count = 0;
            Level.Now = level;
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
                obj.Value.Start();
                count++;
            }
            Task.Run(async () =>
            {
                var key = 0;
                VisibleObjects.Objects = new IDrawable[level.Objects.Count];
                foreach (var name in level.Objects.Keys)
                {
                    VisibleObjects.Objects[key] = level.Objects[name];
                    key++;
                }

                while (true)
                {
                    foreach (var obj in Level.Now.Objects)
                    {
                        obj.Value.Update();
                    }
                    await Task.Delay(2);
                }
            });
        }
    }
}
