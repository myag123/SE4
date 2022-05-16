using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Shape interface creates objects, that are in the hierarchy. 
    /// Methods conform to template.
    /// </summary>
    interface ShapeInterface
    {
        void set(Color c, params int[] list);  
        public abstract double calcArea();
        public abstract double calcPerimeter();

    }
}
