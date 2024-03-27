namespace AirportTicketBookingSystem.Presentation.MenuSystem;

public class MenuItem : IMenuItem
{
    public string Name { get; }
    private Action Action { get; }

    public MenuItem(string name, Action action)
    {
        Name = name;
        Action = action;
    }

    public void Invoke() => Action.Invoke();
}