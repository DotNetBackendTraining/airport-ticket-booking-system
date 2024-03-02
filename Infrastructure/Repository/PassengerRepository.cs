using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Domain.Repository;

namespace AirportTicketBookingSystem.Infrastructure.Repository;

public class PassengerRepository(
    IFileService<Passenger> fileService
) : IPassengerRepository
{
    private IFileService<Passenger> FileService { get; } = fileService;

    public void Add(Passenger passenger)
    {
        FileService.Append(passenger);
    }

    public Passenger? GetById(int id)
    {
        return FileService
            .ReadAll()
            .FirstOrDefault(p => p.Id == id);
    }
}