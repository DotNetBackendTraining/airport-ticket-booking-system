using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Interfaces.Service;

namespace AirportTicketBookingSystem.Application.Service;

public class FlightManagementService : IFlightManagementService
{
    private readonly IFlightService _flightService;
    public FlightManagementService(IFlightService flightService) => _flightService = flightService;

    public async Task<OperationResult<Flight>> AddFlightAsync(Flight flight)
    {
        try
        {
            await _flightService.AddAsync(flight);
            return new OperationResult<Flight>(
                Success: true,
                Message: "Flight creation completed successfully",
                Item: flight);
        }
        catch (DatabaseException e)
        {
            return new OperationResult<Flight>(
                Success: false,
                Message: "Flight creation failed:  " + e.Message,
                Item: flight);
        }
    }
}