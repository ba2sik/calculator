using System;
using System.Data;
using Calculator.Core;
using Calculator.Core.Abstraction;
using Calculator.Core.Helper;
using Calculator.Core.Implementation;
using Calculator.UI.Abstraction;
using Calculator.UI.Implementation;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            IDisplay display = new ConsoleDisplay();
            IParser parser = new CalculatorParser();
            MyCalculator calculator = new MyCalculator(display, parser);

            calculator.GetCalculationFromUser();
            calculator.CalculateAnswer();
            calculator.DisplayAnswwer();
        }
    }
}