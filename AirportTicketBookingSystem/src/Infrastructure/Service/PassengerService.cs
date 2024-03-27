using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class PassengerService : IPassengerService
{
    private readonly IPassengerRepository _repository;
    public PassengerService(IPassengerRepository repository) => _repository = repository;

    public void Add(Passenger passenger) => _repository.Add(passenger);

    public Passenger? GetById(int id) => _repository.GetById(id);
}