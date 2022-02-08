using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    class Circle : Shape //extends shape (base class)
    {
        int radius;  //declaring radius of circle variable

        public Circle() : base() //explicitly calling constructor of shape
        { 

        }

        public Circle(Color colour, int x, int y, int radius) : base(colour, x, y)
        {
            this.radius = radius; //setting extra property of circle
        }

        public override void set(Color colour, params int[] list)
        {
            base.set(colour, list[0], list[1]); //calling shape class setting colour, x, y, radius passing it through list
            this.radius = list[2];
        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2); //creates pen object, pen draws border around a shape
            Brush b = new SolidBrush(colour);  //brush paints the interior of a shape
            g.FillEllipse(b, x, y, radius * 2, radius * 2);
            g.DrawEllipse(p, x, y, radius * 2, radius * 2);
        }

        public override double calcArea()
        {
            return Math.PI * (radius ^ 2); //calculating area
        }

        public override double calcPerimeter()
        {
            return 2 * Math.PI * radius; //calculating perimeter
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.radius;
        }
    }
}
