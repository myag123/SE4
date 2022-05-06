using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Var class for variables. Extends command class.
    /// </summary>
    class Var : Command
    {
        // Declaring properities of a variable Radius = 10
        // Parser splits on space so Radius||=||10 - name, expression, value
        private String name;
        private String[] expression;
        private int value;

        /// <summary>
        /// Method to get and set var name.
        /// </summary>
        /* public String VarName
         {
             get => varName;
             set => varName = value;
         }

         public int VarValue
         {
             get => varValue;
             set => varValue = value;
         }

         public String VarExpression
         {
             get => varExpression;
             set => varExpression = value;
         }*/

        public string GetName()
        {
            return this.name;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public string[] GetExpression()
        {
            return this.expression;
        }

        public void SetExpression(String[] expression)
        {
            this.expression = expression;
        }
        public int GetValue()
        {
            return this.value;
        }

        public void SetValue(int value)
        {
            this.value = value;
        }

       

        public Var()
        { 
        
        }

        public Var(String name, String varExpression, int varVal)
        {
            base.Set("Var", varVal);
        }


      /*  public void Set(String varName, int varVal)
        {
            this.varName = varName;
            this.varValue = varVal;
        }

        public void Set(String varExpression)
        {
            this.varExpression = varExpression;
        }
      */

        public override bool Execute()
        {
            return true;
        }

      
    }
}
