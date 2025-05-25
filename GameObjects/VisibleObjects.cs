#pragma warning disable CS8500

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameObjects
{
    internal static class VisibleObjects
    {
        public static unsafe IDrawable*[] Objects = new IDrawable*[0];
    }
}
