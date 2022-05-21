using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Class to validate code before it is executed.
    /// </summary>
    public class CodeChecker
    {
        // Declaring array lists for types of commands
        public ArrayList drwCmds = new ArrayList();
        public ArrayList shrtDrwCmds = new ArrayList();
        public ArrayList varCmds = new ArrayList();

        Regex regexInt = new Regex(@"[\d]"); // Creating regular expression for numbers only
        Regex regexLetters = new Regex(@"^[a-zA-Z]+$"); // Creating regular expression for letters only

        public string exists;

        /// <summary>
        /// Constructor for code checker class.
        /// Adds command name values to array list.
        /// </summary>
        public CodeChecker()
        {
            drwCmds.Add("circle");    // [0]
            drwCmds.Add("square");    // [1]
            drwCmds.Add("triangle");  // [2]
            drwCmds.Add("rectangle"); // [3]
            drwCmds.Add("drawto");    // [4]
            drwCmds.Add("moveto");    // [5]
            drwCmds.Add("pen");       // [6]
            drwCmds.Add("fill");      // [7]
            drwCmds.Add("pie");       // [8]
            drwCmds.Add("loop");      // [9]

            shrtDrwCmds.Add("clear"); // [0]
            shrtDrwCmds.Add("reset"); // [1]
            shrtDrwCmds.Add("method"); // [2]
            shrtDrwCmds.Add("endmethod"); // [3]
            shrtDrwCmds.Add("endloop"); // [4]

            varCmds.Add("var"); // [0]
        }

        /// <summary>
        /// Method that validates all draw commands that require 2 parameters.
        /// </summary>
        /// <param name="cmd">Name of command</param>
        /// <param name="value">Contents of parameters for command</param>
        /// <param name="res">Result of res boolean after command has been validated</param>
        /// <returns>true or false</returns>
        public bool DrawCmdRules(String cmd, String value, out bool res)
        {
            if (cmd.Equals(drwCmds[0]) == true) // If command equals circle
            {
                value.Trim();
                if (value.Contains(",") == true) { res = false; return res; } // If parameter of circle contains a comma then res is false as circle requires only one parameter
                if (regexInt.IsMatch(value)) { res = true; return res; }  // If parameter of circle is a number
                if(regexLetters.IsMatch(value)) { res = true; return res; } // If parameter of circle is letters of a variable then res equals true
                else { res = false; return res; } 
            }

            // If command is any of the draw commands with more than one parameter required 
            else if (cmd.Equals(drwCmds[2]) == true || cmd.Equals(drwCmds[3]) == true|| cmd.Equals(drwCmds[4]) == true || cmd.Equals(drwCmds[5]) == true || cmd.Equals(drwCmds[8]) == true)
            {
                String[] splitter;
                splitter = value.Split(","); // Splits value up by comma
                if(splitter.Length.Equals(1)||splitter.Length > 2) { res = false; return res; } // If value contains one or more than two parameters
                if (regexInt.IsMatch(splitter[0]) && regexInt.IsMatch(splitter[1])){ res = true;} else { res = false; } // If command values contains just numbers then res = true
                return res;
            }
            else if (cmd.Equals(drwCmds[1]) == true) // If command equals square
            {
                String[] splitter;
                splitter = value.Split(",");
                if (splitter.Length.Equals(1) || splitter.Length > 2) { res = false; return res; } // If value contains one or more than two parameters
                if (splitter[0].Contains(splitter[1]) == true && regexInt.IsMatch(splitter[0]) && regexInt.IsMatch(splitter[1])) // If parameters equal same number and are both numbers
                { res = true; return res; } else { res = false; return res; }
            }
            else if (cmd.Equals(drwCmds[7]) == true) // If command equals fill
            {
                value.Trim();
                if (value.Equals("on") || value.Equals("off")){ res = true; } else { res = false; } // If parameters equal on or off then res equals true
            }
            else if(cmd.Equals(drwCmds[6]) ==  true)  // If command equals pen
            {
                value.Trim();
                try
                {
                    if (ColorTranslator.FromHtml(value).IsKnownColor == true) // If value is a color that exists res is true 
                    {
                        res = true;
                        return res;
                    }
                    else { res = false; }
                }
                catch (ArgumentException ex)
                {
                    res = false;
                }
            }
            else if(cmd.Equals(drwCmds[9]) == true) // If command equals loop
            {
                if(regexInt.IsMatch(value) == true)
                { res = true; return res; }
                else { res = false; }
            }
            else { res = false; }
            return res;
        }

        /// <summary>
        /// Method that validates short commands that require no parameter such as clear, reset.
        /// </summary>
        /// <param name="cmd">Name of command</param>
        /// <param name="res">Result of res boolean after command has been validated</param>
        /// <returns>true or false</returns>
        public bool DrawShrtCmdRules(String cmd, out bool res)
        {
            // If command equals clear, reset, method, endmethod or endloop then res equals true
            if (cmd.Equals(shrtDrwCmds[0]) == true || cmd.Equals(shrtDrwCmds[1]) == true || cmd.Equals(shrtDrwCmds[2]) == true || cmd.Equals(shrtDrwCmds[3]) == true || cmd.Equals(shrtDrwCmds[4]) == true) { res = true; return res; }
            else { res = false; }
            return res;
        }

        /// <summary>
        /// Method for when variables are declared.
        /// </summary>
        /// <param name="cmd">Full command</param>
        /// <param name="name">Variable name</param>
        /// <param name="value">Int value of variable</param>
        /// <param name="res">Result of res boolean after command has been validated</param>
        /// <returns>true or false</returns>
        public bool DeclareVar(String cmd, String name, int value, out bool res)
        {
            //If command equals var and command contains only letters and value contains only numbers
            if(cmd.Equals(varCmds[0]) == true && regexLetters.IsMatch(name) && regexInt.IsMatch(value.ToString()) && exists != "Y")
            { res = true; return res; }
            else if (exists.Equals("Y") == true)
            { res = false; return res; }
            else{ res = false; return res; }
        }

        /// <summary>
        /// Method for when variable is set to a value.
        /// </summary>
        /// <param name="name">variable name</param>
        /// <param name="value">Int value of variable</param>
        /// <param name="res">Result of res boolean after command has been validated</param>
        /// <returns>true or false</returns>
        public bool SetDeclaredVar(String name, int value, out bool res)
        {
            if (regexLetters.IsMatch(name) && regexInt.IsMatch(value.ToString())) //If command equals var and command contains only letters and value contains only numbers
            { res = true;
                return res;
            }
            else { res = false; }
            return res;
        }

        /// <summary>
        /// Method to check variable before it is passed into Loop class.
        /// </summary>
        /// <param name="var">variable claculation</param>
        /// <param name="res">return true or false</param>
        /// <returns></returns>
        public bool DeclareVarForLoop(String var, out bool res)
        {
            if(var.Contains("=") || var.Contains("*") || var.Contains("+") || var.Contains("-")|| var.Contains("/"))
            {   
                res = true;
                return res;
            }
            else { res = false; }
            return res;
        }

        /// <summary>
        /// Method to check if statement.
        /// </summary>
        /// <param name="name">If statement</param>
        /// <param name="res">Result of res boolean after command has been validated</param>
        /// <returns>true or false</returns>
        public bool IfStatement(String name, out bool res)
        {
            if(name.Contains("if") == true && name.Contains("==") == true) // If statement contains if and == then res is true
            {
                res = true;
                return res;
            }

            if(name.Equals("endif")) // If statement contains endif then res is true
            {
                res = true;
                return res;
            }
            else { res = false; }
            return res;
        }
    }
}
