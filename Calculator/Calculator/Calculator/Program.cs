using Calculator.Core;
using Calculator.Core.Abstraction;
using Calculator.Core.Implementation;
using Calculator.UI.Abstraction;
using Calculator.UI.Implementation;
using System.Collections.Generic;


namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<CalculatorToken> a = new List<CalculatorToken>();
            //a.Add(new CalculatorToken(TokenTypes.Literal, "-12"));
            //a.Add(new CalculatorToken(TokenTypes.Operator, "*"));
            //a.Add(new CalculatorToken(TokenTypes.Literal, "34"));

            //List<CalculatorToken> b = new List<CalculatorToken>();
            //b.Add(new CalculatorToken(TokenTypes.Literal, "-12"));
            //b.Add(new CalculatorToken(TokenTypes.Operator, "*"));
            //b.Add(new CalculatorToken(TokenTypes.Literal, "34"));


            //System.Console.WriteLine(a.GetHashCode());
            //System.Console.WriteLine(b.GetHashCode());
            char[] operators = { '+', '-', '*', '/', '^' };
            IParser parser = new CalculatorParser(operators);
            IDisplay display = new ConsoleDisplay();
            MyCalculator calculator = new MyCalculator(display, parser);

            calculator.GetCalculationFromUser();
            calculator.CalculateAnswer();
            calculator.DisplayAnswwer();
        }
    }
}