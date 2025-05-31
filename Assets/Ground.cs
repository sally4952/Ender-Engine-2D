using EnderEngine2D.GameObjects;
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
    }
}
