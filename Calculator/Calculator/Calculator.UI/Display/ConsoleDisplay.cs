using System;

namespace Calculator.UI.Display
{
    public class ConsoleDisplay : IDisplay
    {
        public string GetCalculationFromUser()
        {
            Console.WriteLine("Please Enter Calculation:");
            return Console.ReadLine();
        }

        public void DisplayAnswer(string answer)
        {
            Console.WriteLine(answer);
        }
    }
}
