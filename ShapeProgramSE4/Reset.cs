using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    public class Reset : DrawCommand
    {
        public Reset()
        { 
        
        }

        /// <summary>
        /// Method to set Clear command. 
        /// </summary>
        /// <param name="c">Canvas</param>
        /// <param name="name">Name of command</param>
        public void Set(Canvas c, String name)
        {
            base.Set(c, name);
        }

        /// <summary>
        /// Execute method that resets pen to original points.
        /// </summary>
        /// <returns>Returns true boolean value.</returns>
        public override bool Execute()
        {
            c.Reset();
            return true;
        }

        /// <summary>
        /// Overriding ToString method.
        /// </summary>
        /// <returns>Returns name of class.</returns>
        public override string ToString()
        {
            return base.ToString() + "Reset";
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
