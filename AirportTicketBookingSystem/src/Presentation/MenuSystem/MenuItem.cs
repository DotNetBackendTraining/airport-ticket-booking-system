namespace AirportTicketBookingSystem.Presentation.MenuSystem;

public class MenuItem(string name, Action action) : IMenuItem
{
    public string Name { get; } = name;
    private Action Action { get; } = action;

    public void Invoke() => Action.Invoke();
}