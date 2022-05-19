using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShapeProgramSE4
{
    /// <summary>
    /// This class will allow keywords to be highlighted when the user types commands into the command line/box
    /// of the application. This allows the user to be aware of the syntax/keywords for the application
    /// </summary>
    class Keywords
    {
        // Declaring variables
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
            // Assigning variables
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

                    // While keyword is found in text then specified colour is selected
                    while ((index = t1.Text.IndexOf(text, (index + 1))) != -1)
                    {
                        t1.SelectionColor = Color.Black; // Ensure start word typed is black 
                        t1.Select((index + startIndex), text.Length);
                        t1.SelectionColor = color;
                        t1.Select(selectStart, 0);
                        t1.SelectionColor = Color.Black; // Return colour to black
                    }
                }
            }
        }
    }
}
