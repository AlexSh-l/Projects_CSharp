using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KnockoffPaint
{
    [Serializable]
    public class Square : Figure
    {
        public override void Draw(Graphics e)
        {
            int a, b;
            if (x1 < x2)
                a = x1;
            else
                a = x2;
            if (y1 < y2)
                b = y1;
            else
                b = y2;
            e.DrawRectangle(new Pen(Color.Black, 5), a, b, Math.Abs(x2 - x1), Math.Abs(x2 - x1));
        }
        public Square(int X1, int Y1, int X2, int Y2)
        {
            x1 = X1;
            x2 = X2;
            y1 = Y1;
            y2 = Y2;
        }
    }
}
