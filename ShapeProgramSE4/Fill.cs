using System;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Fill class that extends drawcommand.
    /// This class sets values of fill depending on what option the user assigns to the fill command; that being 
    /// on or off.
    /// </summary>
    public class Fill : DrawCommand
    {
        String name, status;

        /// <summary>
        /// Method to get and set status of fill command.
        /// </summary>
        public string Status
        {
            get => status;
            set => status = value;
        }

        /// <summary>
        /// Construtor for fill class.
        /// </summary>
        public Fill()
        { 
        
        }

        /// <summary>
        /// Set method for fill command.
        /// Method requires canvas object, name of command and one parameter of on/off to set the status of fillFlag.
        /// </summary>
        /// <param name="c">Canvas object</param>
        /// <param name="Name">Name of command</param>
        /// <param name="Parameters">Parameters (on/off)</param>
        public void Set(Canvas c, String Name, String Parameters)
        {
            base.Set(c, "fill", Parameters);
            this.name = Name;
            this.status = Parameters.Trim();
        }

        /// <summary>
        /// Execute method for fill command to set value of fillFlag depending on input of user.
        /// </summary>
        /// <returns>Returns true boolean value.</returns>
        public override bool Execute()
        {
            if(status == "on") { c.fillFlag = "Y"; } // If status equals on then fillFlag of canvas class equals Y
            if (status == "off") { c.fillFlag = "N"; } // If status equals off then fillFlag of canvas class equals N
            return true;
        }

        /// <summary>
        /// Overriding ToString method.
        /// </summary>
        /// <returns>Returns name of class.</returns>
        public override string ToString()
        {
            return base.ToString() + "Fill";
        }

        public override void ParseParameters(int[] parameterList)
        {
            throw new NotImplementedException();
        }

        public override void ProcessParameters(string Parameters, out int[] ParamsInt)
        {
            throw new NotImplementedException();
        }

        public override void ProcessParameters(string Parameters, out int ParamsInt)
        {
            throw new NotImplementedException();
        }

    }
}
