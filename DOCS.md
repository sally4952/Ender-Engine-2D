# 前言
这个游戏引擎完全使用 C# 编写，极大地降低了编写游戏的门槛。本引擎基于 .NET Framework 4.8 框架制作， C# 版本较低，因此有些高版本语法糖或者特性可能不适用。这个游戏引擎易用易上手，只要你跟着教程一步步做，一定可以成功理解这个引擎并且做出属于你自己的游戏。
# 编写你的 Hello World
## 下载并编译
请你先下载 Visual Studio 2022 并且安装好 .NET 开发环境，再从 Release 中选择合适的版本（建议最新稳定版， Pre-Release 版慎用），然后下载并解压它，双击打开 `.sln` 解决方案，并且 `Ctrl + B` 进行首次生成，此时 NuGet 会自动下载所需依赖项。一段时间过后在 `bin` 文件夹中就能看到编译好的 `.exe` 文件了。
## 正式开始编写代码
首先按下 `F5` 运行，此时你可以看到屏幕上一片漆黑，只有左下角不断跳动的帧数。按下 `Alt + F4` 关闭游戏并且返回 VS ，在 `Assets` 文件夹中新建一个文件名为 `Player.cs` ，并且输入以下代码：
```CSharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnderEngine2D.Attributes;
using EnderEngine2D.GameObjects;

namespace EnderEngine2D.Assets
{
    [RealGameObject(JoinGameAs.RigidBody)]
    internal class Player : Square
    {
        public Player() : base(
            new System.Drawing.RectangleF(
                GameScreenConvert.PercentageToScreen(0.1f, DirectionType.X), 
                GameScreenConvert.PercentageToScreen(0.04f, DirectionType.Y), 
                GameScreenConvert.PercentageToScreen(0.04f, DirectionType.X), 
                GameScreenConvert.PercentageToScreen(0.04f, DirectionType.X)), 
            System.Drawing.Color.White)
        {
        
        }
        public override void Update()
        {
            if (Program.MainForm.KeyboardInput.GetKeyDown(System.Windows.Forms.Keys.A))
            {
                this.RigidBody.Force += new System.Numerics.Vector2(-2f, 0f);
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(System.Windows.Forms.Keys.D))
            {
                this.RigidBody.Force += new System.Numerics.Vector2(2f, 0f);
            }
        }
    }
}
```
随后保存并关闭 `Player.cs` ，再创建一个文件名为 `Ground.cs` ，并且输入以下代码：
```CSharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnderEngine2D.Attributes;
using EnderEngine2D.GameObjects;

namespace EnderEngine2D.Assets
{
    [RealGameObject(JoinGameAs.StaticBody)]
    internal class Ground1 : Square
    {
        public Ground1() : base(
            new System.Drawing.RectangleF(
                GameScreenConvert.PercentageToScreen(0f, DirectionType.X), 
                GameScreenConvert.PercentageToScreen(0.95f, DirectionType.Y), 
                GameScreenConvert.PercentageToScreen(1f, DirectionType.X), 
                GameScreenConvert.PercentageToScreen(0.01f, DirectionType.Y)), 
            System.Drawing.Color.WhiteSmoke)
        {
        }
    }
}
```
随后保存并关闭。最后再在此文件夹中创建一个文件名为 `LevelController.cs` ，并且输入以下代码：
```CSharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnderEngine2D.Attributes;
using EnderEngine2D.GameObjects;

namespace EnderEngine2D.Assets
{
    internal class LevelController
    {
        private static Level _level = default;

        public static Level Level
        {
            get
            {
                if (_level.Equals(default(Level)))
                {
                    _level = CreateLevel();
                }
                return _level;
            }
        }

        private static Level CreateLevel()
        {
            var level = new Level
            {
                BackgroundColor = Color.Black,
                Objects = new Dictionary<string, GameObjectBase>
                {
                    { "PLAYER", new Player() },
                    { "GROUND", new Ground() },
               }
            };
            return level;
        }
    }
}
```
随后保存并关闭。最后打开 `Native` 中的 `Program.cs` 文件并修改 `Main` 方法中的代码：
```CSharp
static void Main(string[] args)
{
    Control.CheckForIllegalCrossThreadCalls = false;
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.ThreadException += Application_ThreadException;
    PhysicalEngine = new Physics.Engine(1.1f);
    Task.Run(async () =>
    {
        while (true)
        {
            PhysicalEngine.Update();
            await Task.Delay(1);
        }
    });
    MainForm = new MainForm();
    GameObjectBase.Init(new Level { BackgroundColor = Color.Black, Objects = new Dictionary<string, GameObjectBase>() }); // <-- 修改这一行
    UI.UIContainer.IsGaming = true;
    Application.Run(MainForm);
}

修改为：

static void Main(string[] args)
{
    Control.CheckForIllegalCrossThreadCalls = false;
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.ThreadException += Application_ThreadException;
    PhysicalEngine = new Physics.Engine(1.1f);
    Task.Run(async () =>
    {
        while (true)
        {
            PhysicalEngine.Update();
            await Task.Delay(1);
        }
    });
    MainForm = new MainForm();
    GameObjectBase.Init(LevelController.Level);  // <-- 修改这一行
    UI.UIContainer.IsGaming = true;
    Application.Run(MainForm);
}
```
最后按下 `F5` 运行，可以看到一个最基础的游戏制作出来了，按下 `A` 或 `D` 可以使小正方形向左或向右运动。

