using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Iterator interface contains methods for fetching iterators.
    /// </summary>
    public interface IMethodIterator 
    {
        /// <summary>
        /// Method that gets current element
        /// </summary>
        string Current { get; }

        /// <summary>
        /// Method to move iterator to next element.
        /// </summary>
        bool MoveNext();
    }
}
