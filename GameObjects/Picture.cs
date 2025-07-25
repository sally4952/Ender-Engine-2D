using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnderEngine2D.Attributes;
using EnderEngine2D.Physics;

namespace EnderEngine2D.GameObjects
{
    /// <summary>
    /// 确定Picture类在渲染时使用哪种方式渲染图片。
    /// </summary>
    public enum PictureDrawType : byte
    {
        /// <summary>
        /// 可缩放的图片，需要手动定义图片的长和宽。
        /// </summary>
        None,
        /// <summary>
        /// 根据已经定义的宽和图片比例自动推算要显示的高，需要手动定义宽。
        /// </summary>
        AutoHeight,
        /// <summary>
        /// 根据已经定义的高和图片比例自动推算要显示的宽，需要手动定义高。
        /// </summary>
        AutoWidth,
        /// <summary>
        /// 使用图像原始物理大小绘制图像，不受Height和Width属性的影响。
        /// </summary>
        Unscaled,
        /// <summary>
        /// 在不缩放的情况下绘制图片，并且在需要时按照由X、Y、Width、Height组成的长方形进行裁剪。
        /// </summary>
        UnscaledAndClipped,
    }

    /// <summary>
    /// 表示一个能够绘制图片的GameObject。
    /// </summary>
    internal class Picture : GameObjectBase, IDrawable
    {
        /// <summary>
        /// 获取此图像的刚体（表示此Obejct可以掉落）。
        /// </summary>
        public override RigidBody RigidBody { get; set; }
        /// <summary>
        /// 获取此图像的静态体，表示此Object在引力下是固定不动的。
        /// </summary>
        public override StaticBody StaticBody { get; set; }
        /// <summary>
        /// 图像的X轴。
        /// </summary>
        [AbleToNdc(TypeCode.Single, DirectionType.X)]
        public override float X
        {
            get
            {
                return RigidBody.X;
            }
            set
            {
                if (RigidBody != null)
                {
                    RigidBody.X = value;
                }
                if (StaticBody != null)
                {
                    StaticBody.X = value;
                }
            }
        }
        /// <summary>
        /// 图像的Y轴。
        /// </summary>
        [AbleToNdc(TypeCode.Single, DirectionType.Y)]
        public override float Y
        {
            get
            {
                return RigidBody.Y;
            }
            set
            {
                if (RigidBody != null)
                {
                    RigidBody.Y = value;
                }
                if (StaticBody != null)
                {
                    StaticBody.Y = value;
                }
            }
        }
        /// <summary>
        /// 图像的宽。
        /// </summary>
        [AbleToNdc(TypeCode.Single, DirectionType.X)]
        public float Width { get; set; }
        /// <summary>
        /// 图像的高。
        /// </summary>
        [AbleToNdc(TypeCode.Single, DirectionType.Y)]
        public float Height { get; set; }
        /// <summary>
        /// 图像的大小。
        /// </summary>
        public SizeF Size
        {
            get => new SizeF(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }
        /// <summary>
        /// 此图像组成的矩形。
        /// </summary>
        public RectangleF Rectangle
        {
            get => new RectangleF(X, Y, Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// 此Picture类要显示的图像。
        /// </summary>
        public virtual Bitmap Image { get; set; }
        /// <summary>
        /// 确定使用哪种方式绘制图像。
        /// </summary>
        public PictureDrawType DrawType { get; set; }
        void IDrawable.Draw(Graphics g)
        {
            switch (DrawType)
            {
                case PictureDrawType.None:
                    g.DrawImage(Image, X, Y, Width, Height);
                    break;
                case PictureDrawType.AutoHeight:
                    g.DrawImage(Image, X, Y, Width, Image.Height * Width / Image.Width);
                    break;
                case PictureDrawType.AutoWidth:
                    g.DrawImage(Image, X, Y, Image.Width * Height / Image.Height, Height);
                    break;
                case PictureDrawType.Unscaled:
                    g.DrawImageUnscaled(Image, (int)Math.Round(X), (int)Math.Round(Y));
                    break;
                case PictureDrawType.UnscaledAndClipped:
                    g.DrawImageUnscaledAndClipped(Image, new Rectangle((int)Math.Round(X), (int)Math.Round(Y), (int)Math.Round(Width), (int)Math.Round(Height)));
                    break;
            }
        }
    }
}
