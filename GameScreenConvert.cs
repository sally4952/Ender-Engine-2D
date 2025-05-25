using EnderEngine2D.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnderEngine2D
{
    internal enum DirectionType
    {
        X,
        Y,
    }

    internal static class GameScreenConvert
    {
        public static float GameToScreen(float value, DirectionType dt)
        {
            var result = 0f;
            if (dt == DirectionType.X)
            {
                value -= Camera.X;
                value += Screen.PrimaryScreen.Bounds.Width / 2;
            }
            else if (dt == DirectionType.Y)
            {
                value -= Camera.Y;
                value += Screen.PrimaryScreen.Bounds.Height / 2;
            }
            result = value * 10;
            return result;
        }

        public static Vector2 GameToScreen(Vector2 value)
        {
            return new Vector2(GameToScreen(value.X, DirectionType.X), GameToScreen(value.Y, DirectionType.Y));
        }

        public static RectangleF GameToScreen(RectangleF value)
        {
            return new RectangleF(
                GameToScreen(value.X, DirectionType.X), 
                GameToScreen(value.Y, DirectionType.Y), 
                GameToScreen(value.Width, DirectionType.X),
                GameToScreen(value.Height, DirectionType.Y));
        }

        public static float ScreenToGame(float value, DirectionType dt)
        {
            var result = 0f;
            if (dt == DirectionType.X)
            {
                value -= Screen.PrimaryScreen.Bounds.Width / 2;
                value += Camera.X;
            }
            else if (dt == DirectionType.Y)
            {
                value -= Screen.PrimaryScreen.Bounds.Height / 2;
                value += Camera.Y;
            }
            result = value / 10;
            return result;
        }

        public static float PercentageToScreen(float value, DirectionType dt)
        {
            var result = 0f;
            if (dt == DirectionType.X)
            {
                result = Screen.PrimaryScreen.Bounds.Width * value;
            }
            else if (dt == DirectionType.Y)
            {
                result = Screen.PrimaryScreen.Bounds.Height * value;
            }
            return result;
        }

        public static float ScreenToPercentage(float value, DirectionType dt)
        {
            var result = 0f;
            if (dt == DirectionType.X)
            {
                result = value / Screen.PrimaryScreen.Bounds.Width;
            }
            else if (dt == DirectionType.Y)
            {
                result = value / Screen.PrimaryScreen.Bounds.Height;
            }
            return result;
        }
    }
}
