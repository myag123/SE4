﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShapeProgramSE4
{
    /// <summary>
    /// This class will allow keywords to be highlighted when the user types commands into the command line/box
    /// of the application.
    /// </summary>
    class Keywords
    {
        //declaring variables
       private RichTextBox t1;
       private String text;
       private Color color;
       private int startIndex;

        /// <summary>
        /// Constructor for Keywords class.
        /// </summary>
        public Keywords()
        {
            
        }


        /// <summary>
        /// Method to check syntax keyword user types into box, will turn green if text is a keyword in 
        /// graphical programming language application.
        /// </summary>
        /// <param name="t1">RichTextBox type</param>
        /// <param name="text">Keyword user types e.g. 'drawto'.</param>
        /// <param name="color">Parameter for colour of keyword. </param>
        /// <param name="StartIndex">Position selector will start from.</param>
        public void chkKeyword(RichTextBox t1, String text, Color color, int startIndex)
        {
            //assigning variables
            this.t1 = t1;
            this.text = text;
            this.color = color;
            this.startIndex = startIndex;

            // if user types keyword in either textbox on form then colour of word is changed
            if (t1.Name.Equals("richTxtCmdLine") || t1.Name.Equals("richTxtCmdBox"))
            {
                if (t1.Text.Contains(text))
                {
                    int index = -1;
                    int selectStart = t1.SelectionStart;

                    //while keyword is found in text then specified colour is selected
                    while ((index = t1.Text.IndexOf(text, (index + 1))) != -1)
                    {
                        t1.SelectionColor = Color.Black; //ensure start word typed is black 
                        t1.Select((index + startIndex), text.Length);
                        t1.SelectionColor = color;
                        t1.Select(selectStart, 0);
                        t1.SelectionColor = Color.Black; //return colour to black
                    }
                }
            }
        }
    }
}
