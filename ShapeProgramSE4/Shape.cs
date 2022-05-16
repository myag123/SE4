﻿using System;
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

        public abstract void draw(Graphics g);

        /// <summary>
        /// Abstrract method for calculating area of Shape. Width times Height.
        /// </summary>
        /// <returns></returns>
        public abstract double calcArea();

        /// <summary>
        /// Abstract method for calculating perimeter of Shape. Width plus Height.
        /// </summary>
        /// <returns></returns>
        public abstract double calcPerimeter();

        /// <summary>
        /// Virtual method to set colour and parameter list, to be filled in later in derived classes.
        /// All shapes will need a colour and x and y parameters in this application.
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="list"></param>
        public virtual void set(Color colour, params int[] list) 
        {
            this.colour = colour;
            this.x = list[0];
            this.y = list[1]; 
        }

        public override string ToString()
        {
            return base.ToString()+ "  " + this.x + "," + this.y + " : ";
        }

    }
}
