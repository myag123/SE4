using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Abstract class named Shape inheriting ShapeInterface, containing abstract methods which other classes will inherit.  
    /// All shapes are required to have a colour and a specifiy x and y axis position.
    /// Any classes that inherit Shape will implement these following abstract methods below.
    /// </summary>
    public abstract class Shape:ShapeInterface
    {
        //declaring protected variables/properties of a shape that can be referenced in inherited classes
        protected Color colour;
        protected int x, y;

        //Constructor containing properties of Shape
        public Shape(Color colour, int x, int y)
        {
            this.colour = colour; // Shapes colour
            this.x = x; // x position
            this.y = y; // y position
        }

        protected Shape()
        {
        }
        /// <summary>
        /// Abstract method Draw for shapes. 
        /// </summary>
        /// <param name="g">Graphics</param>
        public abstract void Draw(Graphics g);

        /// <summary>
        /// Abstract method Draw for shapes.
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="Fill">Fill status</param>
        public abstract void Draw(Graphics g, String Fill);

        /// <summary>
        /// Abstract method for calculating area of Shape. Width times Height.
        /// </summary>
        /// <returns></returns>
        public abstract double CalcArea();

        /// <summary>
        /// Abstract method for calculating perimeter of Shape. Width plus Height.
        /// </summary>
        /// <returns></returns>
        public abstract double CalcPerimeter();

        /// <summary>
        /// Virtual method to set colour and parameter list, to be filled in later in derived classes.
        /// All shapes will need a colour and x and y parameters in this application.
        /// </summary>
        /// <param name="colour">Colour of pen</param>
        /// <param name="list">Parameter list of numbers</param>
        public virtual void Set(Color colour, params int[] list) 
        {
            this.colour = colour;
            this.x = list[0];
            this.y = list[1]; 
        }

        /// <summary>
        /// Overriding ToString method.
        /// </summary>
        /// <returns>Returns name of class and inputted parameters.</returns>
        public override string ToString()
        {
            return base.ToString()+ "  " + this.x + "," + this.y + " : ";
        }
    }
}
