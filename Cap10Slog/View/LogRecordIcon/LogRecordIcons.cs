using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Cap10Slog.View.LogRecordIcon
{
    public abstract class AbstractLogRecordIcon : ILogRecordIcon
    {
        protected Brush brush;
        protected Pen pen;

        public AbstractLogRecordIcon(Color color)
        {
            this.brush = new SolidBrush(color);
            this.pen = new Pen(this.brush, 3.0f);
        }

        public abstract void Draw(Graphics g, Rectangle r);
    }

    class Circle : AbstractLogRecordIcon
    {
        public Circle(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            g.FillEllipse(this.brush, r);
        }
    }

    class Square : AbstractLogRecordIcon
    {
        public Square(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            g.FillRectangle(this.brush, r);
        }
    }

    class Diamond : AbstractLogRecordIcon
    {
        PointF[] points = new PointF[4];

        public Diamond(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            points[0].X = r.X + r.Width/2;
            points[0].Y = r.Y;

            points[1].X = r.X + r.Width;
            points[1].Y = r.Y + r.Height/2;

            points[2].X = r.X + r.Width/2;
            points[2].Y = r.Y + r.Height;

            points[3].X = r.X;
            points[3].Y = r.Y + r.Height/2;


            g.FillPolygon(this.brush, points);
        }
    }

    class Triangle1 : AbstractLogRecordIcon
    {
        PointF[] points = new PointF[3];

        public Triangle1(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            points[0].X = r.X + r.Width/2;
            points[0].Y = r.Y;

            points[1].X = r.X + r.Width;
            points[1].Y = r.Y + r.Height;

            points[2].X = r.X;
            points[2].Y = r.Y + r.Height;

            g.FillPolygon(this.brush, points);
        }
    }

    class Triangle2 : AbstractLogRecordIcon
    {
        PointF[] points = new PointF[3];

        public Triangle2(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            points[0].X = r.X;
            points[0].Y = r.Y;

            points[1].X = r.X + r.Width;
            points[1].Y = r.Y;

            points[2].X = r.X + r.Width/2;
            points[2].Y = r.Y + r.Height;

            g.FillPolygon(this.brush, points);
        }
    }

    class EmptyCircle : AbstractLogRecordIcon
    {
        public EmptyCircle(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            g.DrawEllipse(this.pen, r);
        }
    }

    class EmptySquare : AbstractLogRecordIcon
    {
        public EmptySquare(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            g.DrawRectangle(this.pen, r);
        }
    }

    class EmptyDiamond : AbstractLogRecordIcon
    {
        PointF[] points = new PointF[4];

        public EmptyDiamond(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            points[0].X = r.X + r.Width / 2;
            points[0].Y = r.Y;

            points[1].X = r.X + r.Width;
            points[1].Y = r.Y + r.Height / 2;

            points[2].X = r.X + r.Width / 2;
            points[2].Y = r.Y + r.Height;

            points[3].X = r.X;
            points[3].Y = r.Y + r.Height / 2;

            g.DrawPolygon(this.pen, points);
        }
    }

    class EmptyTriangle1 : AbstractLogRecordIcon
    {
        PointF[] points = new PointF[3];

        public EmptyTriangle1(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            points[0].X = r.X + r.Width / 2;
            points[0].Y = r.Y;

            points[1].X = r.X + r.Width;
            points[1].Y = r.Y + r.Height;

            points[2].X = r.X;
            points[2].Y = r.Y + r.Height;

            g.DrawPolygon(this.pen, points);
        }
    }

    class EmptyTriangle2 : AbstractLogRecordIcon
    {
        PointF[] points = new PointF[3];

        public EmptyTriangle2(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            points[0].X = r.X;
            points[0].Y = r.Y;

            points[1].X = r.X + r.Width;
            points[1].Y = r.Y;

            points[2].X = r.X + r.Width / 2;
            points[2].Y = r.Y + r.Height;

            g.DrawPolygon(this.pen, points);
        }
    }

    class X : AbstractLogRecordIcon
    {
        public X(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            g.DrawLine(this.pen, r.X, r.Y, r.X + r.Width, r.Y + r.Height);
            g.DrawLine(this.pen, r.X, r.Y + r.Height, r.X + r.Width, r.Y);
        }
    }

    class Plus : AbstractLogRecordIcon
    {
        public Plus(Color color)
            : base(color)
        { }

        override public void Draw(Graphics g, Rectangle r)
        {
            g.DrawLine(this.pen, r.X+r.Width/2, r.Y, r.X + r.Width/2, r.Y + r.Height);
            g.DrawLine(this.pen, r.X, r.Y + r.Height / 2, r.X + r.Width, r.Y + r.Height / 2);
        }
    }

    class Dot : AbstractLogRecordIcon
    {
        public Dot(Color color)
            : base(color)
        { }

        public override void Draw(Graphics g, Rectangle r)
        {
            g.FillEllipse(this.brush, r.X + r.Width / 2 - 2, r.Y + r.Height/2 - 2, 4, 4);
        }
    }
}
