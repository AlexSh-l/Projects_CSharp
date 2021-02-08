using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using KnockoffPaint;


namespace Trapezium
{
    [Serializable]
    public class Trapezium : Figure
    {
        public override void Draw(Graphics e)
        {
            arr = new Point[4];
            arr[0].X = x1;
            arr[0].Y = y2;
            arr[3].X = x2;
            arr[3].Y = y2;
            arr[1].X = x1 + ((x2 - x1) / 4);
            arr[1].Y = y1;
            arr[2].X = x1 + (3 * (x2 - x1) / 4);
            arr[2].Y = y1;
            e.DrawPolygon(new Pen(Color.Black, 5), arr);

        }
        public Trapezium(int X1, int Y1, int X2, int Y2)
        {
            x1 = X1;
            x2 = X2;
            y1 = Y1;
            y2 = Y2;
        }
    }
}
