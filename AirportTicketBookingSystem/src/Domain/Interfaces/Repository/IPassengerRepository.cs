namespace AirportTicketBookingSystem.Domain.Interfaces.Repository;

public interface IPassengerRepository
{
    /// <exception cref="ArgumentException">Thrown when a passenger with the same identifier already exists in the repository.</exception>
    public void Add(Passenger passenger);

    public Passenger? GetById(int id);
}