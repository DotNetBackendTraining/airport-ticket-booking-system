namespace AirportTicketBookingSystem.Presentation.Controller;

public class ManagerController(IServiceProvider serviceProvider)
{
    private IServiceProvider Provider { get; } = serviceProvider;

    public void Start()
    {
    }
}