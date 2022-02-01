using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
/*
 * Abstract class named Shape containing abstract methods which other classes will inherit.  
 * All shapes are required to have a colour and a specifiy x and y axis position.
 *
 */
namespace ShapeProgramSE4
{
    abstract class Shape
    {
        //declaring protected variables/properties of a shape that can be referenced in inherited classes
        protected Color colour;
        protected int x, y;

        //Constructor containing properties of Shape
        public Shape(Color colour, int x, int y)
        {
            this.colour = colour; //shapes colour
            this.x = x; //x position
            this.y = y; //y position
        }

        public abstract void draw(Graphics g); //any classes that inherit Shape will implement this method

        //derived from object class
        public override string ToString()
        {
            return base.ToString()+ "  " + this.x + "," + this.y + " : ";
        }

    }
}
