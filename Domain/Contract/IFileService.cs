namespace AirportTicketBookingSystem.Domain.Contract;

public interface IFileService<TEntity>
{
    public IEnumerable<TEntity> ReadAll();
    public Task AppendAsync(IEnumerable<TEntity> entities);
    public void Append(TEntity entity);
    public bool Replace(TEntity oldEntity, TEntity newEntity);
    public bool Remove(TEntity entity);
}