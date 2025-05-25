using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    internal abstract class GameObjectBase
    {
        public virtual float X { get; set; }
        public virtual float Y { get; set; }
        public Vector2 Position
        {
            get => new Vector2(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }
    }
}
