using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShapeProgramSE4
{
    class MoveTo : DrawCommand
    {
        private int xPos, yPos;

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

        public MoveTo(Canvas c, int x, int y) : base(c)
        {
            Name = "moveto";
            this.xPos = x;
            this.yPos = y;
        }

        /// <summary>
        /// Constructor method for MoveTo
        /// </summary>
        public MoveTo()
        {

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
        /// Method to ensure parameter list for moveto command contains no less than 2 parameters.
        /// </summary>
        /// <param name="ParameterList"></param>
        public override void ParseParameters(int[] parameterList)
        {
            if (parameterList.Length != 2)
            {
                throw new ApplicationException("Invalid number of parameters in moveTo"); //add gplexception class later
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
            base.Set(c, "moveto", Parameters);
            this.ProcessParameters(Parameters, out int[] ParamsInt);
            this.ParseParameters(ParamsInt);
            this.xPos = ParamsInt[0];
            this.yPos = ParamsInt[1];
        }

        public override string ToString()
        {
            return base.ToString() + "MoveTo" + this.xPos + " " + this.yPos;
        }

        public override bool Execute()
        {
            c.moveTo(xPos, yPos); //does the actual draw to the canvas class
            return true;
        }
    }
}
