using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    public class CommandFactory
    {
        /// <summary>
        /// Make command method returns object of command user types in.
        /// If command isn't found then an exception is raised.
        /// </summary>
        /// <param name="command">Command name</param>
        /// <returns></returns>
        public Command MakeCommand(String command)
        { 
            if(command.Equals("moveto"))
            {
                return new MoveTo();
            }
            if (command.Equals("drawto"))
            {
                return new DrawTo();
            }

            throw new ApplicationException("CommandFactory error: " + command + " is not valid.");
          
        }
    }
}
