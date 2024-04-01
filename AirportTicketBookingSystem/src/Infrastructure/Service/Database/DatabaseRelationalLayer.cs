using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database;

/// <summary>
/// Validates relational constraints before performing CRUD operations
/// and propagates any exceptions from the underlying crud database service.
/// </summary>
/// <typeparam name="TEntity">Type of domain entity.</typeparam>
public abstract class DatabaseRelationalLayer<TEntity> : ICrudDatabaseService<TEntity>
    where TEntity : IEntity
{
    private readonly ICrudDatabaseService<TEntity> _databaseService;

    protected DatabaseRelationalLayer(ICrudDatabaseService<TEntity> databaseService) =>
        _databaseService = databaseService;

    public async Task AddAsync(TEntity entity)
    {
        ValidateAddOrThrow(entity);
        await _databaseService.AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity newEntity)
    {
        ValidateUpdateOrThrow(newEntity);
        await _databaseService.UpdateAsync(newEntity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        ValidateDeleteOrThrow(entity);
        await _databaseService.DeleteAsync(entity);
    }

    /// <summary>
    /// Throws <see cref="DatabaseRelationalException"/> if <see cref="AddAsync"/> operation is not allowed.
    /// </summary>
    protected abstract void ValidateAddOrThrow(TEntity entity);

    /// <summary>
    /// Throws <see cref="DatabaseRelationalException"/> if <see cref="UpdateAsync"/> operation is not allowed.
    /// </summary>
    protected abstract void ValidateUpdateOrThrow(TEntity newEntity);

    /// <summary>
    /// Throws <see cref="DatabaseRelationalException"/> if <see cref="DeleteAsync"/> operation is not allowed.
    /// </summary>
    protected abstract void ValidateDeleteOrThrow(TEntity entity);
}