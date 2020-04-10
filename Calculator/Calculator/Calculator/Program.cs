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