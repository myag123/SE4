using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Class to draw pie which extends draw command.
    /// </summary>
    public class DrawPie : DrawCommand
    {
        private int xPos, yPos, width, height;

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
        /// Method to get and set width of rectangle.
        /// </summary>
        public int Width
        {
            get => width;
            set => width = value;
        }

        /// <summary>
        /// Method to get and set height of rectangle.
        /// </summary>
        public int Height
        {
            get => height;
            set => height = value;
        }

        /// <summary>
        /// Method to split input by comma and then convert input array to integer.
        /// </summary>
        /// <param name="Parameters">String of parameters.</param>
        /// <param name="ParamsInt">Output for integer array.</param>
        public override void ProcessParameters(string Parameters, out int[] ParamsInt)
        {
            String[] processor;
            if (Parameters == null)
            {
                throw new GPLException("\nUnable to process parameters due to null value"); // Exception thrown if parameters are null
            }

            if (!Parameters.Contains(","))
            {
                throw new GPLException("\n Unable to process Pie parameters due to syntax error.");
            }

            processor = Parameters.Split(",");

            if (processor[1] == "")
            {
                throw new GPLException("\n Unable to process Pie parameters due to syntax error.");
            }
            else
            {
                Array.ConvertAll(processor, s => int.Parse(s));
                ParamsInt = Array.ConvertAll(processor, s => int.Parse(s));
            }
        }

        /// <summary>
        /// Method to ensure parameter list for drawpie command contains no less than 2 parameters.
        /// </summary>
        /// <param name="ParameterList"></param>
        public override void ParseParameters(int[] parameterList)
        {
            if (parameterList.Length != 2)
            {
                throw new GPLException("Invalid number of parameters in DrawPie."); // Exception thrown if incorrect number of parameters are inputted
            }
        }

        /// <summary>
        /// Set method for DrawPie
        /// Method requires canvas object, command name and height and width values.
        /// </summary>
        /// <param name="c">Canvas object</param>
        /// <param name="Name">Command name</param>
        /// <param name="Parameters">height and width values</param>
        public void Set(Canvas c, String Name, String Parameters)
        {
            base.Set(c, "pie", Parameters);
            this.ProcessParameters(Parameters, out int[] ParamsInt);
            this.ParseParameters(ParamsInt);
            this.width = ParamsInt[0];
            this.height = ParamsInt[1];
        }

        /// <summary>
        /// Overriding ToString method.
        /// </summary>
        /// <returns>Returns name of class and x and y value.</returns>
        public override string ToString()
        {
            return base.ToString() + "DrawPie: " + this.xPos + " " + this.yPos;
        }

        /// <summary>
        /// Execute method for DrawPie to draw pie to canvas with inputted values.
        /// </summary>
        /// <returns>Returns true</returns>
        public override bool Execute()
        {
            c.DrawPie(width, height); // Does the actual draw to the canvas class
            return true;
        }

        public override void ProcessParameters(string parameters, out int ParamsInt)
        {
            throw new NotImplementedException();
        }
    }
}
