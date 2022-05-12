using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;

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
        String name, coords, colour, command, expression, exists;
        String varChk = "";
        int value, varCounter = 0, varChkCounter = 0, methodCounter = 0;
        ArrayList varNameList = new ArrayList();
        ArrayList varValueList = new ArrayList();
        ArrayList varChkNameList = new ArrayList();
        ArrayList varChkValueList = new ArrayList();

        Regex regexInt = new Regex(@"[\d]"); // Creating regular expression for numbers only
        Regex regexLetters = new Regex(@"^[a-zA-Z]+$"); // Creating regular expression for letters only

        bool valid;


        CommandFactory CommFactory = new CommandFactory();
        Canvas canvas;
        CodeChecker validator = new CodeChecker();

        /// <summary>
        /// Constructor for parser class requires Canvas to be passed in.
        /// </summary>
        /// <param name="c">Object of canvas class.</param>
        public Parser(Canvas c)
        {
           canvas = c;
        }

        /// <summary>
        /// Command method to ParseCommand from inputted lines of code from the user.
        /// </summary>
        /// <param name="command">Command parameter e.g. MoveTo 100,200</param>
        /// <param name="execute">Execute flag, if true code will be executed, if false code will not be executed </param>
        /// <returns>Returns true if successful and false if failed.</returns>
        public Command ParseCommand(String line, bool execute)
        {
            try
            {
                if (line.Length == 0) // If user enters in blank line, then exception is thrown
                {
                    // throw new GPLException("Please enter a command. \nCommand was blank.");
                    //canvas.DrawString("Please enter a command. \nCommand was blank.");
                    Debug.WriteLine("blank");
                }
            

            splitter = line.Split(" "); // Split up user input by space drawto 100,150 .. drawto||100,150
            command = splitter[0]; // Setting first  position of array to equal command variable, for if statements below
            Var varCommand = (Var)CommFactory.MakeCommand("var"); // Creating object of Var class

                for (int i = 0; i < splitter.Length;) // Loop until index is less than length of splitter array
                {
                    Debug.WriteLine(splitter[i]);
                    i++;
                }
         
                if (command.Equals("mymethod") == true)
                {

                }
                if (command.Equals("reset") == true && execute == false || command.Equals("clear") == true && execute == false) // If command equals reset or clear and execute is false
                {
                    validator.drawShrtCmdRules(command.Trim(), out valid); // Command is passed to codechecker for validation
                    if (valid == true) { throw new GPLException(""); } // If codechecker class method returns true then exception is cleared from screen
                    if (valid == false) { throw new GPLException("Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                }
                // If execute is false and command is passed to drawShrtCmdRules for validation on short commands
                 if (execute == false && command.Equals("drawto") == true || execute == false && command.Equals("moveto") == true || execute == false && command.Equals("square") == true
                        || execute == false && command.Equals("triangle") == true || execute == false && command.Equals("pen") == true || execute == false && command.Equals("fill") == true || execute == false && command.Equals("rectangle") == true)
                 {
                    validator.drawCmdRules(command.Trim(), splitter[1], out valid);  // Command is passed to drawCmdRules for validation on draw commands

                    if (valid == false) // If codechecker class method returns false then exception is thrown
                    { throw new GPLException("Invalid syntax for " + command + "\n Please correct before running code."); }

                    if (valid == true) // If codechecker class method returns true then exception is cleared from screen
                    { throw new GPLException(""); }
                 }
                if (command.Equals("var") == true && splitter.Length.Equals(4) == true) // If user declares full variable e.g. var apple = 40
                {
                    name = splitter[1].ToString();
                    value = Int32.Parse(splitter[3]);

                    if (execute == true)
                    {
                        foreach (string item in varCommand.varNameList) // Loop to search each string in array
                        {
                            if (item.Contains(name)) // If name of variable is found in array during execution then exception is thrown
                            {
                                exists = "Y";
                                throw new GPLException("Error! Variable " + name + " already exists.");
                            }
                        }
                            if (execute == true && exists != "Y") // If execute is true parameters are passed to Var class
                            {
                                varCommand.Set(name, value);             // Passing name and value to var class
                                varCommand.varNameList.Add(name);        // Adding name and value of declared variable to array lists
                                varCommand.varValueList.Add(value);

                                varCommand.varNameList[varCounter] = name; // Storing position of name and value in array lists
                                varCommand.varValueList[varCounter] = value;
                                varCounter++; // Increasing counter for next variable
                                varCommand.Execute();
                            }
                    }
                    if (execute == false) // If execute is false parameters are passed to CodeChecker class to be validated
                    {
                        foreach (string item in varChkNameList) // Loop to search each string in array
                        {
                            if (item.Contains(name)) // If name of variable is found in array outside execution then exception is thrown
                            {
                                validator.exists = "Y"; // Sets exists string to Y if variable exists 
                                //throw new GPLException("Variable " + name + " already exists. \nPlease declare a different named variable");
                            }
                        }

                        if (validator.exists != "Y")
                        {
                            varChkNameList.Add(name);        // Adding name and value of declared variable to array lists
                            varChkValueList.Add(value);

                            varChkNameList[varChkCounter] = name; // Storing position of name and value in array lists
                            varChkValueList[varChkCounter] = value;
                            varChkCounter++; // Increasing counter for next variable
                            varChk = "Y";
                            validator.DeclareVar(command, name, value, out valid);

                            if (valid == false) // If codechecker class method returns false then exception is thrown
                            {
                                throw new GPLException("Invalid syntax for " + command + "\n Please correct before running code.");
                            }
                            /*if (valid == true) // If codechecker class method returns true then exception is cleared from screen
                            {
                                throw new GPLException("");
                            }*/
                        }
                    }
                } // End of if user declares full variable 

                if (splitter[0].Equals("var") == true && splitter.Length.Equals(2) == true) // If user declares variable without setting value e.g. var apple
                {
                    name = splitter[1].ToString();
                    value = 0;

                    foreach (string item in varCommand.varNameList) // Loop to search each string in array
                    {
                        if (item.Contains(name)) // If name of variable is found in array during execution then exception is thrown
                        {
                            if (execute == true)
                            {
                                throw new GPLException("Error! Variable " + name + " already exists.");
                            }
                        }
                    }

                    foreach (string item in varChkNameList) // Loop to search each string in array
                    {
                        if (item.Contains(name)) // If name of variable is found in array outside execution then exception is thrown
                        {
                            if (execute == false)
                            {
                                validator.exists = "Y"; // Sets exists string to Y if variable exists 
                                throw new GPLException("Variable " + name + " already exists. \nPlease declare a different named variable");
                            }
                        }
                    }

                    if (execute == true)
                    {
                        varCommand.Set(name, value);  // Passing name and value to var class
                        varCommand.varNameList.Add(name);        // Adding name and value of declared variable to array lists
                        varCommand.varValueList.Add(value);

                        varCommand.varNameList[varCounter] = name; // Storing position of name and value in array lists
                        varCommand.varValueList[varCounter] = value;
                        varCounter++; // Increasing counter for next variable
                        varCommand.Execute();
                    }

                    if (execute == false)
                    {
                        varChkNameList.Add(name);        // Adding name and value of declared variable to array lists
                        varChkValueList.Add(value);

                        varChkNameList[varChkCounter] = name; // Storing position of name and value in array lists
                        varChkValueList[varChkCounter] = value;
                        varChkCounter++; // Increasing counter for next variable
                        varChk = "Y";
                        validator.DeclareVar(command, name, value, out valid);

                        if (valid == false) // If codechecker class method returns false then exception is thrown
                        {
                            throw new GPLException("Invalid syntax for " + command + "\n Please correct before running code.");
                        }
                        if (valid == true) // If codechecker class method returns true then exception is cleared from screen
                        {
                            throw new GPLException("");
                        }
                    }
                } // End of if user declares variable without setting value

                if (splitter[1].Equals("=") == true && splitter.Length.Equals(3))  // If user sets the value of variable to a different number e.g. apple = 50 (after declaration)
                {
                    int position;
                    Debug.WriteLine("var " + splitter[0].ToString() + " length of var = 3");

                    name = splitter[0].ToString();
                    value = Int32.Parse(splitter[2]);

                    foreach (string item in varCommand.varNameList) // Loop to search each string in array
                    {
                        if (execute == true) // If name of variable is found in array during execution then exception is thrown
                        {
                            if (item.Contains(name))
                            {
                                position = varCommand.varNameList.IndexOf(splitter[0]); // Finds the position of the inputted variable
                                varCommand.varValueList[position] = splitter[2];  // Sets the value of the variable in array list

                                varCommand.SetName(name);
                                varCommand.SetValue(value);
                                varCommand.Execute();
                            }
                            else if (!item.Contains(name))
                            {
                                throw new GPLException("Cannot find variable " + name + "\n Please declare variable before it is set.");
                            }
                        }
                    }

                    foreach (string item in varChkNameList) // Loop to search each string in array
                    {
                        if (execute == false) // If name of variable is found in array outside execution then exception is thrown
                        {
                            if (item.Contains(name))
                            {
                                varChk = "Y";
                                position = varChkNameList.IndexOf(splitter[0]); // Finds the position of the inputted variable
                                varChkValueList[position] = splitter[2];  // Sets the value of the variable in array list
                                validator.SetDeclaredVar(name, value, out valid);

                                if (valid == false) // If codechecker class method returns false then exception is thrown
                                {
                                    throw new GPLException("Invalid syntax for " + command + "\n Please correct before running code.");
                                }
                                if (valid == true) // If codechecker class method returns true then exception is cleared from screen
                                {
                                    throw new GPLException("");
                                }
                            }
                            else if (!item.Contains(name))
                            {
                                throw new GPLException("Cannot find variable " + name + "\n Please declare variable before it is set.");
                            }
                        }
                    }
                } // End of if user sets the value of variable to a different number

                if (splitter[1].Equals("=") == true && splitter.Length > 3) // If user sets value of a variable by calculation e.g. apple = 50 * 5
                {
                    Debug.WriteLine("var " + splitter[0].ToString() + " length of var more than 3");
                    name = splitter[0];
                    int position, length = splitter.Length;
                    length = length - 2; // Minusing 2 from length to start from equals sign of splitter array
                    ArraySegment<string> arrGetCalc = new ArraySegment<string>(splitter, 2, length); // Gets the contents of array after = sign to work out calculation

                    foreach (string item in varCommand.varNameList) // Loop to search each string in array
                    {
                        if (execute == true) // If name of variable is found in array during execution then exception is thrown
                        {
                            if (item.Contains(name))
                            {
                                CalculateArrayVal(name, arrGetCalc, out int result); // Passes calculation to CalculateArrayVal method and returns sum of calculation
                                position = varCommand.varNameList.IndexOf(name); // Finds the position of the inputted variable
                                varCommand.varValueList[position] = result;  // Sets the value of the variable in array list
                                value = result;
                                varCommand.SetName(name);
                                varCommand.SetValue(value);
                                varCommand.Execute();
                                Debug.WriteLine("var " + splitter[0].ToString() + " new calculation = " + value);
                            }
                            else if (!item.Contains(name))
                            {
                                throw new GPLException("Cannot find variable " + name + "\n Please declare variable before it is set.");
                            }
                        }
                    }

                    foreach (string item in varChkNameList) // Loop to search each string in array
                    {
                        if (execute == false)  // If name of variable is found in array outside execution then exception is thrown
                        {
                            if (item.Contains(name))
                            {
                                varChk = "Y";
                                CalculateArrayVal(name, arrGetCalc, out int result); // Passes calculation to CalculateArrayVal method and returns sum of calculation
                                position = varChkNameList.IndexOf(name); // Finds the position of the inputted variable
                                varChkValueList[position] = result;  // Sets the value of the variable in array list
                                value = result;
                                validator.SetDeclaredVar(name, value, out valid);
                                Debug.WriteLine("var " + splitter[0].ToString() + " new calculation = " + value);

                                if (valid == false) // If codechecker class method returns false then exception is thrown
                                {
                                    throw new GPLException("Invalid syntax for " + command + "\n Please correct before running code.");
                                }
                                if (valid == true) // If codechecker class method returns true then exception is cleared from screen
                                {
                                    throw new GPLException("");
                                }
                            }
                            else if (!item.Contains(name))
                            {
                                throw new GPLException("Cannot find variable " + name + "\n Please declare variable before it is set.");
                            }
                        }
                    }
                }

                if (command.Equals("drawto") == true) //if command equals drawto then command is split into arrays and passed to drawto class
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
                else if (command.Equals("circle") == true)// && execute == true) //REMOVE THIS MORNING
                {
                    DrawCircle c = (DrawCircle)CommFactory.MakeCommand("circle");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();
                    int position;
                    string result;

                    if (execute == true && regexLetters.IsMatch(coords) == true) // If execute is true and coords contain letters
                    {
                      //  foreach (string item in varCommand.varNameList) // Checks if coords exists in array
                        //{
                            //if (item.Contains(coords)) // If variable is found in array then circle parameters are set
                            //if (varCommand.varNameList.Contains(coords) == true)
                           // {
                                position = varCommand.varNameList.IndexOf(coords); // Finds the position of the inputted variable
                                result = varCommand.varValueList[position].ToString();
                                c.Set(canvas, name, result);
                                c.Execute(); return c;
                            }
                           // else
                           // {
                             //   throw new GPLException("Invalid variable."); // Else exception is thrown
                            //}
                     //   }
                 //   }
                     if (execute == true && regexInt.IsMatch(coords)) // If execute is true and coords contain numbers
                    {
                        c.Set(canvas, name, coords);
                        c.Execute(); return c;
                    }
                    if (execute == false && regexLetters.IsMatch(coords))
                    {
                        foreach (string item in varChkNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(coords)) // If variable is found in array then circle parameters are set
                            {
                                position = varChkNameList.IndexOf(coords); // Finds the position of the inputted variable
                                result = varChkValueList[position].ToString();
                                validator.drawCmdRules(command, result, out bool valid);

                                if (valid == false) // If codechecker class method returns false then exception is thrown
                                {
                                    throw new GPLException("Invalid syntax for " + command + "\n Please correct before running code.");
                                }
                                if (valid == true) // If codechecker class method returns true then exception is cleared from screen
                                {
                                    throw new GPLException("");
                                }
                            }
                            else
                            {
                                throw new GPLException("Invalid variable."); // Else exception is thrown
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(coords))
                    {
                        foreach (string item in varChkNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(coords)) // If variable is found in array then circle parameters are set
                            {
                                position = varChkNameList.IndexOf(coords); // Finds the position of the inputted variable
                                result = varChkValueList[position].ToString();
                                validator.drawCmdRules(command, result, out bool valid);
                                if (valid == false) // If codechecker class method returns false then exception is thrown
                                {
                                    throw new GPLException("Invalid syntax for " + command + "\n Please correct before running code.");
                                }
                                if (valid == true) // If codechecker class method returns true then exception is cleared from screen
                                {
                                    throw new GPLException("");
                                }
                            }
                           /* else
                            {
                                throw new GPLException("Invalid variable."); // Else exception is thrown
                            }*/
                        }
                    }
                 //   else { c.Set(canvas, name, coords); }
                    // try { coords = splitter[1].ToString(); } catch (IndexOutOfRangeException ex) { canvas.DrawString(ex.Message); }

                    //if (coords == null) { throw new GPLException("No coordinates for Circle."); }
                    //if (coords.Contains(",")||coords.Contains(" ")) { throw new GPLException("Invalid number of parameters for Circle."); }
                //    if (execute == true) { }
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
                  //  if (coords == null) { throw new GPLException("No coordinates for Square."); }

                    coords = splitter[1].ToString();
                    s.Set(canvas, name, coords);
                    if (execute == true) { s.Execute(); return s; }
                }
                else if (command.Equals("triangle"))
                {
                    DrawTriangle t = (DrawTriangle)CommFactory.MakeCommand("triangle");
                    name = splitter[0].ToString();
                 //   if (coords == null) { throw new GPLException("No coordinates for Triangle."); }
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
              /*  else if (command.Equals(""))
                {
                    throw new GPLException("No code to run.");
                }*/
            }
           /* catch (GPLException ex)
            {
                canvas.DrawString(ex.Message);
            }*/
            catch (IndexOutOfRangeException ex)
            {
                canvas.DrawString(ex.Message);
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
