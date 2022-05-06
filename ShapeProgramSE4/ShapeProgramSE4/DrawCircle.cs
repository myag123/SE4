using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Class to draw Circle which extends draw command.
    /// </summary>
    class DrawCircle : DrawCommand
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
            //exception needs to be handles here plesae reaserch

            ParamsInt = Int32.Parse(parameters);
        }

        /// <summary>
        /// Method to ....
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
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Execute()
        {
            c.DrawCircle(radius); //does the actual draw to the canvas class
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
