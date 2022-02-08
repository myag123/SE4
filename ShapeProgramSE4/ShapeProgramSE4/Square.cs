using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
/*
 * Square class extends rectangle as a square shares most of the same properties with a rectangle. 
 * Only difference is that one size is applied to all sides of a square.
 * Therefore size is applied to Rectangle
 */
namespace ShapeProgramSE4
{
    class Square : Rectangle //extends rectangle
    {
        private int size; //only one size can be applied to all sides of a square

        public Square() :base() //explicitly calling constructor rectangle
        { 

        }

        public Square(Color colour, int x, int y, int size) : base(colour, x, y, size, size)
        {
            this.size = size;
        }

        public override void draw(Graphics g)
        {
            base.draw(g); //references draw method in Rectangle class
        }
    }
}
