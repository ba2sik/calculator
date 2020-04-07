using Calculator.UI.Abstraction;
using System;

namespace Calculator.UI.Implementation
{
    public class ConsoleDisplay : IDisplay
    {
        public string GetCalculationFromUser()
        {
            Console.WriteLine("Please Enter Calculation:");
            return Console.ReadLine();
        }

        public void DisplayAnswer(double answer)
        {
            Console.WriteLine($"The answer is: {answer}");
        }
    }
}
