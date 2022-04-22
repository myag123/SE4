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

        /// <summary>
        /// Constructor for DrawCommand
        /// </summary>
        public DrawCommand()
        {

        }

        /// <summary>
        /// DrawCommand method will draw command to canvas.
        /// </summary>
        public DrawCommand(Canvas c)
        {
            this.c = c;
        }

        /// <summary>
        /// Set method to set parameters passed in.
        /// </summary>
        /// <param name="c">Object of canvas class</param>
        /// <param name="Name">Name of command</param>
        /// <param name="Params">Draw parameters to draw on canvas</param>
        public void Set(Canvas c, String Name, String Params)
        {
            base.Set(Name, Params);
            this.c = c;
        }
        /// <summary>
        /// Canvas method to set and get canvas.
        /// </summary>
        public Canvas canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        /// <summary>
        /// Execute method to process and parse parameters passed in from user.
        /// </summary>
        /// <returns></returns>
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
