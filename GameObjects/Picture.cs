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
    public enum PictureDrawType : byte
    {
        None,
        AutoHeight,
        AutoWidth,
        Unscaled,
        UnscaledAndClipped,
    }

    internal class Picture : GameObjectBase, IDrawable
    {
        public override RigidBody RigidBody { get; set; }
        public override StaticBody StaticBody { get; set; }
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
        [AbleToNdc(TypeCode.Single, DirectionType.X)]
        public float Width { get; set; }
        [AbleToNdc(TypeCode.Single, DirectionType.Y)]
        public float Height { get; set; }
        public SizeF Size
        {
            get => new SizeF(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }
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
        public virtual Bitmap Image { get; set; }
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
