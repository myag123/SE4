using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Command interface creates objects, that are in the hierarchy. 
    /// Methods conform to template.
    /// Part of factory design pattern.
    /// </summary>
    interface CommandInterface
    {
        /// <summary>
        /// Method to set single word commands, not followed by any parameters.
        /// </summary>
        /// <param name="command">Command name</param>
        void Set(String command);

        /// <summary>
        /// Method to set command followed by string parameters.
        /// </summary>
        /// <param name="command">Command name</param>
        /// <param name="list">Parameters set to command</param>
        void Set(String command, String list);

        /// <summary>
        /// Depending on result decided whether the next command should be executed
        /// </summary>
        public abstract bool Execute();

    }
}
