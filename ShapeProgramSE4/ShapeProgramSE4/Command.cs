using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Abstract class Command extends CommandInterface class.
    /// This class contains setter and getter methods that every command must require.
    /// For example all commands require a name followed by a list of parameters except short command like 'clear'.
    /// </summary>
    public abstract class Command : CommandInterface
    {
        //all commands require a name followed by a list of parameters e.g. MoveTo 100,130
        private String name = "";
        private String parameterList;

        protected Command()
        { 
        
        }

        /// <summary>
        /// Public String to set and get Name of command.
        /// </summary>
        public String Name
        {
            get { return Name; }
            set { Name = value; }
        }

        /// <summary>
        /// Public String to get ParameterList from command.
        /// </summary>
        public String ParameterList
        {
            get { return ParameterList; }
        }

        /// <summary>
        /// Abstract method to Execute.
        /// All commands must have an execute method, which will be overriden in classes that inherit this.
        /// </summary>
        /// <returns>
        /// False means executions has failed. 
        /// True means execution was successful.
        /// </returns>
        public abstract bool Execute();

        /// <summary>
        /// Set method to set command name and parameterlist.
        /// </summary>
        /// <param name="name">Name of command. e.g. MoveTo</param>
        /// <param name="ParameterList">List of parameters e.g. 150,123</param>
        public void Set(String name, String ParameterList)
        {
            this.name = name;
            this.parameterList = ParameterList;
        }
    }
}
