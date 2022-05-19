using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Graphical programming language exception class.
    /// Extends exception class, to make it easier to recognise exceptions that are GPL program related.
    /// </summary>
   public class GPLException : Exception 
    {
        /// <summary>
        /// Constructor to extend base class of £xception to allow creation of custom error message.
        /// </summary>
        /// <param name="msg">Error message</param>
        public GPLException(String msg) : base(msg)
        { 
        
        }

        

    }
}
