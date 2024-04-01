using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Application.Interfaces.Service;

public interface IFlightManagementService
{
    Task<OperationResult<Flight>> AddFlightAsync(Flight flight);
}