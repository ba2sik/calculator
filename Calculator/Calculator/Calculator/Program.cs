using Calculator.Core.Parser;
using Calculator.UI.Abstraction;
using Calculator.UI.Implementation;


namespace Calculator
{
    internal class Program
    {
        static void Main()
        {
            IParser parser = new ShuntingYardParser();
            IDisplay display = new ConsoleDisplay();
            var calculator = new MyCalculator(display, parser);

            while (true)
            {
                calculator.GetCalculationFromUser();
                calculator.CalculateAnswer();
                calculator.DisplayAnswer();
            }
        }
    }
}