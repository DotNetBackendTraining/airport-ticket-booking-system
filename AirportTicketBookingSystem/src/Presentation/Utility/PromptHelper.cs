namespace AirportTicketBookingSystem.Presentation.Utility;

public static class PromptHelper
{
    public static bool PromptYesNo(string message)
    {
        while (true)
        {
            Console.Write(message);
            switch (Console.ReadLine()?.Trim().ToLower())
            {
                case "y":
                case "yes":
                    return true;
                case "n":
                case "no":
                    return false;
                default:
                    Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
                    break;
            }
        }
    }

    // Loop until valid input is received or the user chooses not to retry
    public static bool TryPromptForInput<T>(string promptMessage, Func<string, T> parseFunction, out T? result)
    {
        while (true)
        {
            Console.Write(promptMessage);
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                result = default;
                return false;
            }

            try
            {
                result = parseFunction(input);
                return true;
            }
            catch (FormatException)
            {
                var retry = PromptYesNo("Invalid input format! Would you like to try again? (y/n): ");
                if (retry) continue;

                result = default;
                return false;
            }
        }
    }
}