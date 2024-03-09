namespace AirportTicketBookingSystem.Presentation.MenuSystem;

public class Menu : IMenuItem
{
    private readonly string _returnMessage;
    private readonly List<IMenuItem> _items;
    public string Name { get; set; }

    public Menu(string name, string returnMessage = "Return")
    {
        Name = name;
        _returnMessage = returnMessage;
        _items = new List<IMenuItem>();
    }
    
    public Menu AddItem(IMenuItem menuItem)
    {
        _items.Add(menuItem);
        return this;
    }
    
    public void Invoke()
    {
        while (true)
        {
            Display();
            var validIndex = ReadValidIndexInput();
            if (validIndex == _items.Count) return;
            _items[validIndex].Invoke();
        }
    }

    private void Display()
    {
        Console.WriteLine(Name);
        for (var i = 0; i < _items.Count; i++)
            Console.WriteLine($"{i + 1}. {_items[i].Name}");
        Console.WriteLine($"{_items.Count + 1}. {_returnMessage}");
    }

    private int ReadValidIndexInput()
    {
        while (true)
        {
            var input = Console.ReadLine();
            var success = int.TryParse(input, out var res);
            var index = res - 1;
            if (success && index >= 0 && index <= _items.Count)
                return index;
            Console.WriteLine("Invalid option, please try again.");
        }
    }
}