using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KnockoffPaint
{
    [Serializable]
    public class Line : Figure
    {
        public override void Draw(Graphics e)
        {
            e.DrawLine(new Pen(Color.Black, 5), x1, y1, x2, y2);           
        }
        public Line(int X1,int Y1, int X2, int Y2)
        {
            x1 = X1;
            x2 = X2;
            y1 = Y1;
            y2 = Y2;
        }
    }
}
