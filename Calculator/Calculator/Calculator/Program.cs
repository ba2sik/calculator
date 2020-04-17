using Calculator.Core.Parser;
using Calculator.Core.Tokens.Operators;
using Calculator.UI.Display;
using System.Collections.Generic;


namespace Calculator
{
    internal class Program
    {
        static void Main()
        {
            var operators = new List<OperatorToken>
            {
                new AdditionToken(),
                new SubtractionToken(),
                new MultiplicationToken(),
                new DivisionToken(),
                new PowerToken()
            };

            IParser  parser     = new ShuntingYardParser(operators);
            IDisplay display    = new ConsoleDisplay();
            var      calculator = new MyCalculator(display, parser);

            while (true)
            {
                calculator.GetCalculationFromUser();
                calculator.CalculateAnswer();
                calculator.DisplayAnswer();
            }
        }
    }
}