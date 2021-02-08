using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KnockoffPaint
{
    public abstract class FigureFactory
    {
        public abstract Figure Create (Point[] points);
    }

    public class LineFactory : FigureFactory
    {
        public LineFactory()
        {
            
        }
        public override Figure Create(Point[] points)
        {
            Point point1 = points[points.Length - 2], point2 = points[points.Length-1];
            return new Line(point1.X, point1.Y, point2.X, point2.Y);
        }
    }
    public class RectangleFactory : FigureFactory
    {
        public RectangleFactory()
        {
            
        }
        public override Figure Create(Point[] points)
        {
            Point point1 = points[points.Length - 2], point2 = points[points.Length - 1];
            return new Rectangle(point1.X, point1.Y, point2.X, point2.Y);
        }
    }
    public class SquareFactory : FigureFactory
    {
        public SquareFactory()
        {
            
        }
        public override Figure Create(Point[] points)
        {
            Point point1 = points[points.Length - 2], point2 = points[points.Length - 1];
            return new Square(point1.X, point1.Y, point2.X, point2.Y);
        }
    }
    public class EllipsisFactory : FigureFactory
    {
        public EllipsisFactory()
        {
            
        }
        public override Figure Create(Point[] points)
        {
            Point point1 = points[points.Length - 2], point2 = points[points.Length - 1];
            return new Ellipsis(point1.X, point1.Y, point2.X, point2.Y);
        }
    }
    public class CircleFactory : FigureFactory
    {
        public CircleFactory()
        {
            
        }
        public override Figure Create(Point[] points)
        {
            Point point1 = points[points.Length - 2], point2 = points[points.Length - 1];
            return new Circle(point1.X, point1.Y, point2.X, point2.Y);
        }
    }
    public class PolylineFactory : FigureFactory
    {
        public PolylineFactory()
        {

        }
        public override Figure Create(Point[] points)
        {
            return new Polyline(points);
        }
    }
}
