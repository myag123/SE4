using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Class to draw Circle which extends draw command.
    /// </summary>
    public class DrawCircle : DrawCommand
    {
        private int xPos, yPos, radius;

        /// <summary>
        /// Method to get and set x axis position.
        /// </summary>
        public int XPos
        {
            get => xPos;
            set => xPos = value;
        }

        /// <summary>
        /// Method to get and set y axis position.
        /// </summary>
        public int YPos
        {
            get => yPos;
            set => yPos = value;
        }

        /// <summary>
        /// Method to get and set radius.
        /// </summary>
        public int Radius
        {
            get => radius;
            set => radius = value;
        }

        /// <summary>
        /// Method to change inputted radius from String to Int.
        /// </summary>
        /// <param name="Parameters">String of parameters.</param>
        /// <param name="ParamsInt">Output for integer array.</param>
        public override void ProcessParameters(String parameters, out int ParamsInt)
        {
            try { ParamsInt = Int32.Parse(parameters);}
            catch (FormatException ex){ throw new GPLException("Invalid number of parameters for circle." + ex.ToString()); }
        }

        /// <summary>
        /// Method to set and process parameters to draw circle.
        /// </summary>
        /// <param name="c">Canvas object</param>
        /// <param name="Name">Command name</param>
        /// <param name="Parameters">Radius value</param>
        public void Set(Canvas c, String Name, String Parameters)
        {
            base.Set(c, "circle", Parameters);
            this.ProcessParameters(Parameters, out int ParamsInt);
            this.radius = ParamsInt;
        }

        /// <summary>
        /// Overriding ToString method.
        /// </summary>
        /// <returns>Returns name of class and inputted parameters.</returns>
        public override string ToString()
        {
            return base.ToString() + "DrawCircle" + this.xPos + " " + this.yPos;
        }

        /// <summary>
        /// Execute method for DrawCircle.
        /// Method calls DrawCircle method in the canvas class to draw circle on screen.
        /// </summary>
        /// <returns>Returns true boolean value.</returns>
        public override bool Execute()
        {
            c.DrawCircle(radius); 
            return true;
        }

        public override void ProcessParameters(String parameters, out int[] ParamsInt)
        {
            throw new NotImplementedException();
        }

        public override void ParseParameters(int[] parameterList)
        {
            throw new NotImplementedException();
        }
    }
}
