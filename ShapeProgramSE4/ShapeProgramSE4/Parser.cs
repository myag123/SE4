using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ShapeProgramSE4
{
    /// <summary>
    /// This class will get the user input from the text boxes on the form interface and split them up into 
    /// arrays and then process the information inside the arrays so a shape can be drawn on the screen depending 
    /// on the users input.
    /// </summary>
    class Parser
    {
        //Creating variables for Parser
        String command, toX, toY;
        String[] splitter;
        String[] parameters;

        /// <summary>
        /// Parser class takes in user input and splits it up to store inside different arrays.
        /// </summary>
        /// <param name="input">Input from user - usually command syntax followed by values.</param>
        public Parser(String input)
        {
            splitter = input.Split(" "); //split up user input by space

            for (int i = 0; i < splitter.Length; i++) //loop to assign user input to arrays
            {
                Debug.WriteLine(splitter[i]);
                command = splitter[i];
                i++;
                parameters = splitter[i].Split(",");

                if (parameters.Length != 2) //if user inputs more than 2 width/height values then application error occurs
                {
                    throw new ApplicationException("Incorrect number of parameters in syntax, please enter width or x axis & height or y axis only.");
                }

                Debug.WriteLine(parameters.Length); //for debugging

                //assigning variables to parameters
                toX = parameters[0];
                toY = parameters[1];

                Debug.WriteLine("X: " + toX + " Y: " + toY); //for debugging
            }

            //converting to integers
            Int32.Parse(toX);
            Int32.Parse(toY);

           /* if (command.Equals("drawto") == true)
            {

            }*/
        }

    }
}
