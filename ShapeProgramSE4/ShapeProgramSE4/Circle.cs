using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    class Circle : Shape //extends shape
    {
        int radius;  //declaring radius of circle variable

        public Circle(Color colour, int x, int y, int radius) : base(colour, x, y)
        {
            this.radius = radius; //setting extra property of circle
        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2); //creates pen object, pen draws border around a shape
            Brush b = new SolidBrush(colour);  //brush paints the interior of a shape
            g.FillEllipse(b, x, y, radius * 2, radius * 2);
            g.DrawEllipse(p, x, y, radius * 2, radius * 2);
        }


        public override string ToString()
        {
            return base.ToString() + " " + this.radius;
        }
    }
}
