using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// LoopIterator that extends IProgramIterator the iterator object.
    /// loopLines ArrayList from Loop class is passed into LoopIterator and set to a readonly object.
    /// This allows the contents of the List loopLines to be kept track of while methods are used to iterate through the elements inside.
    /// </summary>
    class LoopIterator : IProgramIterator // Iterator object
    {
        private readonly List<string> loopLines;
        private int position;

        /// <summary>
        /// Constructor for LoopIterator
        /// </summary>
        public LoopIterator(List<string> loopLines)
        {
            this.loopLines = loopLines;
            this.position = -1; // Setting position to -1 so MoveNext can find the first element of list
        }

        /// <summary>
        /// Method to get the current element in loopLines list
        /// </summary>
        public string Current => loopLines[position].ToString();

        /// <summary>
        /// Boolean method MoveNext will return true if the loopLines count is not equal to 0
        /// Loop is called in Parser class to allow each element of loopLines to be parsed through the Parser class
        /// after a loop has been declared.
        /// </summary>
        /// <returns>true or false</returns>
        public bool MoveNext()
        {
            if (++position == loopLines.Count) return false;
            return true;
        }
    }
}
