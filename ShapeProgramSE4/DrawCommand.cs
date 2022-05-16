using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Abstract class extending command class.
    /// All commands that draw on the canvas will inherit this class.
    /// </summary>
    public abstract class DrawCommand : Command
    {
        protected Canvas c;

        /// <summary>
        /// Abstract method to contain parameters for certain commands.
        /// </summary>
        /// <param name="ParameterList">Array list to store parameters.</param>
        public abstract void ParseParameters(int[] parameterList);

        /// <summary>
        /// Abstract method to process parameters from string to int.
        /// </summary>
        /// <param name="parameters">String parameters e.g. drawto 100,200 to store string inputted.</param>
        /// <param name="ParamsInt"></param>
        public abstract void ProcessParameters(String parameters, out int[] ParamsInt);

        public DrawCommand()
        {

        }

        /// <summary>
        /// DrawCommand method will draw command to canvas
        /// </summary>
        public DrawCommand(Canvas c)
        {
            this.c = c;
        }

        public void Set(Canvas c, String Name, String Params)
        {
            base.Set(Name, Params);
            this.c = c;
        }

        public Canvas canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        public override bool Execute()
        {
            this.ProcessParameters(this.ParameterList, out int[] ParamsInt);
            this.ParseParameters(ParamsInt);
            return true;
        }

        public override string ToString()
        {
            return base.ToString() + "DrawCommand";
        }
    }
}