## 代码解析
我们来逐行解析一下这些代码：

`Player.cs`
```CSharp
using System;
...
using EnderEngine2D.GameObjects;
// 这上面都是引用，不需要看。

namespace EnderEngine2D.Assets
{
    [RealGameObject(JoinGameAs.RigidBody)] // 这行代码表示声明的这个Player类拥有RealGameObject特性，其中的参数表示这个类要以什么方式加入物理引擎，None表示不加入，StaticBody表示这是静态的、不受重力约束的，可以阻挡RigidBody的运动，RigidBody表示这是受到重力约束的、会被StaticBody阻挡的。这个特性是必须的，如果没有则无法加入游戏。
    internal class Player : Square // 这一行声明了Player类，它继承于Square类，表示它将显示为一个矩形，而Square继承于GameObjectBase，这是这个游戏引擎中所有类的根基，如果不直接或间接地继承于它，那么这个类将无法加入游戏。
    {
        public Player() : base(
            new System.Drawing.RectangleF(
                GameScreenConvert.PercentageToScreen(0.1f, DirectionType.X), 
                GameScreenConvert.PercentageToScreen(0.04f, DirectionType.Y), 
                GameScreenConvert.PercentageToScreen(0.04f, DirectionType.X), 
                GameScreenConvert.PercentageToScreen(0.04f, DirectionType.X)), 
            System.Drawing.Color.White) // 这一行声明了这个类地构造方法，其中这些都是声明了这个矩形地初始大小、位置及颜色。
        {
        
        }
        public override void Update() // 这一行重写了Update方法，这个方法是在游戏每次更新时要调用的方法。
        {
            if (Program.MainForm.KeyboardInput.GetKeyDown(System.Windows.Forms.Keys.A)) // 这一行表示获取按键A是否被按下，Program.MainForm.KetboardInput中可以获取任何按键是否被按下。
            {
                this.RigidBody.Force += new System.Numerics.Vector2(-2f, 0f); // 表示对这个RigidBody施加一个向左的力。
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(System.Windows.Forms.Keys.D)) // 这一行表示获取按键D是否被按下。
            {
                this.RigidBody.Force += new System.Numerics.Vector2(2f, 0f); // 表示对这个RigidBody施加一个向右的力。
            }
        }
    }
}
```

