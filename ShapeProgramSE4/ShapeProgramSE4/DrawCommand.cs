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
        protected Color col;

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
        public abstract void ProcessParameters(String Parameters, out int[] ParamsInt);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="ParamsInt"></param>
        public abstract void ProcessParameters(String Parameters, out int ParamsInt);

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
        /// Method to set short commands with no parameters required. 
        /// For example; clear, reset etc.
        /// </summary>
        /// <param name="c">Canvas</param>
        /// <param name="Name">Command name</param>
        public void Set(Canvas c, String Name)
        {
            base.Set(Name);
            this.c = c;
        }

        /// <summary>
        /// Set method to set parameters passed in for a usual command followed by a list of parameters.
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
        /// Set method to set parameters passed in for colour.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="Name"></param>
        /// <param name="Colour"></param>
        public void Set(Canvas c, String Name, Color Colour)
        {
            base.Set(Name);
            this.c = c;
            this.col = Colour;
        }

        /// <summary>
        /// Canvas method to set and get canvas.
        /// </summary>
        public Canvas canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        public Color colour
        {
            get { return colour; }
            set { colour = value; }
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
