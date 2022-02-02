﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    class Triangle : Shape
    {
        int width, height;
        Point[] pts = new Point[3]; //creating points for triangle shape

        public Triangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {
            this.width = width;
            this.height = height;

            //Setting points to form a triangle
            pts[0].X = width / 2;
            pts[0].Y = height / 2 - 50;
            pts[1].X = width / 2 + 50;
            pts[1].Y = height / 2 + 50;
            pts[2].X = width / 2 - 50;
            pts[2].Y = height / 2 + 50;
        }
        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2); //creates pen object, pen draws border around a shape
            Brush b = new SolidBrush(colour);  //brush paints the interior of a shape

            g.FillPolygon(b, pts); //filling in colour of triangle
            g.DrawPolygon(p, pts); //drawing triangle passing in points of triangle from array
            
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.width + "  " + this.height;
        }
    }
}
