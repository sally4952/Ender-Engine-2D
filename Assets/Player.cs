using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Player(RectangleF rect, Color color) : base(rect, color)
        {
            this.UseNdc = false;
        }

        bool jumping = false;
        bool flying = false;
        float flyingHeight;
        bool canCameraMove = true;
        bool menuShowing = false;
        int a = 0;
        int b = 0;
        int c = 0;

        public override void Update()
        {
            if (flying)
            {
                this.RigidBody.Y = flyingHeight;
                this.RigidBody.Force = new System.Numerics.Vector2(RigidBody.Force.X, 0);
            }
            InputHandle();
            CameraMove();
        }

        void InputHandle()
        {
            if (Program.MainForm.KeyboardInput.GetKeyUp(System.Windows.Forms.Keys.Escape))
            {
                if (a == 0)
                {
                    if (menuShowing)
                    {
                        CloseMenu();
                        menuShowing = false;
                        a = 16;
                    }
                    else
                    {
                        ShowMenu();
                        menuShowing = true;
                        a = 16;
                    }
                }
            }
            if (menuShowing)
            {
                if (a > 0) a--;
                if (b > 0) b--;
                if (c > 0) c--;
                return;
            }
            if (flying)
            {
                if (Program.MainForm.KeyboardInput.GetKeyDown(System.Windows.Forms.Keys.ShiftKey))
                {
                    flyingHeight += 10f;
                }
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(System.Windows.Forms.Keys.A))
            {
                this.RigidBody.Force += new System.Numerics.Vector2(-1.2f, 0);
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(System.Windows.Forms.Keys.D))
            {
                this.RigidBody.Force += new System.Numerics.Vector2(1.2f, 0);
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(System.Windows.Forms.Keys.Space))
            {
                if (flying)
                {
                    flyingHeight -= 10f;
                }
                else if (this.RigidBody.IsGrounded)
                {
                    if (!jumping)
                    {
                        jumping = true;
                        c = 12;
                        Task.Run(async () =>
                        {
                            for (var i = 12; i > 0; i--)
                            {
                                this.RigidBody.Force += new System.Numerics.Vector2(0, i);
                                await Task.Delay(14);
                            }
                            jumping = false;
                        });
                    }
                }
                else if (jumping)
                {
                    if (c == 0)
                    {
                        while (jumping) ;
                        flying = true;
                        flyingHeight = this.RigidBody.Y;
                    }
                }
            }
            if (Program.MainForm.KeyboardInput.GetKeyUp(System.Windows.Forms.Keys.R))
            {
                if (b == 0)
                {
                    Respawn();
                    b = 64;
                }
            }
            if (a > 0) a--;
            if (b > 0) b--;
            if (c > 0) c--;
        }

        void CameraMove()
        {
            if (canCameraMove)
            {
                if (this.X < Camera.X + 400f)
                {
                    Camera.X = this.X - 400f;
                }
                else if (this.X > Camera.X + GameScreenConvert.PercentageToScreen(1f, DirectionType.X) - 400f)
                {
                    Camera.X = this.X + 400f - GameScreenConvert.PercentageToScreen(1f, DirectionType.X);
                }
                if (this.Y < Camera.Y + 300f)
                {
                    Camera.Y = this.Y - 300f;
                }
                else if (this.Y > Camera.Y + GameScreenConvert.PercentageToScreen(1f, DirectionType.Y) - 300f)
                {
                    Camera.Y = this.Y + 300f - GameScreenConvert.PercentageToScreen(1f, DirectionType.Y);
                }
            }
        }

        void Respawn()
        {
            this.RigidBody.X = 400;
            this.RigidBody.Y = 100;
        }

        void ShowMenu()
        {
            UI.UIContainer.IsGaming = false;
            UI.UIContainer.Now = new[]
            {
                new UI.UIContainer(0, Color.FromArgb(200, Color.Black))
                {
                    Components = new Dictionary<string, UI.UIBase>
                    {
                        { "TITLE", new UI.Label(0, 100, 100, "标题") },
                        { "CONTINUE", new UI.Button(0, "继续游戏", 100, 300, Color.White, Color.Gray, Color.LightGray, new Font("微软雅黑", 16f), false, false, () => CloseMenu()) { Width = 100f, Height = 30f } },
                        { "EXIT", new UI.Button(0, "退出游戏", 100, 500, Color.White, Color.Gray, Color.LightGray, new Font("微软雅黑", 16f), false, false, () => Environment.Exit(0)) { Width = 100f, Height = 30f } },
                    },
                },
            };
            Program.PhysicalEngine.Pause();
        }

        void CloseMenu()
        {
            UI.UIContainer.IsGaming = true;
            menuShowing = false;
            Program.PhysicalEngine.Continue();
        }
    }
}
