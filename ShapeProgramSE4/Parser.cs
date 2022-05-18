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
    public class Parser
    {
        String[] splitter; //array string to split up command 
        String name, coords, colour, command, exists;
        String varChk = "";
        int value, result, methodCounter = 0, varChecker = 0, lineMethodCounter = 0;

        ArrayList varChkNameList = new ArrayList();
        ArrayList varChkValueList = new ArrayList();
        ArrayList varNameList = new ArrayList();
        ArrayList varValueList = new ArrayList();

        Regex regexInt = new Regex(@"[\d]"); // Creating regular expression for numbers only
        Regex regexLetters = new Regex(@"^[a-zA-Z]+$"); // Creating regular expression for letters only

        bool valid, methodFlag = false, methodExecute = false;

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
                Var varCommand = (Var)CommFactory.MakeCommand("var"); // Creating object of Var class

                if (line.Equals("") == true) { throw new GPLException("\n No code to run."); } // If user enters in blank line, then exception is thrown

                splitter = line.Split(" "); // Split up line by space
                command = splitter[0]; 

                if (command.Equals("method") == true) // If command equals method
                {
                }
               
                if (command.Equals("reset") == true && execute == false || command.Equals("clear") == true && execute == false) // If command equals reset or clear and execute is false
                {
                    validator.DrawShrtCmdRules(command.Trim(), out valid); // Command is passed to codechecker for validation
                    if (valid == true) { canvas.DrawString(""); } // If codechecker class method returns true then no error is reported
                    if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                }

                // If execute is false and command equals pen or fill then line is passed to code checker for validation
                if(execute == false && command.Equals("pen") == true || execute == false && command.Equals("fill") == true)
                {
                    if (splitter.Length.Equals(1)) { throw new GPLException("Command " + command + " must require value."); } // If length of splitter equals 1 then exception is thrown
                    validator.DrawCmdRules(command.Trim(), splitter[1], out valid); // Command is passed to drawCmdRules for validation on draw commands
                    if (valid == true) { canvas.DrawString(""); } // If codechecker class method returns true then no error is reported
                    if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                }

                // If user declares full variable e.g. var apple = 40
                if (command.Equals("var") == true && splitter.Length.Equals(4) == true) 
                {
                    name = splitter[1].ToString();
                    value = Int32.Parse(splitter[3]); // Converting 3rd element of splitter array to int

                    if (execute == true)
                    {
                        foreach (string item in varNameList) // Loop to search each string in array
                        {
                            if (item.Contains(name)) { exists = "Y"; throw new GPLException("\n Error! Variable " + name + " already exists."); } // If name of variable is found in array during execution then exception is thrown
                        }
                        if (execute == true && exists != "Y") // If execute is true and name doesnt exist in array then parameters are stored in arrays
                        { 
                            varCommand.Set(name, value); // Passing name and value to var class
                            varNameList.Add(name); // Adding name and value of declared variable to array lists
                            varValueList.Add(value);
                            varCommand.Execute();
                        }
                    } // End of if execute true

                    if (execute == false) // If execute is false parameters are passed to CodeChecker class to be validated
                    {
                        foreach (string item in varChkNameList) // Loop to search each string in array
                        {
                            if (item.Contains(name)) // If name of variable is found in array outside execution then exception is thrown
                            {
                                varChecker++;
                                if (varChecker > 2) // If varChecker is more than 2 (if variable declared more than once) then exception is raised
                                {
                                    validator.exists = "Y"; // Sets exists string to Y if variable exists 
                                    throw new GPLException("\n Variable " + name + " already exists. \nPlease declare a different named variable before running code.");
                                }
                            }
                        }

                        if (validator.exists != "Y") // If variable doesnt exist in codechecker then it is added to the array
                        {
                            varChkNameList.Add(name); // Adding name and value of declared variable to array lists
                            varChkValueList.Add(value);
                            varChk = "Y";
                            validator.DeclareVar(command, name, value, out valid);
                            if (valid == true) { canvas.DrawString(""); } // If codechecker class method returns true then no exception is reported
                            if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                        }
                    } 
                } // End of if user declares full variable 

                // If user declares variable without setting value e.g. var apple
                if (splitter[0].Equals("var") == true && splitter.Length.Equals(2) == true)
                {
                    name = splitter[1].ToString();
                    value = 0;

                    foreach (string item in varNameList) // Loop to search each string in array
                    {
                        if (item.Contains(name)) // If name of variable is found in array during execution then exception is thrown
                        {
                            if (execute == true) { throw new GPLException("\n Error! Variable " + name + " already exists."); }
                        }
                    }

                    foreach (string item in varChkNameList) // Loop to search each string in array
                    {
                        if (item.Contains(name)) // If name of variable is found in array outside execution then exception is thrown
                        {
                            if (execute == false) { validator.exists = "Y"; throw new GPLException("\n Variable " + name + " already exists. \n Please declare a different named variable"); }
                        }
                    }

                    if (execute == true) // If excute is true then name and value are stored in arrays and passed to var class
                    { 
                        varCommand.Set(name, value);
                        varNameList.Add(name);  
                        varValueList.Add(value);
                        varCommand.Execute();
                    }

                    if (execute == false) // If execute is false then line is passed to codechecker for validation
                    {
                        varChkNameList.Add(name); 
                        varChkValueList.Add(value);
                        validator.DeclareVar(command, name, value, out valid);

                        if (valid == true) { canvas.DrawString(""); } // If codechecker class method returns true then no exception is reported
                        if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                    }
                } // End of if user declares variable without setting value

                // If user sets the value of variable to a different number e.g. apple = 50 (after declaration)
                if (varNameList.Contains(splitter[0]) == true && splitter[1].Equals("=") == true && splitter.Length.Equals(3) == true)  
                {
                    int position;
                    name = splitter[0].ToString();
                    value = Int32.Parse(splitter[2]); // Converting third element of splitter array to int

                    foreach (string item in varNameList) // Loop to search each string in array
                    {
                        if (execute == true) // If name of variable is found in array during execution then exception is thrown
                        {
                            if (item.Contains(name))
                            {
                                position = varNameList.IndexOf(splitter[0]); // Finds the position of the inputted variable
                                varValueList[position] = splitter[2];  // Sets the value of the variable in array list
                                varCommand.SetName(name);
                                varCommand.SetValue(value);
                                varCommand.Execute();
                            }
                            else if (!item.Contains(name)) { throw new GPLException("\n Cannot find variable " + name + "\n Please declare variable before it is set."); }
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

                                if (valid == true) { canvas.DrawString(""); } // If codechecker class method returns true then exception is cleared from screen
                                if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                            }
                            else if (!item.Contains(name)) { throw new GPLException("\n Cannot find variable " + name + "\n Please declare variable before it is set."); }
                        }
                    }
                } // End of if user sets the value of variable to a different number

                // If user sets value of a variable by calculation e.g. apple = 50 * 5
                if (varNameList.Contains(command) == true && splitter[1].Equals("=") == true && splitter.Length > 3 == true) 
                {
                    name = command;
                    int position, length = splitter.Length;
                    length = length - 2; // Minusing 2 from length to start from equals sign of splitter array

                    ArraySegment<string> arrGetCalc = new ArraySegment<string>(splitter, 2, length); // Gets the contents of array after = sign to work out calculation

                    foreach (string item in varNameList) // Loop to search each string in array
                    {
                        if (execute == true) // If name of variable is found in array during execution then exception is thrown
                        {
                            if (item.Contains(name))
                            {
                                CalculateArrayVal(name, arrGetCalc, out result); // Passes calculation to CalculateArrayVal method and returns sum of calculation
                                position = varNameList.IndexOf(name); // Finds the position of the inputted variable
                                varValueList[position] = result;  // Sets the value of the variable in array list
                                value = result;
                                varCommand.SetName(name);
                                varCommand.SetValue(value);
                                varCommand.Execute();
                                Debug.WriteLine("var " + splitter[0].ToString() + " new calculation = " + value);
                            }
                            else if (!item.Contains(name)) { throw new GPLException("\n Cannot find variable " + name + "\n Please declare variable before it is set."); }
                        }
                    }

                    foreach (string item in varChkNameList) // Loop to search each string in array
                    {
                        if (execute == false)  // If name of variable is found in array outside execution then exception is thrown
                        {
                            if (item.Contains(name))
                            {
                                varChk = "Y";
                                CalculateArrayVal(name, arrGetCalc, out result); // Passes calculation to CalculateArrayVal method and returns sum of calculation
                                position = varChkNameList.IndexOf(name); // Finds the position of the inputted variable
                                varChkValueList[position] = result;  // Sets the value of the variable in array list
                                value = result;
                                validator.SetDeclaredVar(name, value, out valid);
                                Debug.WriteLine("var " + splitter[0].ToString() + " new calculation = " + value);

                                if (valid == true) { canvas.DrawString(""); } // If codechecker class method returns true then no exception is reported
                                if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown

                            }
                            else if (!item.Contains(name)) { throw new GPLException("\n Cannot find variable " + name + "\n Please declare variable before it is set."); }
                        }
                    }
                }
                if (command.Equals("clear") == true && execute == true) // If command is equal to clear
                {
                    Clear c = (Clear)CommFactory.MakeCommand("clear");
                    name = splitter[0].ToString();
                    c.Set(canvas, name);
                    if (execute == true) { c.Execute(); return c; }
                }
                if (command.Equals("reset") == true && execute == true) // If command is equal to reset
                {
                    Reset r = (Reset)CommFactory.MakeCommand("reset");
                    name = splitter[0].ToString();
                    r.Set(canvas, name);
                    if (execute == true) { r.Execute(); return r; }
                }
                if (command.Equals("fill") && execute == true) // If command is equal to fill
                {
                    if (coords == null) { throw new GPLException("\n No status for Fill."); }
                    Fill f = (Fill)CommFactory.MakeCommand("fill");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString(); f.Set(canvas, name, coords);
                    if (execute == true) { f.Execute(); return f; }
                }
                if (command.Equals("pen") == true && execute == true) // If command is equal to Pen and execute true
                {
                    PenColor penCommand = (PenColor)CommFactory.MakeCommand("pen");
                    try 
                    { 
                        name = splitter[0].ToString();
                        colour = splitter[1].ToString();
                        Color mycol = ColorTranslator.FromHtml(colour); // Translating string to color 
                        penCommand.Set(canvas, name, mycol);
                        if (execute == true) { penCommand.Execute(); return penCommand; }
                    }
                    catch (ArgumentException ex)
                    {
                        throw new GPLException("\n Invalid colour " + colour + " Please enter a valid colour.");
                    }
                }

                if (command.Equals("circle")) // If command is equal to Circle
                {
                    int position = 0;
                    string result;

                    DrawCircle c = (DrawCircle)CommFactory.MakeCommand("circle");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    if (execute == true && regexInt.IsMatch(coords))  { c.Set(canvas, name, coords);  c.Execute(); return c;  } // If execute is true and coords contain numbers parameters are passed to drawcircle class
                    if (execute == true && regexLetters.IsMatch(coords)) // If execute is true and coords contain letters
                    {
                        foreach (string item in varNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(coords)) // If variable is found in array then circle parameters are set
                            {
                                position = varNameList.IndexOf(coords); 
                                result = varValueList[position].ToString();
                                c.Set(canvas, name, result);
                                c.Execute(); 
                                return c;
                            }
                            else { throw new GPLException("\n Cannot find declared variable to draw circle."); }
                        }
                    }
                    if (execute == false && regexLetters.IsMatch(coords)) // If execute is false and coords contain letters
                    {
                        foreach (string item in varChkNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(coords)) // If variable is found in array line is sent to code checker for validation
                            {
                                position = varChkNameList.IndexOf(coords); // Finds the position of the inputted variable
                                result = varChkValueList[position].ToString();
                                validator.DrawCmdRules(command, result, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }// If codechecker class method returns true then no exception is reported
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw circle.\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                            }
                            else { throw new GPLException("\n Invalid variable used to draw circle."); }// Else exception is thrown 
                        }
                    }
                    if (execute == false && regexInt.IsMatch(coords)) // If execute is false and coords contain numbers
                    {
                        validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { canvas.DrawString(""); } 
                        if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw circle.\n Please correct before running code."); } 
                    }
                } // End of if command equals Circle

                if (command.Equals("rectangle")) // If command equals rectangle 
                {
                    int position = 0;
                    string result;

                    DrawRectangle r = (DrawRectangle)CommFactory.MakeCommand("rectangle");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();
                    String[] check = coords.Split(",");

                    if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) { r.Set(canvas, name, coords); r.Execute(); return r; } // If execute is true and coords contain numbers parameters are passed to drawrectangle class
                    if(execute == true && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1])) // If execute is true and first parameter or second parameter equals letter
                    {
                        String parsed;
                        foreach (string item in varNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. rectangle varname,varname - value of variable is found and passed to drawrectangle class
                            {
                                string result2;
                                position = varNameList.IndexOf(check[0]); 
                                result = varValueList[position].ToString();
                                position = varNameList.IndexOf(check[1]); 
                                result2 = varValueList[position].ToString();
                                parsed = result + "," + result2;
                                r.Set(canvas, name, parsed); 
                                r.Execute(); 
                                return r;
                            }
                            if (item.Contains(check[0])) // If e.g. rectangle varname,12 - value of variable is found and passed to drawrectangle class
                            {
                                position = varNameList.IndexOf(check[0]); // Finds the position of the inputted variable
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1]; 
                                r.Set(canvas, name, parsed); 
                                r.Execute(); 
                                return r;
                            }
                            if (item.Contains(check[1])) // If e.g. rectangle 12,varname - value of variable is found and passed to drawrectangle class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result; 
                                r.Set(canvas, name, parsed);
                                r.Execute(); 
                                return r;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { canvas.DrawString(""); }
                        if (valid == false) { throw new GPLException("\n Cannot draw rectangle.\n Please correct before running code."); }
                    }
                    if (execute == false && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1]))
                    {
                        String parsed;
                        foreach (string item in varChkNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. rectangle varname,varname - value of variable is found and passed to codechecker for validation 
                            {
                                string result2;
                                position = varChkNameList.IndexOf(check[0]); 
                                result = varChkValueList[position].ToString();
                                position = varChkNameList.IndexOf(check[1]); 
                                result2 = varChkValueList[position].ToString();
                                parsed = result + "," + result2;
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw rectangle.\n Please correct before running code."); }
                            }
                            if (item.Contains(check[0])) // If e.g. rectangle varname,12 - - value of variable is found and passed to codechecker for validation 
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw rectangle.\n Please correct before running code."); }
                            }
                            if (item.Contains(check[1])) // If e.g. rectangle 12,varname - value of variable is found and passed to codechecker for validation 
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw rectangle.\n Please correct before running code."); }
                            }
                        }
                    }
                } //End of if command equals rectangle

                if (command.Equals("square")) // If command is equal to square
                {
                    int position = 0;
                    string result;
                    DrawSquare s = (DrawSquare)CommFactory.MakeCommand("square");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    String[] check = coords.Split(",");

                    if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) { s.Set(canvas, name, coords); s.Execute(); return s; } // If e.g. square 12,12 and execute is true numbers parameters are passed to drawsquare class
                    if (execute == true && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1])) // If execute is true and first parameter or second parameter equals letter
                    {
                        String parsed;
                        foreach (string item in varNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. square varname,varname - value of variable is found and passed to drawsquare class
                            {
                                string result2;
                                position = varNameList.IndexOf(check[0]);
                                result = varValueList[position].ToString();
                                position = varNameList.IndexOf(check[1]);
                                result2 = varValueList[position].ToString();
                                parsed = result + "," + result2;
                                s.Set(canvas, name, parsed);
                                s.Execute();
                                return s;
                            }
                            if (item.Contains(check[0])) // If e.g. square varname,12 - value of variable is found and passed to drawsquare class
                            {
                                position = varNameList.IndexOf(check[0]); 
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1];
                                s.Set(canvas, name, parsed);
                                s.Execute();
                                return s;
                            }
                            if (item.Contains(check[1])) // If e.g. square 12,varname - value of variable is found and passed to drawsquare class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result;
                                s.Set(canvas, name, parsed);
                                s.Execute();
                                return s;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { canvas.DrawString(""); }
                        if (valid == false) { throw new GPLException("\n Cannot draw square.\n Please correct before running code."); }
                    }
                    if (execute == false && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1])) // If execute is false and parameters either parameters are set to variables
                    {
                        String parsed;
                        foreach (string item in varChkNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. square varname,varname - value of variable is found and passed to codechecker for validation
                            {
                                string result2;
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                position = varChkNameList.IndexOf(check[1]);
                                result2 = varChkValueList[position].ToString();
                                parsed = result + "," + result2;
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw square.\n Please correct before running code."); }
                            }
                            if (item.Contains(check[0])) // If e.g. square varname,12 - value of variable is found and passed to codechecker for validation
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw square.\n Please correct before running code."); }
                            }
                            if (item.Contains(check[1])) // If e.g. square 12,varname - value of variable is found and passed to codechecker for validation
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw square.\n Please correct before running code."); }
                            }
                        }
                    }
                } // End of if command equals square

                if (command.Equals("triangle")) // If command is equal to triangle
                {
                    int position = 0;
                    string result;

                    DrawTriangle t = (DrawTriangle)CommFactory.MakeCommand("triangle");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    String[] check = coords.Split(",");

                    if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) { t.Set(canvas, name, coords); t.Execute(); return t; } // If execute is true and coords contain numbers parameters are passed to drawtriangle class
                    if (execute == true && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1])) // If execute is true and first parameter or second parameter equals letter
                    {
                        String parsed;
                        foreach (string item in varNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. triangle varname,varname - position of inputted variable is found and passed to drawrectangle class
                            {
                                string result2;
                                position = varNameList.IndexOf(check[0]);
                                result = varValueList[position].ToString();
                                position = varNameList.IndexOf(check[1]);
                                result2 = varValueList[position].ToString();
                                parsed = result + "," + result2;
                                t.Set(canvas, name, parsed);
                                t.Execute();
                                return t;
                            }
                            if (item.Contains(check[0])) // If e.g. triangle varname,12 - position of inputted variable is found and passed to drawtriangle class
                            {
                                position = varNameList.IndexOf(check[0]); 
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1];
                                t.Set(canvas, name, parsed);
                                t.Execute();
                                return t;
                            }
                            if (item.Contains(check[1])) // If e.g. rectangle 12,varname - position of inputted variable is found and passed to drawtriangle class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result;
                                t.Set(canvas, name, parsed);
                                t.Execute();
                                return t;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { canvas.DrawString(""); }
                        if (valid == false) { throw new GPLException("\n Cannot draw triangle.\n Please correct before running code."); }
                    }
                    if (execute == false && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1]))
                    {
                        String parsed;
                        foreach (string item in varChkNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. triangle varname,varname
                            {
                                string result2;
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                position = varChkNameList.IndexOf(check[1]);
                                result2 = varChkValueList[position].ToString();
                                parsed = result + "," + result2;
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw triangle.\n Please correct before running code."); }
                            }
                            if (item.Contains(check[0])) // If e.g. triangle varname,12
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw triangle.\n Please correct before running code."); }
                            }
                            if (item.Contains(check[1])) // If e.g. triangle 12,varname
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw triangle.\n Please correct before running code."); }
                            }
                        }
                    }
                } // End of is command equals triangle

                if (command.Equals("drawto") == true) //if command equals drawto then command is split into arrays and passed to drawto class
                {
                    int position = 0;
                    string result;

                    DrawTo d = (DrawTo)CommFactory.MakeCommand("drawto"); //creating drawto command from factory class
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    String[] check = coords.Split(",");

                    if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) { d.Set(canvas, name, coords); d.Execute(); return d; } // If execute is true and coords contain numbers parameters are passed to drawto class
                    if (execute == true && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1])) // If execute is true and first parameter or second parameter equals letter
                    {
                        String parsed;
                        foreach (string item in varNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. drawto varname,varname - position of inputted variable is found and passed to drawrectangle class
                            {
                                string result2;
                                position = varNameList.IndexOf(check[0]);
                                result = varValueList[position].ToString();
                                position = varNameList.IndexOf(check[1]);
                                result2 = varValueList[position].ToString();
                                parsed = result + "," + result2;
                                d.Set(canvas, name, parsed);
                                d.Execute();
                                return d;
                            }
                            if (item.Contains(check[0])) // If e.g. drawto varname,12 - position of inputted variable is found and passed to drawto class
                            {
                                position = varNameList.IndexOf(check[0]);
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1];
                                d.Set(canvas, name, parsed);
                                d.Execute();
                                return d;
                            }
                            if (item.Contains(check[1])) // If e.g. drawto 12,varname - position of inputted variable is found and passed to drawto class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result;
                                d.Set(canvas, name, parsed);
                                d.Execute();
                                return d;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { canvas.DrawString(""); }
                        if (valid == false) { throw new GPLException("\n Cannot execute drawto.\n Please correct before running code."); }
                    }
                    if (execute == false && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1]))
                    {
                        String parsed;
                        foreach (string item in varChkNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. drawto varname,varname
                            {
                                string result2;
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                position = varChkNameList.IndexOf(check[1]);
                                result2 = varChkValueList[position].ToString();
                                parsed = result + "," + result2;
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for drawto.\n Please correct before running code."); }
                            }
                            if (item.Contains(check[0])) // If e.g. drawto varname,12
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for drawto.\n Please correct before running code."); }
                            }
                            if (item.Contains(check[1])) // If e.g. drawto 12,varname
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for drawto.\n Please correct before running code."); }
                            }
                        }
                    }
                } // End of if command equals drawto

                if (command.Equals("moveto")) // If command equals moveto
                {
                    int position = 0;
                    string result;

                    MoveTo m = (MoveTo)CommFactory.MakeCommand("moveto");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    String[] check = coords.Split(",");

                    if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) { m.Set(canvas, name, coords); m.Execute(); return m; } // If execute is true and coords contain numbers parameters are passed to drawto class
                    if (execute == true && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1])) // If execute is true and first parameter or second parameter equals letter
                    {
                        String parsed;
                        foreach (string item in varNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. drawto varname,varname - position of inputted variable is found and passed to drawrectangle class
                            {
                                string result2;
                                position = varNameList.IndexOf(check[0]);
                                result = varValueList[position].ToString();
                                position = varNameList.IndexOf(check[1]);
                                result2 = varValueList[position].ToString();
                                parsed = result + "," + result2;
                                m.Set(canvas, name, parsed);
                                m.Execute();
                                return m;
                            }
                            if (item.Contains(check[0])) // If e.g. moveto varname,12 - position of inputted variable is found and passed to drawto class
                            {
                                position = varNameList.IndexOf(check[0]);
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1];
                                m.Set(canvas, name, parsed);
                                m.Execute();
                                return m;
                            }
                            if (item.Contains(check[1])) // If e.g. moveto 12,varname - position of inputted variable is found and passed to moveto class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result;
                                m.Set(canvas, name, parsed);
                                m.Execute();
                                return m;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { canvas.DrawString(""); }
                        if (valid == false) { throw new GPLException("\n Cannot execute moveto.\n Please correct before running code."); }
                    }
                    if (execute == false && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1]))
                    {
                        String parsed;
                        foreach (string item in varChkNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. moveto varname,varname
                            {
                                string result2;
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                position = varChkNameList.IndexOf(check[1]);
                                result2 = varChkValueList[position].ToString();
                                parsed = result + "," + result2;
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for moveto.\n Please correct before running code."); }
                            }
                            if (item.Contains(check[0])) // If e.g. moveto varname,12
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for moveto.\n Please correct before running code."); }
                            }
                            if (item.Contains(check[1])) // If e.g. moveto 12,varname
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;
                                validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for moveto.\n Please correct before running code."); }
                            }
                        }
                    }
                } // End of if command equals moveto

            } // End bracket for try
              catch (GPLException ex)
              {
                canvas.DrawString(ex.Message);
              }
              catch (IndexOutOfRangeException ex)
              {
                 Debug.WriteLine(ex);
              }
              return null;

        } // End of ParseCommand method

        /// <summary>
        /// Method to find declared variable value 
        /// </summary>
        /// <param name="varname"></param>
        /// <param name="results"></param>
        public void VariableValueFinder(String varname, out string results)
        {
            int position;
            position = varNameList.IndexOf(varname); // Finds the position of the inputted variable
            varValueList[position] = result;  // Sets the value of the variable in array list
            results = result.ToString();
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
