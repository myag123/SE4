using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Command interface creates objects, that are in the hierarchy. 
    /// Methods conform to template.
    /// </summary>
    interface CommandInterface
    {
        /// <summary>
        /// Method to set each command, needs command name followed by list of parameters.
        /// </summary>
        /// <param name="command">Command Name</param>
        /// <param name="list">Parameters passed to command name</param>
        //void Set(String command, params String[] list);

        /// <summary>
        /// Method to set single word commands, not followed by any parameters.
        /// </summary>
        /// <param name="command"></param>
        void Set(String command);
        
        
        /// <summary>
        /// Method to set command followed by string parameters.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="list"></param>
        void Set(String command, String list);

        /// <summary>
        /// Depending on result decided whether the next command should be executed
        /// </summary>
        public abstract bool Execute();

    }
}
