using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShapeProgramSE4
{
    class MoveTo : DrawCommand
    {
        private int xPos, yPos;

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
        /// Method to move pen on canvas by setting values of x axis and y axis
        /// </summary>
        /// <param name="c">Canvas object</param>
        /// <param name="x">x axis position</param>
        /// <param name="y">y axis positon</param>
        public MoveTo(Canvas c, int x, int y) : base(c)
        {
            Name = "moveto";
            this.xPos = x;
            this.yPos = y;
        }

        /// <summary>
        /// Constructor method for MoveTo.
        /// </summary>
        public MoveTo()
        {

        }

        /// <summary>
        /// Method to split input by comma and then convert input array to integer.
        /// </summary>
        /// <param name="Parameters">String of parameters.</param>
        /// <param name="ParamsInt">Output for integer array.</param>
        public override void ProcessParameters(String Parameters, out int[] ParamsInt)
        {
            String[] processor;

            //if (Parameters == null)
            //{
             //   throw new GPLException("\nUnable to process parameters due to null value"); // Exception thrown if parameters are null
           // }

            //if (!Parameters.Contains(","))
            //{
             //   throw new GPLException("\n Unable to process MoveTo parameters due to syntax error.");
            //}

            processor = Parameters.Split(",");

           // if (processor[1] == "")
            //{
            //    throw new GPLException("\n Unable to process MoveTo parameters due to syntax error.");
            //}
          //  else 
            //{ 
                Array.ConvertAll(processor, s => int.Parse(s));
                ParamsInt = Array.ConvertAll(processor, s => int.Parse(s));
            //}
        }

        /// <summary>
        /// Method to ensure parameter list for moveto command contains no less than 2 parameters.
        /// </summary>
        /// <param name="ParameterList"></param>
        public override void ParseParameters(int[] parameterList)
        {
            if (parameterList.Length != 2)
            {
                throw new GPLException("Invalid number of parameters for MoveTo command"); // Exception thrown if incorrect number of parameters are inputted
            }
        }

        /// <summary>
        /// Set method for MoveTo.
        /// Method requires canvas object, command name and x and y axis values.
        /// </summary>
        /// <param name="c">Canvas object</param>
        /// <param name="Name">Command name</param>
        /// <param name="Parameters">x and y axis values</param>
        public void Set(Canvas c, String Name, String Parameters)
        {
            base.Set(c, "moveto", Parameters);
            try
            {
                this.ProcessParameters(Parameters, out int[] ParamsInt);
                this.ParseParameters(ParamsInt);
                this.xPos = ParamsInt[0];
                this.yPos = ParamsInt[1];
            }
            catch (GPLException ex)
            {
                c.DrawString(ex.Message);
            }
        }

        /// <summary>
        /// Overriding ToString method.
        /// </summary>
        /// <returns>Returns name of class and inputted parameters.</returns>
        public override string ToString()
        {
            return base.ToString() + "MoveTo" + this.xPos + " " + this.yPos;
        }

        /// <summary>
        /// Execute method for MoveTo that sets x and y to new position.
        /// </summary>
        /// <returns>Returns true boolean value.</returns>
        public override bool Execute()
        {
            c.MoveTo(xPos, yPos); //does the actual draw to the canvas class
            return true;
        }

        public override void ProcessParameters(string parameters, out int ParamsInt)
        {
            throw new NotImplementedException();
        }
    }
}
