using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicMath
{
    /// <summary>
    /// This is a basic math class that compiles to a dll
    /// this project is referenced into the FodyTests project
    /// </summary>
    public static class Math
    {

        public static double Add(double a, double b) { return a + b; }

        public static double Subtract(double a, double b) { return a - b; }

        public static double Multiply(double a, double b) { return a * b; }

        public static double Divide(double a, double b) { return a / b; }
    }
}
