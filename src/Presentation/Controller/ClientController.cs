namespace AirportTicketBookingSystem.Presentation.Controller;

public class ClientController(IServiceProvider serviceProvider)
{
    private IServiceProvider Provider { get; } = serviceProvider;

    public void Start()
    {
    }
}