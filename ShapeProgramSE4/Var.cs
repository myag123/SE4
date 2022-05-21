using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Var class for variables. Extends command class.
    /// </summary>
    public class Var : Command
    {
        private String name;
        private int value;
        int position;
        private ArrayList varNameList = new ArrayList();
        private ArrayList varValueList = new ArrayList();

        /// <summary>
        /// Method to set var name.
        /// </summary>
        public void SetName(String name)
        {
            this.name = name;
            varNameList.Add(name);
        }

        /// <summary>
        /// Method to get var name.
        /// </summary>
        public string GetName()
        {
            return this.name;
        }

        /// <summary>
        /// Method to set value.
        /// </summary>
        /// <param name="value">Value if variable</param>
        public void SetValue(int value)
        {
            this.value = value;
            varValueList.Add(value);
        }

        public int GetValue(string varName)
        {
            int position = varValueList.IndexOf(varName);
            string result = varValueList[position].ToString();
            value = Int32.Parse(result);
            return this.value;
        }

        /// <summary>
        /// Constructor for Var
        /// </summary>
        public Var()
        { 
        
        }
        
        /// <summary>
        /// Execute method for Var.
        /// </summary>
        /// <returns>Returns true/.</returns>
        public override bool Execute()
        {
            return true;
        }
      
    }
}
