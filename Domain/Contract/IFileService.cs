namespace AirportTicketBookingSystem.Domain.Contract;

public interface IFileService<T>
{
    public IEnumerable<T> ReadAll();
    public Task AppendAsync(IEnumerable<T> entities);
    public void Append(T entity);
    public bool Replace(T oldEntity, T newEntity);
    public bool Remove(T entity);
}