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
    [RealGameObject(JoinGameAs.StaticBody)]
    internal class Ground : Square
    {
        public Ground(RectangleF rect, Color color) : base(rect, color)
        {
        }
        bool up = false;
        public override void Update()
        {
            if (up)
            {
                //this.Y -= 2;
                if (this.Y < 200)
                {
                    up = false;
                }
            }
            else
            {
                //this.Y += 2;
                if (this.Y > 800)
                {
                    up = true;
                }
            }
        }
    }
}
