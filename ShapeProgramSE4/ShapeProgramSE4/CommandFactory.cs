using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Command Factory for commands.
    /// Following factory design pattern, to allow dependencies to come from this class.
    /// When command is passed to MakeCommand method a new object of a command is returned.
    /// 
    /// </summary>
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
            if (command.Equals("moveto")) { return new MoveTo(); }
            if (command.Equals("drawto")) { return new DrawTo(); }
            if (command.Equals("clear"))  { return new Clear(); }
            if (command.Equals("reset"))  { return new Reset(); }
            if (command.Equals("pen"))    { return new PenColor(); }
            if (command.Equals("circle")) { return new DrawCircle(); }
            if (command.Equals("rectangle")) { return new DrawRectangle(); }
            if (command.Equals("square")) { return new DrawSquare(); }
            if (command.Equals("triangle")) { return new DrawTriangle(); }
            if (command.Equals("fill")) { return new Fill(); }
            if(command.Equals("var")) { Debug.WriteLine("object var created"); return new Var(); }
            if (command.Equals("mymethod")) { Debug.WriteLine("object mymethod created"); return new Method(); }
            throw new ApplicationException("CommandFactory error: " + command + " is not valid.");
          
        }

    }
}
