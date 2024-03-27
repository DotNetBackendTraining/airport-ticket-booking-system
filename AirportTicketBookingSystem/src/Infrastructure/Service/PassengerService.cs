using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class PassengerService : IPassengerService
{
    private IPassengerRepository Repository { get; }
    public PassengerService(IPassengerRepository repository) => Repository = repository;

    public void Add(Passenger passenger) => Repository.Add(passenger);

    public Passenger? GetById(int id) => Repository.GetById(id);
}