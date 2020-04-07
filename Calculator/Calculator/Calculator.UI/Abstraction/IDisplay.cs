namespace Calculator.UI.Abstraction
{
    public interface IDisplay
    {
        string GetCalculationFromUser();

        void DisplayAnswer(double answer);
    }
}
