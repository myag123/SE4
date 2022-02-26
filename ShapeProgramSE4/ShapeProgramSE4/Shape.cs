using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Abstract class named Shape containing abstract methods which other classes will inherit.  
    /// All shapes are required to have a colour and a specifiy x and y axis position.
    /// </summary>
    abstract class Shape:ShapeInterface
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

        protected Shape()
        {
        }

        //must have the methods of the interface as it implements shapesInterface, essentially passing the repsonsiblities on
        //any classes that inherit Shape will implement these following abstract methods below
        public abstract void draw(Graphics g); 
        public abstract double calcArea(); 
        public abstract double calcPerimeter();

        public virtual void set(Color colour, params int[] list) //creating virtual set method to be filled in later in derived classes
        {
            this.colour = colour;
            this.x = list[0];
            this.y = list[1]; //all shapes have this in common
        }

        public override string ToString()
        {
            return base.ToString()+ "  " + this.x + "," + this.y + " : ";
        }

    }
}
