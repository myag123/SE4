using System;
using System.Collections.Generic;
using System.Text;
namespace ShapeProgramSE4
{
    /// <summary>
    /// Factory class, where all the dependencies are.
    /// Contains just one method, getShape that returns the available shapes.
    /// </summary>
    public class ShapeFactory
    {
        /// <summary>
        /// Method to get shape and return the shapes obejct based on user input.
        /// </summary>
        /// <param name="shapeType">Input from user.</param>
        /// <returns>Returns shape object</returns>
        public Shape GetShape(string shapeType)
        {
            shapeType = shapeType.ToLower().Trim();

            if (shapeType.Equals("circle")){ return new Circle(); } 
            if (shapeType.Equals("square")) { return new Square(); } 
            if (shapeType.Equals("triangle")) { return new Triangle(); }
            if (shapeType.Equals("rectangle")) { return new Rectangle(); }
            if (shapeType.Equals("pie")) { return new Pie(); }
            else { throw new ArgumentException("Error" + shapeType + "does not exist."); }
        }

    }
}
