﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Graphical programming language exception class.
    /// Extends exception class
    /// </summary>
   public class GPLException : Exception
    {
        public GPLException(String msg) : base(msg)
        { 
        
        }
    }
}
