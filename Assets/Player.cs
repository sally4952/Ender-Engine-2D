using EnderEngine2D.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D.Assets
{
    [RealGameObject(JoinGameAs.RigidBody)]
    class Player : Square
    {
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
                Environment.Exit(0);
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(Keys.A))
            {
                this.X -= 4f;
            }
            if (Program.MainForm.KeyboardInput.GetKeyDown(Keys.D))
            {
                this.X += 4;
            }
        }
    }
}
