namespace AirportTicketBookingSystem.Presentation.MenuSystem;

public class MenuItem : IMenuItem
{
    public string Name { get; set; }
    private readonly Action _action;
    public MenuItem(string name, Action action)
    {
        Name = name;
        _action = action;
    }

    public void Invoke() => _action.Invoke();
}