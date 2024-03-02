namespace AirportTicketBookingSystem.Domain.Contract;

public interface IFileService<T>
{
    public IEnumerable<T> ReadAll();
    public Task AppendAsync(IEnumerable<T> entities);
}