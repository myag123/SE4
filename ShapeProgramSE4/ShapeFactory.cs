using System;
using System.Collections.Generic;
using System.Text;
/*
 * Factory class, where all the dependencies are.
 * Contains just one method, getShape that returns the available shapes
 */
namespace ShapeProgramSE4
{
    class ShapeFactory
    {
        /// <summary>
        /// Method to get shape and return the shapes obejct based on user input.
        /// </summary>
        /// <param name="shapeType">Input from user.</param>
        /// <returns></returns>
        public Shape getShape(string shapeType)
        {
            shapeType = shapeType.ToLower().Trim();

            if (shapeType.Equals("circle"))
            {
                return new Circle(); //returns circle class
            }

            else if (shapeType.Equals("square"))
            {
                return new Square(); //returns square class
            }

            else if (shapeType.Equals("triangle"))
            {
                return new Triangle(); //returns triangle class
            }

            else if (shapeType.Equals("rectangle"))
            {
                return new Rectangle(); //returns rectangle class
            }
            else
            {
                System.ArgumentException argEx = new System.ArgumentException("Error" + shapeType + "does not exist.");
                throw argEx;
            }
        }

    }
}
