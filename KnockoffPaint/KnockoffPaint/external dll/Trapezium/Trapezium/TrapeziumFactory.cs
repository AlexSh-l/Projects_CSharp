using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using KnockoffPaint;

namespace Trapezium
{
    public class TrapeziumFactory : FigureFactory
    {
        public TrapeziumFactory()
        {

        }
        public override Figure Create(Point[] points)
        {
            Point point1 = points[points.Length - 2], point2 = points[points.Length - 1];
            return new Trapezium(point1.X, point1.Y, point2.X, point2.Y);
        }
    }
}
