using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    class Method : Command
    {
        private string methodName;

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
        working without added in parameters*/
        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}
