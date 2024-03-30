using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Application.Interfaces.Service;

public interface IFlightManagementService
{
    OperationResult<Flight> AddFlight(Flight flight);
}