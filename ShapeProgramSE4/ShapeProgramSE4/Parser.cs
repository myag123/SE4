using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Data;

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
        String name, coords, colour, command, expression;
        int value, varCounter = 0;
        ArrayList varNameList = new ArrayList();
        ArrayList varValueList = new ArrayList();

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


        public void CheckCommands(String Command, String Parameters, bool result)
        {
                if (Command.Contains("drawto") && Parameters.Contains(","))
                {
                    String[] pCheck = Parameters.Split(",");
                    int p1 = Int32.Parse(pCheck[0]);
                    int p2 = Int32.Parse(pCheck[1]);
                    result = true;
                }

        }

        /// <summary>
        /// Command method to ParseCommand from inputted lines of code from the user.
        /// </summary>
        /// <param name="command">Command parameter e.g. MoveTo 100,200</param>
        /// <param name="execute">Boolean True/False</param>
        /// <returns>Returns true if successful and false if failed.</returns>
        public Command ParseCommand(String line, bool execute)
        {
            splitter = line.Split(" "); // Split up user input by space drawto 100,150 .. drawto||100,150

            if(line.Length == 0) // If user enters in blank line, then exception is thrown
            {
                throw new GPLException("Please enter a command. \nCommand was blank.");
            }

            command = splitter[0]; // Setting 1st position of array to equal command variable, for if statements below
            

            for (int i = 0; i < splitter.Length;) // Loop until index is less than length of splitter array
            {
                Debug.WriteLine(splitter[i]);
                i++;
            }

            try
            {
                // If user declares full variable e.g. var apple = 40
                if (command.Equals("var") == true && splitter.Length.Equals(4) == true) 
                {
                    Var varCommand = (Var)CommFactory.MakeCommand("var"); // Creating object of Var class

                    name = splitter[1].ToString(); 
                    value = Int32.Parse(splitter[3]);

                    if (varCommand.varNameList.Contains(name)) // Check to see if variable name exists, if so execption is thrown
                    {
                        throw new GPLException("Variable " + name + " already exists. \nPlease declare a different named variable");
                    }
                    else
                    {
                        varCommand.Set(name, value); // Passing name and value to var class

                        // Adding name and value of declared variable to array lists
                        varNameList.Add(name);
                        varValueList.Add(value);

                        // Storing position of name and value in array lists
                        varNameList[varCounter] = name;
                        varValueList[varCounter] = value;
                        varCounter++; // Increasing counter for next variable
                        
                        if (execute == true) { varCommand.Execute(); }
                    }
                }

                // If user declares variable without setting value e.g. var apple
                if (command.Equals("var") == true && splitter.Length.Equals(2) == true) 
                {
                    Var varCommand = (Var)CommFactory.MakeCommand("var"); 

                    name = splitter[1].ToString();
                    value = 0; // Initialising variable declared so it isnt null

                    if (varNameList.Contains(name)) // Check to see if variable name exists.
                    {
                        throw new GPLException("Variable " + name + " already exists. \nPlease declare a different named variable");
                    }
                    else
                    {
                        varCommand.Set(name, value); // Passing name and value to var class
                        
                        // Adding name and value of declared variable to array lists
                        varNameList.Add(name);
                        varValueList.Add(value);

                        // Storing position of name and value in array lists
                        varNameList[varCounter] = name;
                        varValueList[varCounter] = value;
                        varCounter++; // Increasing counter for next variable

                        if (execute == true) { varCommand.Execute(); }
                    }

                }

                // If user sets value of variable after it has been declared e.g. apple = 50 || apple = 50 * 5
                if (varNameList.Contains(splitter[0]) == true) 
                {
                    Debug.WriteLine("var found " + splitter[0].ToString()); // For debugger

                    int position, length;
                    Var varCommand = (Var)CommFactory.MakeCommand("var");

                    length = splitter.Length; // Getting length of splitter array

                    // If user sets the value of variable to a different number e.g. apple = 50
                    if (splitter[1].Equals("=") && length.Equals(3))
                    {
                        Debug.WriteLine("var " + splitter[0].ToString() + " length of var = 3");

                        name = splitter[0].ToString();
                        value = Int32.Parse(splitter[2]);

                        position = varNameList.IndexOf(splitter[0]); // Finds the position of the inputted variable
                        varValueList[position] = splitter[2];  // Sets the value of the variable in array list

                        varCommand.SetName(name);
                        varCommand.SetValue(value);

                        if (execute == true) { varCommand.Execute(); }
                    }
                    else if (splitter[1].Equals("=") == true && length > 3) // If user sets value of a variable by calculation e.g. apple = 50 * 5
                    {
                        Debug.WriteLine("var " + splitter[0].ToString() + " length of var more than 3");

                        name = splitter[0];

                        length = length - 2; // Minusing 2 from length to start from equals sign of splitter array

                        ArraySegment<string> arrGetCalc = new ArraySegment<string>(splitter, 2, length); // Gets the contents of array after = sign to work out calculation
                        
                        CalculateArrayVal(splitter[0], arrGetCalc, out int result); // Passes calculation to CalculateArrayVal method and returns sum of calculation
                        position = varNameList.IndexOf(splitter[0]); // Finds the position of the inputted variable
                        varValueList[position] = result;  // Sets the value of the variable in array list
                        value = result;

                        varCommand.SetName(name);
                        varCommand.SetValue(value);
                        Debug.WriteLine("var " + splitter[0].ToString() + " new calculation = " + result);

                        if (execute == true) { varCommand.Execute(); }
                    }
                    else
                    {
                        throw new GPLException("Invalid syntax to set variable");
                    }
                }
                else if (command.Equals("drawto") == true) //if command equals drawto then command is split into arrays and passed to drawto class
                {
                    DrawTo dtCommand = (DrawTo)CommFactory.MakeCommand("drawto"); //creating drawto command from factory class
                    name = splitter[0].ToString();
                    try { coords = splitter[1].ToString(); }
                    catch (IndexOutOfRangeException ex) { canvas.DrawString(ex.Message); }
                    coords = splitter[1].ToString();

                    dtCommand.Set(canvas, name, coords); //calling set method of DrawTo class to draw on canavs
                    if (execute == true) { dtCommand.Execute(); return dtCommand; } //if execute method returns true then return command
                }
                else if (command.Equals("moveto") == true)
                {
                    MoveTo mtCommand = (MoveTo)CommFactory.MakeCommand("moveto");
                    name = splitter[0].ToString();
                    try { coords = splitter[1].ToString(); }
                    catch (IndexOutOfRangeException ex) { canvas.DrawString(ex.Message); }
                    mtCommand.Set(canvas, name, coords); //calling set method of MoveTo class to draw on canavs
                    if (execute == true) { mtCommand.Execute(); return mtCommand; } //if execute method returns true then return command
                }
                else if (command.Equals("clear") == true)
                {
                    Clear clrComannd = (Clear)CommFactory.MakeCommand("clear");
                    name = splitter[0].ToString();
                    clrComannd.Set(canvas, name);
                    if (execute == true) { clrComannd.Execute(); return clrComannd; }
                }
                else if (command.Equals("reset") == true)
                {
                    Reset rstCommand = (Reset)CommFactory.MakeCommand("reset");
                    name = splitter[0].ToString();
                    rstCommand.Set(canvas, name);
                    if (execute == true) { rstCommand.Execute(); return rstCommand; }
                }
                else if (command.Equals("pen") == true)
                {
                    PenColor penCommand = (PenColor)CommFactory.MakeCommand("pen");
                    name = splitter[0].ToString();
                  //  if (colour == null) { throw new GPLException("No colour specified for Pen."); }
                    colour = splitter[1].ToString();
                    Color mycol = ColorTranslator.FromHtml(colour); //translating string to color //add exeception handling
                                                                    //  if (mycol)
                    Debug.WriteLine("Colour translated: " + mycol);
                    penCommand.Set(canvas, name, mycol);
                    if (execute == true) { penCommand.Execute(); return penCommand; }
                }
                else if (command.Equals("circle"))
                {
                    DrawCircle c = (DrawCircle)CommFactory.MakeCommand("circle");

                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();
                    if (varNameList.Contains(coords) == true)
                    {
                        int position;
                        string result;

                        position = varNameList.IndexOf(coords); // Finds the position of the inputted variable
                        result = varValueList[position].ToString();

                        c.Set(canvas, name, result);
                    }
                    else { c.Set(canvas, name, coords); }
                    // try { coords = splitter[1].ToString(); } catch (IndexOutOfRangeException ex) { canvas.DrawString(ex.Message); }

                    //if (coords == null) { throw new GPLException("No coordinates for Circle."); }
                    //if (coords.Contains(",")||coords.Contains(" ")) { throw new GPLException("Invalid number of parameters for Circle."); }
                    if (execute == true) { c.Execute(); return c; }
                }
                else if (command.Equals("rectangle"))
                {
                    DrawRectangle r = (DrawRectangle)CommFactory.MakeCommand("rectangle");
                    name = splitter[0].ToString();
                   // if (coords == null) { throw new GPLException("No coordinates for Rectangle."); }
                    coords = splitter[1].ToString();
                    r.Set(canvas, name, coords);
                    if (execute == true) { r.Execute(); return r; }
                }
                else if (command.Equals("square")) //add some exception handling to make sure inputted coords are same value
                { //could also just reference draw rectangle then to reduce the need for extra class 
                    DrawSquare s = (DrawSquare)CommFactory.MakeCommand("square");
                    name = splitter[0].ToString();
                    if (coords == null) { throw new GPLException("No coordinates for Square."); }

                    coords = splitter[1].ToString();
                    s.Set(canvas, name, coords);
                    if (execute == true) { s.Execute(); return s; }
                }
                else if (command.Equals("triangle"))
                {
                    DrawTriangle t = (DrawTriangle)CommFactory.MakeCommand("triangle");
                    name = splitter[0].ToString();
                    if (coords == null) { throw new GPLException("No coordinates for Triangle."); }
                    coords = splitter[1].ToString();
                    t.Set(canvas, name, coords);
                    if (execute == true) { t.Execute(); return t; }
                }
                else if (command.Equals("fill"))
                {
                    Fill f = (Fill)CommFactory.MakeCommand("fill");
                    name = splitter[0].ToString();

                   // if (coords == null) { throw new GPLException("No status for Fill."); }
                     coords = splitter[1].ToString(); f.Set(canvas, name, coords);
                    if (execute == true) { f.Execute(); return f; }
                }
                else if (command.Equals(""))
                {
                    throw new GPLException("No code to run.");
                }
               /* else if()
                {
                    throw new GPLException("Invalid command: " + command);
                }*/
            }
            catch (GPLException ex)
            {
                canvas.DrawString(ex.Message);
            }

                return null;
            }

        /// <summary>
        /// Method to calculate sum of passed in variable calculation.
        /// Data table is created to calclate the passed in expression for variable.
        /// In loop the calculation is formed and data table returns calculation.
        /// </summary>
        /// <param name="varname">Name of variable</param>
        /// <param name="arrSeg">Array segment</param>
        /// <param name="result">Returns calculation result</param>
        public static void CalculateArrayVal(String varname, ArraySegment<string> arrCalc, out int result)
        {
            DataTable dt = new DataTable(); // Creating data table object
            String calc = "";

            for (int i = arrCalc.Offset; i < (arrCalc.Offset + arrCalc.Count); i++)
            {
                calc = string.Join(" ", arrCalc); // Forms calculation by joining together values of arrCalc
            }
            
            result = Int32.Parse(dt.Compute(calc, "").ToString()); // Calculates expression and returns result
        }


    }
}
