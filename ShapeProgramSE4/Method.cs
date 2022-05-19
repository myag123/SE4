using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Method class that extends command.
    /// The class stores the name of method user declares.
    /// </summary>
    public class Method : Command
    {
        private string methodName;
        public ArrayList methodNameList = new ArrayList();

        /// <summary>
        /// Method to set method name.
        /// </summary>
        public void SetName(String methodName)
        {
            this.methodName = methodName;
            methodNameList.Add(methodName);
        }

        public override bool Execute()
        {
            return true;
        }
    }
}
