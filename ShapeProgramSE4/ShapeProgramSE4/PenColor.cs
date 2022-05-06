using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    class PenColor : DrawCommand
    {
        String name;
        private Color color;

        /// <summary>
        /// Method to get and set color.
        /// </summary>
        public Color Color
        {
            get => color;
            set => color = value;
        }

        /// <summary>
        /// Constructor for penColor
        /// </summary>
        public PenColor()
        { 
        
        }

        /// <summary>
        /// Set method for PenColor.
        /// Method requires canvas object, name of command and color to be passed in.
        /// </summary>
        /// <param name="c">Canvas object</param>
        /// <param name="name">Name of command</param>
        /// <param name="color">Color of Pen</param>
        public void Set(Canvas c, String name, Color color)
        {
            base.Set(c, name, color);
            this.color = color;
        }

        /// <summary>
        /// Overriding ToString method.
        /// </summary>
        /// <returns>Returns name of class and inputted parameters.</returns>
        public override string ToString()
        {
            return base.ToString() + "PenCol" + this.color; 
        }


        /// <summary>
        /// Execute method for PenColor.
        /// Passes color to Pen to draw on cnavas.
        /// </summary>
        /// <returns>Returns true boolean value.</returns>
        public override bool Execute()
        {
            Debug.WriteLine("PenColour execute method");
            c.PenColour(color);
            return true;
        }

        public override void ProcessParameters(string parameters, out int ParamsInt)
        {
            throw new NotImplementedException();
        }

        public override void ParseParameters(int[] parameterList)
        {
            throw new NotImplementedException();
        }

        public override void ProcessParameters(string parameters, out int[] ParamsInt)
        {
            throw new NotImplementedException();
        }
    }
}
