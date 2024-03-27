namespace AirportTicketBookingSystem.Domain.Interfaces.Service;

/// <summary>
/// Responsible for implementing criteria filtering logic on the entity type.
/// </summary>
/// <typeparam name="TEntity">Type of entity to be filtered</typeparam>
/// <typeparam name="TCriteria">Type of criteria embedding the filtering logic</typeparam>
public interface IFilteringService<TEntity, in TCriteria> where TEntity : IEntity
{
    IEnumerable<TEntity> Filter(IEnumerable<TEntity> entities, TCriteria criteria);
}