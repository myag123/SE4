using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Clear class that extends draw command.
    /// This class sets values of and executes clear command.
    /// </summary>
    class Clear : DrawCommand
    {
        /// <summary>
        /// Constructor for clear class.
        /// </summary>
        public Clear()
        { 
        
        }

        /// <summary>
        /// Method to set Clear command.
        /// Method requires canvas object and name of command to be passed in.
        /// </summary>
        /// <param name="c">Canvas</param>
        /// <param name="name">Name of command</param>
        public void Set(Canvas c, String name)
        {
            base.Set(c,name);
        }

        /// <summary>
        /// Execute method for clear.
        /// Method calls clear method in the canvas class to set background colour to default colour.
        /// </summary>
        /// <returns>Returns true boolean value.</returns>
        public override bool Execute()
        {
            c.Clear(); 
            return true;
        }

        /// <summary>
        /// Overriding ToString method.
        /// </summary>
        /// <returns>Returns name of class.</returns>
        public override string ToString()
        {
            return base.ToString() + "Clear";
        }

        public override void ParseParameters(int[] parameterList)
        {
            throw new NotImplementedException();
        }

        public override void ProcessParameters(string parameters, out int[] ParamsInt)
        {
            throw new NotImplementedException();
        }

        public override void ProcessParameters(string parameters, out int ParamsInt)
        {
            throw new NotImplementedException();
        }
    }
}
