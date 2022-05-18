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

        public string MethodName
        {
            get => methodName;
            set => methodName = value;
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
