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
        void Set(Color c, params int[] list);  
        public abstract double CalcArea();
        public abstract double CalcPerimeter();

    }
}
