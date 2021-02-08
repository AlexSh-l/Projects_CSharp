using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KnockoffPaint
{
    [Serializable]
    public class Polyline : Figure
    {
        public override void Draw(Graphics e)
        {
            e.DrawPolygon(new Pen(Color.Black, 5), arr);
        }
        public Polyline(Point[] points)
        {
            arr = new Point[points.Length];
            int k = 0;
            foreach(Point temp in points)
            {
                arr[k] = temp;
                k++;
            }
        }
    }
}
