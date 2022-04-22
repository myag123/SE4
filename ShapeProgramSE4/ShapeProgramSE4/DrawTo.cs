using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Class for command drawTo.
    /// Command factory will create an object of this class.
    /// 
    /// </summary>
    class DrawTo : DrawCommand
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
        /// Constructor method for DrawTo
        /// </summary>
        public DrawTo()
        {

        }

        /// <summary>
        /// Method to draw line to canvas using values of x axis and y axis
        /// </summary>
        /// <param name="c">Canvas that pen will draw line between axis coordinates given</param>
        /// <param name="x">x axis position</param>
        /// <param name="y">y axis positon</param>
        public DrawTo(Canvas c, int x, int y) : base(c)
        {
            Name = "drawto";
            this.xPos = x;
            this.yPos = y;
        }

        /// <summary>
        /// Method to split input by comma and then convert input array to integer.
        /// </summary>
        /// <param name="Parameters"></param>
        /// <param name="ParamsInt"></param>
        public override void ProcessParameters(String Parameters, out int[] ParamsInt)
        {
            String[] processor;
            processor = Parameters.Split(",");

            Array.ConvertAll(processor, s => int.Parse(s));
            ParamsInt = Array.ConvertAll(processor, s => int.Parse(s));
            Debug.WriteLine("ParamsInt[0]:" + ParamsInt[0] + "ParamsInt[1]:" + ParamsInt[1]);
        }

        /// <summary>
        /// Method to ensure parameter list for drawto command contains no less than 2 parameters.
        /// </summary>
        /// <param name="ParameterList"></param>
        public override void ParseParameters(int[] parameterList)
        {
            if (parameterList.Length != 2)
            {
                //add gplexception class
                throw new ApplicationException("Invalid number of parameters in drawTo");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="Name"></param>
        /// <param name="Parameters"></param>
        public void Set(Canvas c, String Name, String Parameters)
        {
            base.Set(c, "drawto", Parameters); 
            this.ProcessParameters(Parameters, out int[] ParamsInt);
            this.ParseParameters(ParamsInt);
            this.xPos = ParamsInt[0];
            this.yPos = ParamsInt[1];
        }

        public override string ToString()
        {
            return base.ToString() + "DrawTo" + this.xPos + " " + this.yPos;
        }

        public override bool Execute()
        {
            c.drawTo(xPos, yPos); //does the actual draw to the canvas class
            return true;
        }
    }
}
