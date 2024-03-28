using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Interfaces;

/// <summary>
/// Defines the basic query methods for entities of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">Type of domain entity.</typeparam>
public interface IQueryDatabaseService<out TEntity>
    where TEntity : IEntity
{
    IEnumerable<TEntity> GetAll();
}