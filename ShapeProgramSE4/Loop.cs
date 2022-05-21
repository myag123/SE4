using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Loop class extends command class.
    /// Loop class uses iterator object (following iterator design pattern) to allow the lines of code declared in a loop to be 
    /// iterated through a number of times depending on what the iteration value is set to.
    /// </summary>
    public class Loop : Command
    {
        private int iterateVal; // Fetching value so iterator will know how many times to iterate loop
        private string code;
        public List<string> loopLines = new List<string>(); // ArrayList to store lines of code inside loop
        String[] allCode;

        /// <summary>
        /// Method to set iterateVal, so iterator will know how many 
        /// times to iterate through declared loop.
        /// </summary>
        /// <param name="iterateVal">Number of times to iterate</param>
        public void SetIterateVal(int iterateVal)
        {
            this.iterateVal = iterateVal;
            Debug.WriteLine("IterateVal equals: " + iterateVal);
        }

        /// <summary>
        /// Method to return iteration value.
        /// </summary>
        /// <returns>Number of iterations required for loop</returns>
        public int GetIterateVal()
        {
            return iterateVal;
        }

        /// <summary>
        /// Method to each line of code inside loop.
        /// </summary>
        /// <param name="code">Line of code e.g. moveto x,50</param>
        public void SetLoopLines(string code)
        {
            this.code = code;
            loopLines.Add(code);
            Debug.WriteLine("SetLoopLines added in Loop.cs: " + code);
        }

        /// <summary>
        /// Method to get current loop lines.
        /// </summary>
        /// <returns></returns>
        public List<string> GetLoopLines()
        {
            return loopLines;
        }

        /// <summary>
        /// This method duplicates the code inside the of the loop by the number of iterations the user inputs.
        /// For example if the user entered loop 5 followed by lines of code - the lines of code inside the loop 
        /// would be duplicated 5 times to meet the needs of a simple loop. 
        /// </summary>
        public void DuplicateLoopLines()
        {
            string result = "";
            int lineTotal = loopLines.Count; // Storing loopLine count with original elements in

            for (int i = 0; i < loopLines.Count; i++) // Loop to group all elements of loopLines array together in a string for easier processing 
            { 
                result += loopLines[i].ToString() + "!"; // e.g. moveto x,50!circle 100!x = x + 10
            }

            string str = string.Concat(Enumerable.Repeat(result, iterateVal)); // Concatenating result from loop above - the value of iterateVal will decide how many times result is duplicated 

            loopLines.Clear(); // Clearing all elements from loopLines to allow the correct number of lines to be passed to ArrayList for loop to work
            allCode = str.Split("!"); // Splitting all elements of allCode string array by exclaimation mark for easier processing

            for (int i = 0; i < lineTotal * iterateVal; i++) // Loop to add each element of allCode string array to loopLines - lineTotal and iterateVal are multiplied so all elements of allCode is added to loopLines
            {
                if (!allCode[i].Equals("")) { loopLines.Add(allCode[i]); } // Ensure non null elements are added back to loopLines
            }

            Debug.WriteLine("new counter looplines" + loopLines.Count);
        }

        /// <summary>
        /// Creates an Iterator object and passes list to LoopIterator class.
        /// </summary>
        /// <returns>New LoopIterator List</returns>
        public IProgramIterator GetLoopLinesIterator()
        {
            return new LoopIterator(loopLines); // loopLines is passed to LoopIterator for iteration to begin
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}
