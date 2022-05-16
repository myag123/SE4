using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    public abstract class Command : CommandInterface
    {
        //all commands require a name followed by a list of parameters e.g. MoveTo 100,130
        private String name = "";
        private String parameterList;

        protected Command()
        { 
        
        }

        //Getter and setter methods for name
        public String Name
        {
            get { return Name; }
            set { Name = value; }
        }

        public String ParameterList
        {
            get { return ParameterList; }
        }

        public abstract bool Execute();

        public void Set(String name, String ParameterList)
        {
            this.name = name;
            this.parameterList = ParameterList;
        }
    }
}
