using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// MethodIterator that extends IMethodIterator the iterator object.
    /// methodLines ArrayList from Method class is passed into MethodIterator and set to a readonly object.
    /// This allows methodLines to be iterated through a range of different methods to get
    /// </summary>
    public class MethodIterator : IMethodIterator // Iterator object
    {
        private readonly ArrayList methodLines;
        private int position;

        /// <summary>
        /// Constructor for MethodIterator - requires an ArrayList to be passed into the method.
        /// </summary>
        /// <param name="methodLines">ArrayList containing values</param>
        public MethodIterator(ArrayList methodLines) 
        {
            this.methodLines = methodLines;
            this.position = -1; // Setting position to -1 
        }

        public string Current => methodLines[position].ToString();


        /// <summary>
        /// Boolean method MoveNext will return true if the methodLines count is not equal to 0
        /// Method is called in Parser class to allow each element of methodLines to be parsed through the Parser class
        /// after a method has been called.
        /// </summary>
        /// <returns>true or false</returns>
        public bool MoveNext()
        {
            if (++position == methodLines.Count) return false;
            return true;
        }
    }
}
