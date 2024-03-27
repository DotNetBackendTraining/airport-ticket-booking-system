namespace AirportTicketBookingSystem.Presentation.MenuSystem;

public class Menu : IMenuItem
{
    public string Name { get; }
    private string ReturnMessage { get; }
    private List<IMenuItem> Items { get; } = [];

    public Menu(string name, string returnMessage = "Return")
    {
        Name = name;
        ReturnMessage = returnMessage;
    }

    public Menu AddItem(IMenuItem menuItem)
    {
        Items.Add(menuItem);
        return this;
    }

    public void Invoke()
    {
        while (true)
        {
            Display();
            var validIndex = ReadValidIndexInput();
            if (validIndex == Items.Count) return;
            Items[validIndex].Invoke();
        }
    }

    private void Display()
    {
        Console.WriteLine(Name);
        for (var i = 0; i < Items.Count; i++)
            Console.WriteLine($"{i + 1}. {Items[i].Name}");
        Console.WriteLine($"{Items.Count + 1}. {ReturnMessage}");
    }

    private int ReadValidIndexInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            var success = int.TryParse(input, out var res);
            var index = res - 1;
            if (success && index >= 0 && index <= Items.Count)
                return index;
            Console.WriteLine("Invalid option, please try again.");
        }
    }
}