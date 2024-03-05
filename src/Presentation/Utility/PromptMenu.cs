namespace AirportTicketBookingSystem.Presentation.Utility;

public static class PromptMenu
{
    public static void ActionMenu(string header,
        List<(string Message, Action Action)> menuOptions, string exitOptionMessage = "Exit")
    {
        Console.WriteLine(header);
        while (true)
        {
            Console.WriteLine("\nPlease select an action:");
            var i = 1;
            foreach (var ma in menuOptions)
                Console.WriteLine($"{i++}. {ma.Message}");

            Console.WriteLine($"{i}. {exitOptionMessage}");
            Console.Write("Enter option: ");

            var input = Console.ReadLine();
            var success = int.TryParse(input, out var res);
            var index = res - 1;
            if (!success || index < 0 || index > menuOptions.Count)
            {
                Console.WriteLine("Invalid option, please try again.");
                continue;
            }

            if (index == menuOptions.Count) break;
            menuOptions[index].Action.Invoke();
        }
    }
}