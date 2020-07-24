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

            //CR: I am confused by the spacing here, what is this formatting?
            IParser  parser     = new ShuntingYardParser(operators);
            IDisplay display    = new ConsoleDisplay();
            var      calculator = new MyCalculator(display, parser);

            while (true) //CR: how do you stop?
            {
                calculator.GetCalculationFromUser();
                calculator.CalculateAnswer();
                calculator.DisplayAnswer(); //CR: since when do you need to do anything for the calculator to display the answer? calculators calculate stuff and give you the answer, you never have a button that says "display answer", only a "calculate" button
            }
        }
    }
}