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
        int a = 0;

        public override void Update()
        {
            if (flying)
            {
                this.RigidBody.Y = flyingHeight;
            }
            InputHandle();
            CameraMove();
        }

        void InputHandle()
        {
            if (Program.MainForm.KeyboardInput.GetKeyDown(System.Windows.Forms.Keys.Escape))
            {
                System.Windows.Forms.Application.Exit();
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
                        a = 20;
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
                    if (a == 0)
                    {
                        while (jumping) ;
                        flying = true;
                        flyingHeight = this.RigidBody.Y;
                    }
                }
            }
            if (Program.MainForm.KeyboardInput.GetKeyUp(System.Windows.Forms.Keys.R))
            {
                Respawn();
            }
            if (a > 0) a--;
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
    }
}
