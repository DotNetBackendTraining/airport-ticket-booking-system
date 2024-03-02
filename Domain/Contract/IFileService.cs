namespace AirportTicketBookingSystem.Domain.Contract;

public interface IFileService<T>
{
    public IEnumerable<T> ReadAll();
    public Task AppendAsync(IEnumerable<T> entities);
    public bool Replace(T oldEntity, T newEntity);
    public bool Remove(T entity);
}