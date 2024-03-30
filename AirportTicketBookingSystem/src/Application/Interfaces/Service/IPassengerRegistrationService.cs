namespace AirportTicketBookingSystem.Application.Interfaces.Service;

public interface IPassengerRegistrationService
{
    bool IsPassengerRegistered(int passengerId);
}