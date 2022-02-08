using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
/*
 * 
 * Shape interface creates objects, that are in the hierarchy. Methods conform to template. 
 */
namespace ShapeProgramSE4
{
    interface ShapeInterface
    {
        void set(Color c, params int[] list);  
        public abstract double calcArea();
        public abstract double calcPerimeter();

    }
}
