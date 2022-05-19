using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    public class Method : Command
    {
        private string methodName;
        public ArrayList methodNameList = new ArrayList();
        public ArrayList methodList = new ArrayList();
        public String[] methList;

        /// <summary>
        /// Method to set method name.
        /// </summary>
        public void SetName(String methodName)
        {
            this.methodName = methodName;
            methodNameList.Add(methodName);
        }

        /*
         method mymethod
          moveto 100,250
          circle 50
         endmethod
        mymethod -- to call method
        working without added in parameters
        think abut this 
        get the prgram to look for the methodname and then save everything on the line inside the method 
        */
        public override bool Execute()
        {
            return true;
        }
    }
}
