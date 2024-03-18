namespace AirportTicketBookingSystem.Presentation.MenuSystem;

public interface IMenuItem
{
    public string Name { get; }
    public void Invoke();
}