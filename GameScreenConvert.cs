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
    /// <summary>
    /// 确定转换的方向时横向还是竖向。
    /// </summary>
    internal enum DirectionType
    {
        /// <summary>
        /// 横向。
        /// </summary>
        X,
        /// <summary>
        /// 竖向。
        /// </summary>
        Y,
    }
    /// <summary>
    /// 提供一套从游戏位置转为屏幕位置或从百分比转到屏幕位置（或者反过来）的方法。
    /// </summary>
    internal static class GameScreenConvert
    {
        /// <summary>
        /// （不使用）游戏位置转到屏幕位置。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
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
        /// <summary>
        /// （不使用）游戏位置转到屏幕位置。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2 GameToScreen(Vector2 value)
        {
            return new Vector2(GameToScreen(value.X, DirectionType.X), GameToScreen(value.Y, DirectionType.Y));
        }
        /// <summary>
        /// 游戏位置转到屏幕位置。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static RectangleF GameToScreen(RectangleF value)
        {
            return new RectangleF(
                GameToScreen(value.X, DirectionType.X), 
                GameToScreen(value.Y, DirectionType.Y), 
                GameToScreen(value.Width, DirectionType.X),
                GameToScreen(value.Height, DirectionType.Y));
        }
        /// <summary>
        /// 屏幕位置转到游戏位置。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 百分比转到屏幕位置。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 屏幕位置转到百分比。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
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
