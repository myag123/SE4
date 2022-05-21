using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Method class that is an aggregate object for the iterator pattern.
    /// The class stores the name and lines of code in method user declares.
    /// Lines of code in method is added to methodLines ArrayList which is then 
    /// passed to the MethodIterator class.
    /// </summary>
    public class Method : Command
    {
        private string methodName;
        private string methodCode;

        // Declaring ArrayLists to store method information
        public ArrayList methodNameList = new ArrayList();
        public ArrayList methodLines = new ArrayList();

        /// <summary>
        /// Method to set method name.
        /// </summary>
        public void SetName(String methodName)
        {
            this.methodName = methodName;
            methodNameList.Add(methodName);
            Debug.WriteLine("SetName added in Method.cs: " + methodName);
        }

        /// <summary>
        /// Method to set code inside of declared method.
        /// </summary>
        /// <param name="code">String of code</param>
        public void SetMethodLine(String code)
        {
            this.methodCode = code;
            methodLines.Add(code);
            Debug.WriteLine("SetMethodLine added in Method.cs: " + code);
        }

        /// <summary>
        /// Creates an Iterator object and passes list to MethodIterator class.
        /// </summary>
        /// <returns>New MethodIterator ArrayList</returns>
        public IProgramIterator GetMethodLinesIterator()
         {
             return new MethodIterator(methodLines);
         }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}

