using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Application.Service;

public class PassengerRegistrationService : IPassengerRegistrationService
{
    private readonly IPassengerService _passengerService;
    public PassengerRegistrationService(IPassengerService passengerService) => _passengerService = passengerService;

    public bool IsPassengerRegistered(int passengerId) => _passengerService.GetById(passengerId) != null;
}