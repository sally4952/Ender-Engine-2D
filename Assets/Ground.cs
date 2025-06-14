using EnderEngine2D.GameObjects;
using EnderEngine2D.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Assets
{
    [RealGameObject(JoinGameAs.StaticBody)]
    internal class Ground : Square
    {
        public Ground() :
            base(
                new RectangleF(GameScreenConvert.PercentageToScreen(0.1f, DirectionType.X), GameScreenConvert.PercentageToScreen(0.9f, DirectionType.Y), GameScreenConvert.PercentageToScreen(0.8f, DirectionType.X), 10),
                Color.Gray)
        {
        }

        bool IsLeft = false;

        public override void Update()
        {
            if (IsLeft)
            {
                this.Y -= 4f;
                if (this.Y < 180)
                {
                    IsLeft = false;
                }
            }
            else
            {
                this.Y += 4f;
                if (this.Y + this.Height > GameScreenConvert.PercentageToScreen(1f, DirectionType.Y))
                {
                    IsLeft = true;
                }
            }
        }
    }
}
