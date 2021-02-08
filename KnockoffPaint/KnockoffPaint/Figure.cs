using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KnockoffPaint
{
    [Serializable]
    public abstract class Figure
    {
        public abstract void Draw(Graphics e);
        public int x1, x2, y1, y2;
        public Point[] arr;
    }
}
