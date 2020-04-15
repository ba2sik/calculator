using Calculator.Core.Abstraction;
using Calculator.Core.Parser;
using Calculator.UI.Abstraction;
using Calculator.UI.Implementation;


namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //char[] operators = { '+', '-', '*', '/', '^' };
            //IParser parser = new ShuntingYardParser(operators);
            IParser parser = new PShuntingYardParser();
            IDisplay display = new ConsoleDisplay();
            MyCalculator calculator = new MyCalculator(display, parser);

            while (true)
            {
                calculator.GetCalculationFromUser();
                calculator.CalculateAnswer();
                calculator.DisplayAnswwer();
            }
        }
    }
}