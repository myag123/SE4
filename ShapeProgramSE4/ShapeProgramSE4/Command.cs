using System;
using System.Collections.Generic;
using System.Drawing;
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
        private String name = "";
        private String parameterList;
        private String expression;
        private int varValue;

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
        /// Expression e.g. = + - *
        /// For commands such as variables.
        /// </summary>
        public String Expression
        {
            get { return Expression; }
        }

        /// <summary>
        /// Return value of variable
        /// </summary>
        public int VarValue
        {
            get { return VarValue; }
            set { VarValue = value; }
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

        /// <summary>
        /// Set method for short commands that don't require parameters.
        /// </summary>
        /// <param name="name">Command name</param>
        public void Set(String name)
        {
            this.name = name;
        }

        public void Set(String name, int varValue)
        {
            this.name = name;
            this.varValue = varValue;
        }

    }
}
