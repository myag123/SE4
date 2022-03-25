using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;

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
        int oldX, oldY;


        /// <summary>
        /// Parser class takes in user input and splits it up to store inside different arrays.
        /// </summary>
        /// <param name="input">Input from user - usually command syntax followed by values.</param>
        /// <param name="c">Canvas is passed into method for pen to draw on it</param>
        public Parser(String input, Canvas c)
        {
            if (input.Equals("clear")) //if user inputs clear, then canvas will be cleared
            {
                c.Clear(); //make colour as constant in method!
                Debug.WriteLine("Clear canvas");
            }
            else
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
                        throw new ApplicationException("Incorrect number of parameters in syntax, please enter width or x axis & height or y axis only."); //error raised
                    }

                    Debug.WriteLine(parameters.Length); //for debugging

                    //assigning variables to parameters
                    toX = parameters[0];
                    toY = parameters[1];

                    oldX = Int32.Parse(toX);
                    oldY = Int32.Parse(toY);

                    Debug.WriteLine("X: " + toX + " Y: " + toY); //for debugging
                }

                if (command.Equals("drawto") == true) //if command equal drawto then x and y parameters are converted to integers and line is drawn 
                {
                    c.drawLine(Int32.Parse(toX), Int32.Parse(toY));
                    c.moveTurtle(oldY,oldX,Int32.Parse(toX), Int32.Parse(toY));
                }

                if (command.Equals("drawsquare") == true) //drawsquare command will draw a square based on inputted values
                {
                    if (toX == toY)
                    {
                        c.drawSquare(Int32.Parse(toX), Int32.Parse(toY)); //calling DrawSquare method to canvas
                    }
                    else
                    {
                        Debug.WriteLine("Invalid"); 
                        throw new ApplicationException("\n To draw a square the height and width must be the same values.");
                    }
                }

                if (command.Equals("drawtriangle") == true)
                {
                    c.drawTriangle(Int32.Parse(toX), Int32.Parse(toY));
                    Debug.WriteLine("triangle drawn?");
                }

                    /* if (command.Equals("drawcircle") == true)//needs work
                     {
                         c.drawCircle(Int32.Parse(toX));
                         Debug.WriteLine("circle drawn?");
                     }    */

                    if (command.Equals("moveto") == true) //moveto command will move pen point to inputted values
                {
                    c.moveTo(Int32.Parse(toX), Int32.Parse(toY));  
                    c.moveTurtle(oldY, oldX, Int32.Parse(toX), Int32.Parse(toY));
                    Debug.WriteLine("moveto inputted");
                }

                if (command.Equals("reset")) //when user types reset pen will move to start position
                { 
                    
                }

            }
        }

    }
}