`Ground.cs`
```CSharp
using System;
...
using EnderEngine2D.GameObjects;
// 以上同样是引用，不用看。
//这里大部分代码都讲过了，如果上面那个看懂了，那么这个你也就能够看懂了。

namespace EnderEngine2D.Assets
{
    [RealGameObject(JoinGameAs.StaticBody)]
    internal class Ground1 : Square
    {
        public Ground1() : base(
            new System.Drawing.RectangleF(
                GameScreenConvert.PercentageToScreen(0f, DirectionType.X), 
                GameScreenConvert.PercentageToScreen(0.95f, DirectionType.Y), 
                GameScreenConvert.PercentageToScreen(1f, DirectionType.X), 
                GameScreenConvert.PercentageToScreen(0.01f, DirectionType.Y)), 
            System.Drawing.Color.WhiteSmoke)
        {
        }
    }
}
```

`LevelController.cs`
```CSharp
using System;
...
using EnderEngine2D.GameObjects;

namespace EnderEngine2D.Assets
{
    internal class LevelController
    {
        private static Level _level = default;

        public static Level Level
        {
            get
            {
                if (_level.Equals(default(Level)))
                {
                    _level = CreateLevel();
                }
                return _level;
            }
        }

        // 以上都在声明一个Level。Level是一个集合，当你在调用Level.LoadLevel时，整个游戏引擎就会将这个Level中的所有具有RealGameObject和直接或间接继承于GameObjectBase类的所有类都加入引擎中。

        private static Level CreateLevel() // 这里声明了如何创建这个Level。
        {
            var level = new Level
            {
                BackgroundColor = Color.Black, // 这里规定了这个Level加载后整个游戏的背景色。
                Objects = new Dictionary<string, GameObjectBase> // 这以下都是将要加入游戏引擎的类。
                {
                    { "PLAYER", new Player() }, // 加入了Player的新实例，且其在引擎中的名称为"PLAYER"，这个名称是唯一的，以后可以通过这个名字来找到这个类。
                    { "GROUND", new Ground() }, // 和上面一样，加入了Ground类，且名称为"GROUND"。
               }
            };
            return level;
        }
    }
}
```
如果你已大致了解了，那么你对这个游戏引擎就已经有一定了解了。接下来我将要系统地为你介绍这个游戏引擎。
# 深入了解引擎
## 特性
首先来看 `Attributes` 文件夹中的两个文件文件。
### AbleToNdcAttribute
这个特性一般应用在一个属性或字段上，表示这个属性/字段在调用 `ToSc()` 方法且此类的 `UseNdc` 属性为 `true` 后会从NDC坐标（归一化设备坐标）转为屏幕坐标。不建议使用。
### RealGameObjectAttribute
这个特性是此游戏引擎中最常用的特性，它应用在一个类上，并且表示如果这个类继承于 `GameObjectBase` 且其所在Level已被载入引擎后它可以进入游戏或者以特定的方式加入物理引擎。它有两个构造方法，不过我们一般只使用第一个。其中这里的唯一一个参数是一个枚举 `JoinGameAs` ，这个枚举有三个值，以下表格将介绍它们。

|值|说明|
|:---:|:---:|
|`JoinGameAs.None`|这表示这个类将不会加入物理引擎。|
|`JoinGameAs.StaticBody`|表示这个类将以 `StaticBody` 的形式加入物理引擎。 `StaticBody`将不受重力约束，且它可以阻挡 `RigidBody` 的运动。|
|`JoinGameAs.RigidBody`|表示这个类将以 `RigidBody` 的形式加入物理引擎。 `RigidBody`将受到重力约束，且它会受到 `StaticBody` 的阻挡。|

<br/><br/>
接下来看到 `GameObjects` 文件夹。

## GameObjects
### Camera
这里面储存了3个变量，但是只有 `X` 和 `Y` 会起作用。其原理非常简单， `X` `Y` 就表示这个渲染到窗口的摄像机在什么位置。
### GameObjectBase
这个类是引擎内所有类的根基，也是游戏引擎中最重要的一部分。不过这里将不详细介绍它，具体可以自行查看代码。

*更新于2025年10月25日，未完待续。*