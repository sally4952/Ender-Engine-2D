using EnderEngine2D.GameObjects;
using EnderEngine2D.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D.Assets
{
    [RealGameObject(JoinGameAs.RigidBody)]
    class Player : Square
    {
        bool IsJumping = false;
        bool CanRespawn = true;

        public Player() :
            base(
                new RectangleF(
                    GameScreenConvert.PercentageToScreen(0.2f, DirectionType.X), 
                    GameScreenConvert.PercentageToScreen(0.6f, DirectionType.Y), 
                    100, 100),
                Color.White)
        {
        }

        public override void Update()
        {
            if (Program.MainForm.KeyboardInput.GetKeyDown(Keys.Escape))
            {
                Application.Exit();
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(Keys.A))
            {
                if (this.X - 4f > 0)
                    this.X -= 4f;
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(Keys.D))
            {
                if (this.X + 4 < GameScreenConvert.PercentageToScreen(1f, DirectionType.X))
                    this.X += 4;
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(Keys.Space))
            {
                if (this.RigidBody.Force == Vector2.Zero)
                {
                    if (!IsJumping)
                    {
                        IsJumping = true;
                        Task.Run(async () =>
                        {
                            await Jump(20f);
                            IsJumping = false;
                        });
                    }
                }
            }
            if (this.Y > GameScreenConvert.PercentageToScreen(1f, DirectionType.Y))
            {
                this.X = GameScreenConvert.PercentageToScreen(0.2f, DirectionType.X);
                this.Y = GameScreenConvert.PercentageToScreen(0.6f, DirectionType.Y);
                this.RigidBody.Force = Vector2.Zero;
                Task.Run(async () =>
                {
                    IsJumping = true;
                    await Task.Delay(20);
                    IsJumping = false;
                });
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(Keys.R))
            {
                if (CanRespawn)
                {
                    CanRespawn = false;
                    this.X = GameScreenConvert.PercentageToScreen(0.2f, DirectionType.X);
                    this.Y = GameScreenConvert.PercentageToScreen(0.6f, DirectionType.Y);
                    this.RigidBody.Force = Vector2.Zero;
                    Task.Run(async () =>
                    {
                        IsJumping = true;
                        await Task.Delay(20);
                        IsJumping = false;
                        await Task.Delay(2980);
                        CanRespawn = true;
                    });
                }
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(Keys.Q))
            {
                Level.LoadLevel(new Level { BackgroundColor = Color.White, Objects = new Dictionary<string, GameObjectBase> { { "Ground", new Ground() } } });
            }
        }

        async Task Jump(float force)
        {
            for (var i = force; i > 0; i--)
            {
                this.Y -= i;
                await Task.Delay(4);
            }
        }
    }
}
