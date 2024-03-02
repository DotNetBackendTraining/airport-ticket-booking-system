namespace AirportTicketBookingSystem.Domain.Repository;

public interface IPassengerRepository
{
    public void Add(Passenger passenger);

    public Passenger GetById(int id);
}