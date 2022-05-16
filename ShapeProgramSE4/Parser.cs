using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Parser class requires canvas object to be passed into its constructor method in order to draw command on Canvas.
    /// Parser class has a command method which requires user input of command and will return a true or false result.
    /// Parser class is overall responsible for splitting up a range of commands into arrays and passing them to a 
    /// command class so the command can be eventually drawn on the canvas.
    /// </summary>
    class Parser
    {
        String[] splitter; //array string to split up command 
        String name, coords;

        CommandFactory CommFactory = new CommandFactory();
        Canvas canvas;

        /// <summary>
        /// Constructor for parser class requires Canvas to be passed in.
        /// </summary>
        /// <param name="c">Object of canvas class.</param>
        public Parser(Canvas c)
        {
           canvas = c;
        }

        /// <summary>
        /// ParseCommand method
        /// </summary>
        /// <param name="command"></param>
        /// <param name="execute"></param>
        /// <returns></returns>
        public Command ParseCommand(String command, bool execute)
        {
                splitter = command.Split(" "); //split up user input by space drawto 100,150 .. drawto||100,150

                for (int i = 0; i < splitter.Length; i++) //loop to assign user input to arrays
                {
                    Debug.WriteLine(splitter[i]);
                    command = splitter[i];
                    i++;
                }

                //if command equal drawto then x and y parameters are converted to integers and line is drawn 
                if (command.Equals("drawto") == true) //if command equals drawto then command is split into arrays and passed to drawto class
                {
                    DrawTo dtCommand = (DrawTo) CommFactory.MakeCommand("drawto"); //creating drawto command from factory class
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    dtCommand.Set(canvas, name, coords); //calling set method of DrawTo class to draw on canavs
                    if (execute == true) { dtCommand.Execute(); return dtCommand; } //if execute method returns true then return command
                }
                if (command.Equals("moveto") == true)
                {
                    MoveTo mtCommand = (MoveTo)CommFactory.MakeCommand("moveto");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    mtCommand.Set(canvas, name, coords); //calling set method of MoveTo class to draw on canavs
                    if (execute == true) { mtCommand.Execute(); return mtCommand; } //if execute method returns true then return command
                }
                else
                {
                    throw new ApplicationException("wrong"); //add in proper exception later
                }

            return null;
        }
    }
}
