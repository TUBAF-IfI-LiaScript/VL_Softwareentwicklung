using System;
using System.Collections.Generic;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace TestMathSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beispiele für die Verwendung des MathNet.Symbolics Paket");

            var x = Expr.Variable("x");
            var y = Expr.Variable("y");
            var a = Expr.Variable("a");
            var b = Expr.Variable("b");
            var c = Expr.Variable("c");
            var d = Expr.Variable("d");

            Console.WriteLine("a+a+a =" + (a + a + a).ToString());
            Console.WriteLine("(2 + 1 / x - 1) =" + (2 + 1 / x - 1).ToString());
            Console.WriteLine("((a / b / (c * a)) * (c * d / a) / d) =" + ((a / b / (c * a)) * (c * d / a) / d).ToString());
            Console.WriteLine("Das Ergebnis von ((a / b / (c * a)) * (c * d / a) / d) als Latex-Code lautet " + ((a / b / (c * a)) * (c * d / a) / d).ToLaTeX());
        }
    }
}
