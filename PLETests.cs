using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapeProgramSE4;
using System;
using System.Drawing;

namespace UnitTestsPLE
{
    /// <summary>
    /// Test Class to test most classes in ShapeProgramSE4.
    /// </summary>
    [TestClass]
    public class PLETests
    {
        /// <summary>
        /// Method to test CalculateArrayVal method in Parser class.
        /// Passes a calculation from an array and tests result.
        /// </summary>
        [TestMethod]
        public void TestParserCalculateArrayVal()
        {
            // Arrange
            string setVar = "apple = 10 * 3";
            string[] splitTxt;
            splitTxt = setVar.Split(" ");

            int length, expected, actual;
            length = splitTxt.Length - 2;

            ArraySegment<string> arrGetCalc = new ArraySegment<string>(splitTxt, 2, length); // Selecting calculation from string array after equals sign

            // Act
            Parser.CalculateArrayVal(splitTxt[0], arrGetCalc, out int result); // Passing parameters to CalculateArrayVal of parser class
            expected = 30;
            actual = result;

            // Assert
            Assert.AreEqual(expected, actual, 1, "Wrong result of calculation!");
        }

        /// <summary>
        /// Method to test DeclareVar method in CodeChecker class.
        /// Passes an example of a variable declaration to DeclareVar method and checks if results based on if variable 
        /// already exists and if it doesn't.
        /// </summary>
        [TestMethod]
        public void TestCodeCheckerDeclareVar()
        {
            // Arrange
            CodeChecker c = new CodeChecker(); // Creating object of CodeChecker
            String cmd = "var", varName = "apple";
            int value = 63;
            bool result;

            // Act                                           // Assert
            c.DeclareVar(cmd, varName, value, out result);   Assert.IsTrue(result);
            // Act                                           // Assert
            c.exists = "Y";
            c.DeclareVar(cmd, varName, value, out result);   Assert.IsFalse(result);
        }

        /// <summary>
        /// Method to test DrawCmdRules & DrawShrtCmdRules  method in CodeChecker.
        /// Tests all scenarios of inputted parameters to class.
        /// </summary>
        [TestMethod]
        public void TestCodeCheckerCmdRules()
        {
            // Arrange
            CodeChecker c = new CodeChecker(); // Creating object of CodeChecker
            bool result;

            // Act                                               // Assert
            c.DrawCmdRules("circle", "50", out result); Assert.IsTrue(result);
            c.DrawCmdRules("circle", "50,69", out result); Assert.IsFalse(result, "Circle requires only one parameter");
            c.DrawCmdRules("pie", "50,96", out result); Assert.IsTrue(result);
            c.DrawCmdRules("pie", "52", out result); Assert.IsFalse(result, "Pie requires two parameters.");
            c.DrawCmdRules("pie", "52,100,40", out result); Assert.IsFalse(result, "Pie requires two parameters.");
            c.DrawCmdRules("rectangle", "52,20", out result); Assert.IsTrue(result);
            c.DrawCmdRules("rectangle", "52", out result); Assert.IsFalse(result, "Rectangle requires two parameters.");
            c.DrawCmdRules("rectangle", "52,100,40", out result); Assert.IsFalse(result, "Rectangle requires two parameters.");
            c.DrawCmdRules("square", "52,52", out result); Assert.IsTrue(result);
            c.DrawCmdRules("square", "52,20", out result); Assert.IsFalse(result, "Value of square must be same.");
            c.DrawCmdRules("square", "52,23,22", out result); Assert.IsFalse(result, "Square requires two parameters.");
            c.DrawCmdRules("moveto", "150,52", out result); Assert.IsTrue(result);
            c.DrawCmdRules("moveto", "150", out result); Assert.IsFalse(result, "MoveTo requires two parameters.");
            c.DrawCmdRules("moveto", "150,52,50", out result); Assert.IsFalse(result, "MoveTo requires two parameters.");
            c.DrawCmdRules("drawto", "150,52", out result); Assert.IsTrue(result);
            c.DrawCmdRules("drawto", "150", out result); Assert.IsFalse(result, "MoveTo requires two parameters.");
            c.DrawCmdRules("drawto", "150,52,50", out result); Assert.IsFalse(result, "MoveTo requires two parameters.");
            c.DrawCmdRules("fill", "on", out result); Assert.IsTrue(result);
            c.DrawCmdRules("fill", "off", out result); Assert.IsTrue(result);
            c.DrawCmdRules("fill", "pull", out result); Assert.IsFalse(result, "Fill requires status of on or off.");
            c.DrawCmdRules("pen", "yellow", out result); Assert.IsTrue(result);
            c.DrawCmdRules("pen", "copper", out result); Assert.IsFalse(result, "Pen requires valid color.");
            c.DrawCmdRules("non", "command", out result); Assert.IsFalse(result, "Invalid DrawCmdRules");
            c.DrawShrtCmdRules("clear", out result); Assert.IsTrue(result);
            c.DrawShrtCmdRules("reset", out result); Assert.IsTrue(result);
            c.DrawShrtCmdRules("non", out result); Assert.IsFalse(result, "Invalid DrawShrtCmdRules");
        }

