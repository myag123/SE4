using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Parser class requires canvas object to be passed into its constructor method in order to draw command on Canvas.
    /// Parser class has a ParseCommand method which requires user input of command and will return a true or false result.
    /// Parser class is overall responsible for creating objects to split up a range of commands into arrays and pass them to a 
    /// command class so the command can be eventually drawn on the canvas.
    /// </summary>
    public class Parser
    {
        String[] splitter; // String array to split up command 
        String name, coords, colour, command, exists, iterateBy, loopVar, loopFlag = "X", methodFlag = "X", methodName, methodRun = "N", commandChk = "N", ifStmntTrue = "X";
        String varChk = "";
        int value, result, methodCounter = 0, varChecker = 0, lineMethodCounter = 0, lineTotal = 0;

        //ArrayList declaration for variables
        ArrayList varChkNameList = new ArrayList();
        ArrayList varChkValueList = new ArrayList();
        ArrayList varNameList = new ArrayList();
        ArrayList varValueList = new ArrayList();

        //ArrayList declaration for method
        ArrayList methodNameList = new ArrayList();

        Regex regexInt = new Regex(@"[\d]"); // Creating regular expression for numbers only
        Regex regexLetters = new Regex(@"^[a-zA-Z]+$"); // Creating regular expression for letters only

        bool valid;

        CommandFactory CommFactory = new CommandFactory();
        Canvas Canvas;
        CodeChecker Validator = new CodeChecker();
        Method mthd;
        Loop loop;
        Var varCommand;

        /// <summary>
        /// Constructor for parser class requires Canvas to be passed in.
        /// </summary>
        /// <param name="c">Object of canvas class.</param>
        public Parser(Canvas c)
        {
           Canvas = c;
        }

        /// <summary>
        /// Command method to ParseCommand from inputted lines of code from the user.
        /// A range of if statements will be hit depending if execute is true or false to validate code and draw commands.
        /// </summary>
        /// <param name="command">Command parameter e.g. MoveTo 100,200</param>
        /// <param name="execute">Execute flag, if true code will be executed, if false code will not be executed </param>
        /// <returns>Returns true if successful and false if failed.</returns>
        public Command ParseCommand(String line, bool execute)
        {
            try
            {
                if (line.Equals("") == true) { throw new GPLException("\n No code to run."); } // If user enters in blank line, then exception is thrown

                loopVar = line; // Setting loopVar to equal line for when variable is required to be passed to Loop class

                splitter = line.Split(" "); // Split up line by space
                command = splitter[0];

                if(command.Equals("loop") == true) // If command equals loop
                {
                    loop = (Loop)CommFactory.MakeCommand("loop");
                    if (execute == true)
                    {
                        Debug.WriteLine("Loop declared");
                        iterateBy = splitter[1];
                        loop.SetIterateVal(Int32.Parse(iterateBy)); // Passing value to iterate by to Loop class
                        loopFlag = "Y"; // Loop flag is set to Y so further commands can be passed to Loop class
                    }
                    if (execute == false)
                    {
                        Validator.DrawCmdRules(command, splitter[1], out valid); // Command is passed to codechecker for validation
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no error is reported
                        if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                    }
                } // End of command equals loop

                if(command.Equals("endloop") == true) // If command equals endloop
                {
                    if (execute == true)
                    {
                        loop.DuplicateLoopLines(); // Calling method to make sure lines set in loop class will be iterated through
                        var iterator = loop.GetLoopLinesIterator(); // Calling iterator from LoopIterator following iterator design pattern
                        int val = loop.GetIterateVal(); // Getting value of iteration for loop

                        while (iterator.MoveNext()) // While loopLines List has elements
                        {
                            loopFlag = "X"; // Setting loopFlag to X to prevent code from being set in Loop class
                            ParseCommand(iterator.Current, true); // Pass current element of loopLines to ParseCommand
                        }
                        loopFlag = "N"; // Closing down loop
                    }
                    if (execute == false)
                    {
                        Validator.DrawShrtCmdRules(command, out valid); // Command is passed to codechecker for validation
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no error is reported
                        if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                    }
                } // End of if command equals endloop

                if (command.Equals("method") == true) // If command equals method
                {
                    mthd = (Method)CommFactory.MakeCommand("method");
                    if (execute == true) // If execute is true then method is stored in methodNameList and methodFlag is set to Y and methodRun is set to N
                    {
                        Debug.WriteLine("Method declared");
                        methodFlag = "Y";
                        methodRun = "N";

                        name = splitter[1].ToString();
                        methodName = splitter[1].ToString(); // Storing current method name
                        methodNameList.Add(name);
                        mthd.SetName(name); // Storing method name in arraylist in method class
                        methodCounter++; // Method counter increased
                    }
                    if (execute == false) // If execute false line is passed to codechecker for validation
                    {
                        Validator.DrawShrtCmdRules(command, out valid); // Command is passed to codechecker for validation
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no error is reported
                        if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                    }
                }
                
                if (command.Equals("endmethod")) // If command equals endmethod 
                {
                    if (execute == true) // If execute is true then method flag is set to N and number of lines in the method is stored in line total
                    {
                        methodFlag = "N";
                        lineTotal = lineMethodCounter;
                    }
                    if (execute == false) // If execute false line is passed to codechecker for validation
                    {
                        Validator.DrawShrtCmdRules(command, out valid); // Command is passed to codechecker for validation
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no error is reported
                        if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                    }
                } // End of if command equals endmethod

                if (command.Equals("reset") == true && execute == false || command.Equals("clear") == true && execute == false) // If command equals reset or clear and execute is false line is passed to codechecker for validation
                {
                    Validator.DrawShrtCmdRules(command.Trim(), out valid); // Command is passed to codechecker for validation
                    if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no error is reported
                    if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                } // End of if command equals reset or clear

                // If a declared method is called - method is retrieved from GetMethodLinesIterator in Method class and code is passed to parser
                if (execute == true && !methodFlag.Equals("Y") && methodRun == "N" && methodNameList.Contains(command)) 
                {
                    name = splitter[0].ToString();
                    methodFlag = "X"; // Resetting flag ready for next method to be called

                    var iterator = mthd.GetMethodLinesIterator(); // Calling iterator from LoopIterator following iterator design pattern

                    while (iterator.MoveNext()) // While methodLines has elements
                    {
                        ParseCommand(iterator.Current, true); // Pass current element of methodLines to ParseCommand
                        methodRun = "Y";
                    }

                } // End of if execute true for method called

                // If execute is false and command equals pen or fill then line is passed to code checker for validation
                if (execute == false && command.Equals("pen") == true || execute == false && command.Equals("fill") == true)
                {
                    if (splitter.Length.Equals(1)) { throw new GPLException("Command " + command + " must require value."); } // If length of splitter equals 1 then exception is thrown
                    Validator.DrawCmdRules(command.Trim(), splitter[1], out valid); // Command is passed to drawCmdRules for validation on draw commands
                    if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no error is reported
                    if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                }

                // If user declares full variable e.g. var apple = 40
                if (command.Equals("var") == true && splitter.Length.Equals(4) == true) 
                {
                    varCommand = (Var)CommFactory.MakeCommand("var");  // Creating object of Var class
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

                            if (methodFlag == "Y") // If methodFlag equals Y (meaning method is during declaration)
                            {
                                string code = command +  " " + name + " = " + value;
                                mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                                lineMethodCounter++; // Increases line
                                Debug.WriteLine(code + " " + methodName + " added to methodLines"); 
                            } 
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
                                    Validator.exists = "Y"; // Sets exists string to Y if variable exists 
                                    throw new GPLException("\n Variable " + name + " already exists. \nPlease declare a different named variable before running code.");
                                }
                            }
                        }

                        if (Validator.exists != "Y") // If variable doesnt exist in codechecker then it is added to the array
                        {
                            varChkNameList.Add(name); // Adding name and value of declared variable to array lists
                            varChkValueList.Add(value);
                            varChk = "Y";
                            Validator.DeclareVar(command, name, value, out valid);
                            if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no exception is reported
                            if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                        }
                    } 
                } // End of if user declares full variable 

                // If user declares variable without setting value e.g. var apple
                if (splitter[0].Equals("var") == true && splitter.Length.Equals(2) == true)
                {
                    varCommand = (Var)CommFactory.MakeCommand("var");  // Creating object of Var class
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
                            if (execute == false) { Validator.exists = "Y"; throw new GPLException("\n Variable " + name + " already exists. \n Please declare a different named variable"); }
                        }
                    }

                    if (execute == true) // If excute is true then name and value are stored in arrays and passed to var class
                    { 
                        varCommand.Set(name, value);
                        varNameList.Add(name);  
                        varValueList.Add(value);
                        varCommand.Execute();

                        if (methodFlag == "Y") // If methodFlag equals Y (meaning method is during declaration)
                        {
                            string code = command + " " + name; 
                            mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                            lineMethodCounter++; // Increases line
                        }
                    }

                    if (execute == false) // If execute is false then line is passed to codechecker for validation
                    {
                        varChkNameList.Add(name); 
                        varChkValueList.Add(value);
                        Validator.DeclareVar(command, name, value, out valid);

                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no exception is reported
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

                                if (methodFlag == "Y") // If methodFlag equals Y (meaning method is during declaration)
                                {
                                    string code =  name + " = " + value;
                                    mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                                    lineMethodCounter++; // Increases line
                                }
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
                                Validator.SetDeclaredVar(name, value, out valid);

                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then exception is cleared from screen
                                if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                            }
                            else if (!item.Contains(name)) { throw new GPLException("\n Cannot find variable " + name + "\n Please declare variable before it is set."); }
                        }
                    }
                } // End of if user sets the value of variable to a different number

                // If user sets value of a variable by calculation e.g. apple = 50 * 5
                if (varNameList.Contains(command) == true && splitter[1].Equals("=") == true && splitter.Length > 3 == true && regexInt.IsMatch(splitter[2])) 
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
                                if (methodFlag == "Y") // If methodFlag equals Y (meaning method is during declaration)
                                {
                                    string code = name + " = " + arrGetCalc.ToString();
                                    mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                                    lineMethodCounter++; // Increases line
                                }

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
                                Validator.SetDeclaredVar(name, value, out valid);
                                Debug.WriteLine("var " + splitter[0].ToString() + " new calculation = " + value);

                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no exception is reported
                                if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown

                            }
                            else if (!item.Contains(name)) { throw new GPLException("\n Cannot find variable " + name + "\n Please declare variable before it is set."); }
                        }
                    }
                } // End of var calculation

                // If user sets value of a variable to equal itself followed by a calculation e.g. apple = apple + 10 
                if (varNameList.Contains(command) == true && splitter[1].Equals("=") == true && varNameList.Contains(splitter[2]) && regexLetters.IsMatch(splitter[2]))
                {
                    name = command;
                    string varVal;
                    int length;
                    length = splitter.Length;
                    length = length - 2;

                    if (execute == true)
                    {
                        if (loopFlag == "Y") // Validating declaration of variable before it is stored in Loop class
                        {
                            Validator.DeclareVarForLoop(loopVar, out valid);
                            if (valid == true) { loop.SetLoopLines(loopVar); }
                            if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); }
                        }

                        int position = varNameList.IndexOf(name); // Finds the position of the inputted variable
                        varVal = varValueList[position].ToString();
                        splitter[2] = varVal; // Setting 3rd element of splitter array to its actual value (using example lets say value of apple was 50... 50 + 10 would be passed to arrGetCalc)
                        
                        ArraySegment<string> arrGetCalc = new ArraySegment<string>(splitter, 2, length); // Gets the contents of array after = sign to work out calculation
                        CalculateArrayVal(name, arrGetCalc, out result); // Passes calculation to CalculateArrayVal method and returns sum of calculation
                        
                        position = varNameList.IndexOf(name); // Finds the position of the inputted variable
                        varValueList[position] = result;  // Sets the value of the variable in array list
                        value = result;
                    }
                    if (execute == false)  // If name of variable is found in array outside execution then exception is thrown
                    {
                        varChk = "Y";
                        int position = varChkNameList.IndexOf(name);
                        varVal = varChkValueList[position].ToString();
                        splitter[2] = varVal;

                        ArraySegment<string> arrGetCalc = new ArraySegment<string>(splitter, 2, length); // Gets the contents of array after = sign to work out calculation
                        CalculateArrayVal(name, arrGetCalc, out result); // Passes calculation to CalculateArrayVal method and returns sum of calculation
                        
                        position = varChkNameList.IndexOf(name); // Finds the position of the inputted variable
                        varChkValueList[position] = result;  // Sets the value of the variable in array list
                        value = result;

                        Validator.SetDeclaredVar(name, value, out valid);
                        Debug.WriteLine("loopVar  " + " new calculation = " + value);

                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no exception is reported
                        if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                    }
                } // End of if variable to equal itself

                if (command.Equals("if") == true && !methodFlag.Equals("N")) // If command equals if
                {
                    if (execute == true) // If execute is true then if statement is check against variable already declared
                    {
                        int position = 0;
                        ifStmntTrue = "X"; // Resetting ifStmntTrue flag

                        String[] splitVar; // Declaring string array to split up part of if statement
                        name = splitter[1].ToString(); 
                        splitVar = name.Split("=="); // Split name up by equals signs
                        Debug.WriteLine(splitVar[0].ToString() + " " + splitVar[1].ToString());

                        if (varNameList.Contains(splitVar[0])) // If name of variable in if statment exists in varNameList
                        {
                            position = varNameList.IndexOf(splitVar[0]); // Finds the position of variable in varNameList
                            if(varValueList[position].Equals(Int32.Parse(splitVar[1])))  // If value stored in varValueList equals value of variable in if statement then ifStmntTrue is set to Y
                            {
                                ifStmntTrue = "Y";
                                Debug.WriteLine("if statment is true: " + ifStmntTrue);
                            }
                        }
                        if (!varNameList.Contains(splitVar[0])||!varValueList[position].Equals(Int32.Parse(splitVar[1]))) // If variable not found in varNameList or if value of varible in if doesnt match value of variable stored in varValueList then ifStmntTrue is N exception is thrown  
                        {
                            ifStmntTrue = "N";
                            throw new GPLException("If statement is false.");
                        }
                    }
                    if (execute == false) // If execute is false then if statement is passed to codechecker for validation
                    {
                        Validator.IfStatement("if "+ splitter[1].ToString(), out valid); // Command is passed to codechecker for validation
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no error is reported
                        if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                    }
                } // End of if command equals if

                if (command.Equals("endif") && !methodFlag.Equals("N")) // If command equals endif then ifStmntTrue is set to X
                {
                    if(execute == true)
                    {
                        ifStmntTrue = "X";
                    }
                    if(execute == false)
                    {
                        Validator.IfStatement(command, out valid); // Command is passed to codechecker for validation
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } // If codechecker class method returns true then no error is reported
                        if (valid == false) { throw new GPLException("\n Invalid syntax for " + command + "\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                    }
                } // End of if command equals 

                if (command.Equals("clear") == true && execute == true && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N")) // If command is equal to clear
                {
                    Clear c = (Clear)CommFactory.MakeCommand("clear");
                    name = splitter[0].ToString();

                    if (methodFlag == "Y") // If methodFlag equals Y (meaning method is during declaration)
                    {
                        string code = name;
                        mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                        lineMethodCounter++; // Increases line
                    }
                    else if (!methodFlag.Equals("Y"))
                    {
                        c.Set(Canvas, name);
                        c.Execute(); 
                        return c; 
                    }
                }
                if (command.Equals("reset") == true && execute == true && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N")) // If command is equal to reset
                {
                    Reset r = (Reset)CommFactory.MakeCommand("reset");
                    name = splitter[0].ToString();

                    if (methodFlag == "Y") // If methodFlag equals Y (meaning method is during declaration)
                    {
                        string code = name;
                        mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                        lineMethodCounter++; // Increases line
                    }
                    else if (!methodFlag.Equals("Y"))
                    { 
                        r.Set(Canvas, name);
                        r.Execute(); 
                        return r; 
                    }
                   
                } // End of if command equals reset

                if (command.Equals("fill") && execute == true && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N")) // If command is equal to fill
                {
                    //if (coords == null) { throw new GPLException("\n No status for Fill."); }
                    Fill f = (Fill)CommFactory.MakeCommand("fill");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    if (methodFlag == "Y") // If methodFlag equals Y (meaning method is during declaration)
                    {
                        string code = name + " " + coords;
                        mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                        lineMethodCounter++; // Increases line
                        Debug.WriteLine(code + " " + methodName + " added to method lines");
                    }
                    else if (!methodFlag.Equals("Y")) // If methodFlag is not equal to Y
                    {
                        f.Set(Canvas, name, coords);
                        f.Execute(); 
                        return f; 
                    }
                }
                if (command.Equals("pen") == true && execute == true && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N")) // If command is equal to Pen and execute true
                {
                    PenColor penCommand = (PenColor)CommFactory.MakeCommand("pen");
                    try 
                    {
                        name = splitter[0].ToString();
                        colour = splitter[1].ToString();
                        
                        if (methodFlag == "Y") // If methodFlag equals Y (meaning method is during declaration)
                        {
                            string code = name + " " + colour;
                            mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                            lineMethodCounter++; // Increases line
                            Debug.WriteLine(code + " " + methodName + " added to method lines");
                        }
                        else if (!methodFlag.Equals("Y")) // If methodFlag is not equal to Y
                        {
                            Color mycol = ColorTranslator.FromHtml(colour); // Translating string to color 
                            penCommand.Set(Canvas, name, mycol);
                            penCommand.Execute();
                            return penCommand;
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        throw new GPLException("\n Invalid colour " + colour + " Please enter a valid colour.");
                    }
                } // End of if command equals pen

                if (command.Equals("circle") && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N") && !loopFlag.Equals("N")) // If command is equal to Circle
                {
                    int position = 0;
                    string result;

                    DrawCircle c = (DrawCircle)CommFactory.MakeCommand("circle");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    if (methodFlag == "Y" && execute == true) // If methodFlag equals Y (meaning method is during declaration)
                    {
                        string code = name + " " + coords;
                        mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                        lineMethodCounter++; // Increases line
                    }
                    if (loopFlag == "Y" && execute == true) // If loopFlag equals Y 
                    {
                        string code = name + " " + coords;
                        loop.SetLoopLines(code); 
                    }
                    else if (execute == true && regexInt.IsMatch(coords) && !methodFlag.Equals("Y") && loopFlag.Equals("X"))  { c.Set(Canvas, name, coords);  c.Execute(); return c;  } // If execute is true and coords contain numbers parameters are passed to drawcircle class
                    else if (execute == true && regexLetters.IsMatch(coords) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) // If execute is true and coords contain letters
                    {
                        foreach (string item in varNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(coords)) // If variable is found in array then circle parameters are set
                            {
                                position = varNameList.IndexOf(coords); 
                                result = varValueList[position].ToString();
                                c.Set(Canvas, name, result);
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

                                Validator.DrawCmdRules(command, result, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }// If codechecker class method returns true then no exception is reported
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw circle.\n Please correct before running code."); } // If codechecker class method returns false then exception is thrown
                            }
                            else { throw new GPLException("\n Invalid variable used to draw circle."); }// Else exception is thrown 
                        }
                    }
                    if (execute == false && regexInt.IsMatch(coords)) // If execute is false and coords contain numbers
                    {
                        Validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; } 
                        if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw circle.\n Please correct before running code."); } 
                    }
                } // End of if command equals Circle

                if (command.Equals("rectangle") && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N") && !loopFlag.Equals("N")) // If command equals rectangle 
                {
                    int position = 0;
                    string result;

                    DrawRectangle r = (DrawRectangle)CommFactory.MakeCommand("rectangle");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();
                    String[] check = coords.Split(",");

                    if (methodFlag == "Y" && execute == true) // If methodFlag equals Y (meaning method is during declaration) code is passed to Method class
                    {
                        string code = name + " " + coords;
                        mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                        lineMethodCounter++; // Increases line
                    }
                    if (loopFlag == "Y" && execute == true) // If loopFlag equals Y 
                    {
                        string code = name + " " + coords;
                        loop.SetLoopLines(code);
                    }
                    else if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) { r.Set(Canvas, name, coords); r.Execute(); return r; } // If execute is true and coords contain numbers parameters are passed to drawrectangle class
                    else if(execute == true && regexLetters.IsMatch(check[0]) && !methodFlag.Equals("Y") && loopFlag.Equals("X") || execute == true && regexLetters.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) // If execute is true and first parameter or second parameter equals letter
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
                                r.Set(Canvas, name, parsed); 
                                r.Execute(); 
                                return r;
                            }
                            else if (item.Contains(check[0])) // If e.g. rectangle varname,12 - value of variable is found and passed to drawrectangle class
                            {
                                position = varNameList.IndexOf(check[0]); // Finds the position of the inputted variable
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1]; 

                                r.Set(Canvas, name, parsed); 
                                r.Execute(); 
                                return r;
                            }
                            else if (item.Contains(check[1])) // If e.g. rectangle 12,varname - value of variable is found and passed to drawrectangle class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result; 

                                r.Set(Canvas, name, parsed);
                                r.Execute(); 
                                return r;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        Validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                        if (valid == false) { throw new GPLException("\n Cannot draw rectangle.\n Please correct before running code."); }
                    }
                    if (execute == false && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1])) // If execute is false and coords contains letters second or first
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

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw rectangle.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[0])) // If e.g. rectangle varname,12 - - value of variable is found and passed to codechecker for validation 
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw rectangle.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[1])) // If e.g. rectangle 12,varname - value of variable is found and passed to codechecker for validation 
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw rectangle.\n Please correct before running code."); }
                            }
                        }
                    }
                } //End of if command equals rectangle

                if (command.Equals("pie") && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N") && !loopFlag.Equals("N")) // If command equals pie 
                {
                    int position = 0;
                    string result;

                    DrawPie p = (DrawPie)CommFactory.MakeCommand("pie");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();
                    String[] check = coords.Split(",");

                    if (methodFlag == "Y" && execute == true) // If methodFlag equals Y (meaning method is during declaration) code is passed to Method class
                    {
                        string code = name + " " + coords;
                        mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                        lineMethodCounter++; // Increases line
                    }
                    if (loopFlag == "Y" && execute == true) // If loopFlag equals Y code is passed to Loop class
                    {
                        string code = name + " " + coords;
                        loop.SetLoopLines(code);
                    }
                    else if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) { p.Set(Canvas, name, coords); p.Execute(); return p; } // If execute is true and coords contain numbers parameters are passed to drawpie class
                    else if (execute == true && regexLetters.IsMatch(check[0]) && !methodFlag.Equals("Y") && loopFlag.Equals("X") || execute == true && regexLetters.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) // If execute is true and first parameter or second parameter equals letter
                    {
                        String parsed;
                        foreach (string item in varNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. pie varname,varname - value of variable is found and passed to drawpie class
                            {
                                string result2;
                                position = varNameList.IndexOf(check[0]);
                                result = varValueList[position].ToString();

                                position = varNameList.IndexOf(check[1]);
                                result2 = varValueList[position].ToString();
                                parsed = result + "," + result2;

                                p.Set(Canvas, name, parsed);
                                p.Execute();
                                return p;
                            }
                            else if (item.Contains(check[0])) // If e.g. pie varname,12 - value of variable is found and passed to drawpie class
                            {
                                position = varNameList.IndexOf(check[0]); // Finds the position of the inputted variable
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1];

                                p.Set(Canvas, name, parsed);
                                p.Execute();
                                return p;
                            }
                            else if (item.Contains(check[1])) // If e.g. pie 12,varname - value of variable is found and passed to drawpie class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result;

                                p.Set(Canvas, name, parsed);
                                p.Execute();
                                return p;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        Validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                        if (valid == false) { throw new GPLException("\n Cannot draw pie.\n Please correct before running code."); }
                    }
                    if (execute == false && regexLetters.IsMatch(check[0]) || regexLetters.IsMatch(check[1]))
                    {
                        String parsed;
                        foreach (string item in varChkNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. pie varname,varname - value of variable is found and passed to codechecker for validation 
                            {
                                string result2;
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();

                                position = varChkNameList.IndexOf(check[1]);
                                result2 = varChkValueList[position].ToString();
                                parsed = result + "," + result2;

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw pie.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[0])) // If e.g. pie varname,12 - - value of variable is found and passed to codechecker for validation 
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw pie.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[1])) // If e.g. pie 12,varname - value of variable is found and passed to codechecker for validation 
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw pie.\n Please correct before running code."); }
                            }
                        }
                    }
                } //End of if command equals pie

                if (command.Equals("square") && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N") && !loopFlag.Equals("N")) // If command is equal to square
                {
                    int position = 0;
                    string result;

                    DrawSquare s = (DrawSquare)CommFactory.MakeCommand("square");

                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();
                    String[] check = coords.Split(",");

                    if (methodFlag == "Y" && execute == true) // If methodFlag equals Y (meaning method is during declaration) code is passed to Method class
                    {
                        string code = name + " " + coords;
                        mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                        lineMethodCounter++; // Increases line
                        Debug.WriteLine(code + " " + methodName + " added to method lines");
                    }
                    if (loopFlag == "Y" && execute == true) // If loopFlag equals Y code is passed to Loop class
                    {
                        string code = name + " " + coords;
                        loop.SetLoopLines(code);
                    }
                    else if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) { s.Set(Canvas, name, coords); s.Execute(); return s; } // If e.g. square 12,12 and execute is true numbers parameters are passed to drawsquare class
                    else if (execute == true && regexLetters.IsMatch(check[0]) && !methodFlag.Equals("Y") && loopFlag.Equals("X") || execute == true && regexLetters.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) // If execute is true and first parameter or second parameter equals letter
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

                                s.Set(Canvas, name, parsed);
                                s.Execute();
                                return s;
                            }
                            else if (item.Contains(check[0])) // If e.g. square varname,12 - value of variable is found and passed to drawsquare class
                            {
                                position = varNameList.IndexOf(check[0]); 
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1];
                                s.Set(Canvas, name, parsed);
                                s.Execute();
                                return s;
                            }
                            else if (item.Contains(check[1])) // If e.g. square 12,varname - value of variable is found and passed to drawsquare class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result;
                                s.Set(Canvas, name, parsed);
                                s.Execute();
                                return s;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        Validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
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

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw square.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[0])) // If e.g. square varname,12 - value of variable is found and passed to codechecker for validation
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw square.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[1])) // If e.g. square 12,varname - value of variable is found and passed to codechecker for validation
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw square.\n Please correct before running code."); }
                            }
                        }
                    }
                } // End of if command equals square

                if (command.Equals("triangle") && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N") && !loopFlag.Equals("N")) // If command is equal to triangle
                {
                    int position = 0;
                    string result;

                    DrawTriangle t = (DrawTriangle)CommFactory.MakeCommand("triangle");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    String[] check = coords.Split(",");

                    if (methodFlag == "Y" && execute == true) // If methodFlag equals Y (meaning method is during declaration) code is passed to Method class
                    {
                        string code = name + " " + coords;
                        mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                        lineMethodCounter++; // Increases line
                    }
                    if (loopFlag == "Y" && execute == true) // If loopFlag equals Y 
                    {
                        string code = name + " " + coords;
                        loop.SetLoopLines(code);
                    }
                    else if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) { t.Set(Canvas, name, coords); t.Execute(); return t; } // If execute is true and coords contain numbers parameters are passed to drawtriangle class
                    else if (execute == true && regexLetters.IsMatch(check[0]) && !methodFlag.Equals("Y") && loopFlag.Equals("X") || execute == true && regexLetters.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) // If execute is true and first parameter or second parameter equals letter
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
                                t.Set(Canvas, name, parsed);
                                t.Execute();
                                return t;
                            }
                            else if (item.Contains(check[0])) // If e.g. triangle varname,12 - position of inputted variable is found and passed to drawtriangle class
                            {
                                position = varNameList.IndexOf(check[0]); 
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1];

                                t.Set(Canvas, name, parsed);
                                t.Execute();
                                return t;
                            }
                            else if (item.Contains(check[1])) // If e.g. rectangle 12,varname - position of inputted variable is found and passed to drawtriangle class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result;

                                t.Set(Canvas, name, parsed);
                                t.Execute();
                                return t;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        Validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { Canvas.DrawString(""); }
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

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw triangle.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[0])) // If e.g. triangle varname,12
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw triangle.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[1])) // If e.g. triangle 12,varname
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable to draw triangle.\n Please correct before running code."); }
                            }
                        }
                    }
                } // End of is command equals triangle

                if (command.Equals("drawto") == true && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N") && !loopFlag.Equals("N")) //if command equals drawto then command is split into arrays and passed to drawto class
                {
                    int position = 0;
                    string result;

                    DrawTo d = (DrawTo)CommFactory.MakeCommand("drawto"); //creating drawto command from factory class
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    String[] check = coords.Split(",");

                    if (methodFlag == "Y" && execute == true) // If methodFlag equals Y (meaning method is during declaration) code is passed to Method class
                    {
                        string code = name + " " + coords;
                        mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                        lineMethodCounter++; // Increases line
                    }
                    if (loopFlag == "Y" && execute == true) // If loopFlag equals Y code is passed to Loop class
                    {
                        string code = name + " " + coords;
                        loop.SetLoopLines(code);
                    }
                    else if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) { d.Set(Canvas, name, coords); d.Execute(); return d; } // If execute is true and coords contain numbers parameters are passed to drawto class
                    else if (execute == true && regexLetters.IsMatch(check[0]) && !methodFlag.Equals("Y") && loopFlag.Equals("X") || execute == true && regexLetters.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) // If execute is true and first parameter or second parameter equals letter
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

                                d.Set(Canvas, name, parsed);
                                d.Execute();
                                return d;
                            }
                            else if (item.Contains(check[0])) // If e.g. drawto varname,12 - position of inputted variable is found and passed to drawto class
                            {
                                position = varNameList.IndexOf(check[0]);
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1];

                                d.Set(Canvas, name, parsed);
                                d.Execute();
                                return d;
                            }
                            else if (item.Contains(check[1])) // If e.g. drawto 12,varname - position of inputted variable is found and passed to drawto class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result;

                                d.Set(Canvas, name, parsed);
                                d.Execute();
                                return d;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        Validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
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

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for drawto.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[0])) // If e.g. drawto varname,12
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for drawto.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[1])) // If e.g. drawto 12,varname
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for drawto.\n Please correct before running code."); }
                            }
                        }
                    }
                } // End of if command equals drawto

                if (command.Equals("moveto") && !methodFlag.Equals("N") && !ifStmntTrue.Equals("N") && !loopFlag.Equals("N")) // If command equals moveto
                {
                    int position = 0;
                    string result;

                    MoveTo m = (MoveTo)CommFactory.MakeCommand("moveto");
                    name = splitter[0].ToString();
                    coords = splitter[1].ToString();

                    String[] check = coords.Split(",");

                    if (methodFlag == "Y" && execute == true) // If methodFlag equals Y (meaning method is during declaration) code is passed to Method class
                    {
                        string code = name + " " + coords;
                        mthd.SetMethodLine(code); // Setting code in method declaration to Method class
                        lineMethodCounter++; // Increases line
                    }
                    if (loopFlag == "Y" && execute == true) // If loopFlag equals Y code is passed to Loop class
                    {
                        string code = name + " " + coords;
                        loop.SetLoopLines(code);
                    }
                    else if (execute == true && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) { m.Set(Canvas, name, coords); m.Execute(); return m; } // If execute is true and coords contain numbers parameters are passed to drawto class
                    else if (execute == true && regexLetters.IsMatch(check[0]) && !methodFlag.Equals("Y") && loopFlag.Equals("X") || execute == true && regexLetters.IsMatch(check[1]) && !methodFlag.Equals("Y") && loopFlag.Equals("X")) // If execute is true and first parameter or second parameter equals letter
                    {
                        String parsed;
                        foreach (string item in varNameList) // Checks if coords exists in array
                        {
                            if (item.Contains(check[0]) && item.Contains(check[1])) // If e.g. moveto varname,varname - position of inputted variable is found and passed to drawrectangle class
                            {
                                string result2;
                                position = varNameList.IndexOf(check[0]);
                                result = varValueList[position].ToString();

                                position = varNameList.IndexOf(check[1]);
                                result2 = varValueList[position].ToString();
                                parsed = result + "," + result2;

                                m.Set(Canvas, name, parsed);
                                m.Execute();
                                return m;
                            }
                            else if (item.Contains(check[0])) // If e.g. moveto varname,12 - position of inputted variable is found and passed to drawto class
                            {
                                position = varNameList.IndexOf(check[0]);
                                result = varValueList[position].ToString();
                                parsed = result + "," + check[1];

                                m.Set(Canvas, name, parsed);
                                m.Execute();
                                return m;
                            }
                            else if (item.Contains(check[1])) // If e.g. moveto 12,varname - position of inputted variable is found and passed to moveto class
                            {
                                position = varNameList.IndexOf(check[1]);
                                result = varValueList[position].ToString();
                                parsed = check[0] + "," + result;

                                m.Set(Canvas, name, parsed);
                                m.Execute();
                                return m;
                            }
                        }
                    }
                    if (execute == false && regexInt.IsMatch(check[0]) && regexInt.IsMatch(check[1])) // If execute is false and coords contains numbers
                    {
                        Validator.DrawCmdRules(command, coords, out bool valid);
                        if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
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

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for moveto.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[0])) // If e.g. moveto varname,12
                            {
                                position = varChkNameList.IndexOf(check[0]);
                                result = varChkValueList[position].ToString();
                                parsed = result + "," + check[1];

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for moveto.\n Please correct before running code."); }
                            }
                            else if (item.Contains(check[1])) // If e.g. moveto 12,varname
                            {
                                position = varChkNameList.IndexOf(check[1]);
                                result = varChkValueList[position].ToString();
                                parsed = check[0] + "," + result;

                                Validator.DrawCmdRules(command, parsed, out bool valid);
                                if (valid == true) { Canvas.DrawString(""); commandChk = "Y"; }
                                if (valid == false) { throw new GPLException("\n Cannot find declared variable for moveto.\n Please correct before running code."); }
                            }
                        }
                    }
                } // End of if command equals moveto
                else if(commandChk.Equals("N")) // If command check equals N then 
                {
                    throw new GPLException("Invalid command");
                }
                    
            } // End bracket for try
              catch (GPLException ex)
              {
                Canvas.DrawString(ex.Message);
              }
              catch (IndexOutOfRangeException ex)
              {
                Canvas.DrawString("\n No value entered for " + command + "\n");
            }
              return null;

        } // End of ParseCommand method

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
