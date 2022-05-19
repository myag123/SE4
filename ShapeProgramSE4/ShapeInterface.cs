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
        /// <summary>
        /// Method to set shape. 
        /// Every class that inherits Shape class must have an overriden set method.
        /// </summary>
        /// <param name="c">Colour of pen</param>
        /// <param name="list">List of number parameters</param>
        void Set(Color c, params int[] list);  

        /// <summary>
        /// Abstract method to calculate area of shape.
        /// </summary>
        /// <returns>Value of calculation</returns>
        public abstract double CalcArea();

        /// <summary>
        /// Abstract method to calculate perimeter of shape.
        /// </summary>
        /// <returns>Value of calculation</returns>
        public abstract double CalcPerimeter();

    }
}
