﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Var class for variables. Extends command class.
    /// </summary>
    public class Var : Command
    {
        private String name;
        private int value;
        private ArrayList varNameList = new ArrayList();
        private ArrayList varValueList = new ArrayList();

        /// <summary>
        /// Method to set var name.
        /// </summary>
        public void SetName(String name)
        {
            this.name = name;
            varNameList.Add(name);
        }

        /// <summary>
        /// Method to get var name.
        /// </summary>
        public string GetName()
        {
            return this.name;
        }

        public void SetValue(int value)
        {
            this.value = value;
            varValueList.Add(value);
        }

        public int GetValue()
        {
            return this.value;
        }


        public Var()
        { 
        
        }

        public Var(String name, String varExpression, int varVal)
        {
            base.Set("Var", varVal);
        }
        

        public override bool Execute()
        {
            return true;
        }

      
    }
}