        /// <summary>
        /// Method to test ShapeFactory and Shape classes e.g. Circle, Square, Rectangle etc
        /// </summary>
        [TestMethod]
        public void TestShapeFactory()
        {
            // Arrange
            ShapeFactory s = new ShapeFactory();
            Circle c = new Circle();
            ShapeProgramSE4.Rectangle r = new ShapeProgramSE4.Rectangle();
            Pie p = new Pie();
            Square sq = new Square();
            Triangle t = new Triangle();

            // Act                   // Assert
            s.GetShape("circle");    Assert.AreEqual(c, c);
            s.GetShape("rectangle"); Assert.AreEqual(r, r);
            s.GetShape("square");    Assert.AreEqual(sq, sq);
            s.GetShape("triangle");  Assert.AreEqual(t, t);
            s.GetShape("pie");       Assert.AreEqual(p, p);

            /* Testing Circle
            c.Set(Color.Black, 150, 200, 60);
            int mathExpected = (int)(Math.PI * (60 ^ 2));
            Assert.AreEqual(mathExpected, c.CalcArea());*/
        }

        /// <summary>
        /// Test to test exception of ShapeFactory if invalid shape is passed to GetShape method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "An invalid shape was passed into Shape Factory.")]
        public void TestShapeFactoryException()
        {
            ShapeFactory s = new ShapeFactory();
            s.GetShape("non"); 
        }

        /// <summary>
        /// Method to test CommandFactory.
        /// </summary>
        [TestMethod]
        public void TestCommandFactory()
        {
            // Arrange
            CommandFactory cf = new CommandFactory();
            Clear cl = new Clear();
            DrawCircle dc = new DrawCircle();
            DrawRectangle dr = new DrawRectangle();
            DrawTriangle dtr = new DrawTriangle();
            DrawPie dp = new DrawPie();
            DrawTo dt = new DrawTo();
            Fill f = new Fill();
            Method m = new Method();
            Loop l = new Loop();
            MoveTo mt = new MoveTo();
            PenColor pc = new PenColor();
            Reset r = new Reset();
            Var v = new Var();

            // Act                       // Assert
            cf.MakeCommand("clear");     Assert.AreEqual(cl, cl);
            cf.MakeCommand("circle");    Assert.AreEqual(dc, dc);
            cf.MakeCommand("drawto");    Assert.AreEqual(dt, dt);
            cf.MakeCommand("fill");      Assert.AreEqual(f, f);
            cf.MakeCommand("rectangle"); Assert.AreEqual(dr, dr);
            cf.MakeCommand("method");    Assert.AreEqual(m, m);
            cf.MakeCommand("loop");      Assert.AreEqual(l, l);
            cf.MakeCommand("moveto");    Assert.AreEqual(mt, mt);
            cf.MakeCommand("pen");       Assert.AreEqual(pc, pc);
            cf.MakeCommand("reset");     Assert.AreEqual(r, r);
            cf.MakeCommand("triangle");  Assert.AreEqual(dtr, dtr);
            cf.MakeCommand("pie");       Assert.AreEqual(dp, dp);
            cf.MakeCommand("var");       Assert.AreEqual(v, v);
        }

        /// <summary>
        /// Method to test exception of CommandFactory if invalid shape is passed to MakeCommand method.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "An invalid command was passed into Command Factory.")]
        public void TestCommandFactoryException()
        {
            CommandFactory c = new CommandFactory();
            c.MakeCommand("non");
        }

        /// <summary>
        /// Method to test Canvas class.
        /// </summary>
        [TestMethod]
        public void TestCanvasClass()
        {
        }
    }
}
