using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database;

/// <summary>
/// Validates uniqueness constraints before performing CRUD operations
/// and propagates any exceptions from the underlying crud database service.
/// </summary>
/// <typeparam name="TEntity">Type of domain entity.</typeparam>
public class DatabaseUniquenessLayer<TEntity> : ICrudDatabaseService<TEntity>
    where TEntity : IEntity
{
    private readonly IQueryDatabaseService<TEntity> _queryService;
    private readonly ICrudDatabaseService<TEntity> _crudService;

    public DatabaseUniquenessLayer(
        IQueryDatabaseService<TEntity> queryService,
        ICrudDatabaseService<TEntity> crudService)
    {
        _queryService = queryService;
        _crudService = crudService;
    }

    public async Task AddAsync(TEntity entity)
    {
        if (_queryService.Exists(entity))
            throw new DatabaseOperationException($"Entity {entity} already exists in the database");

        await _crudService.AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity newEntity)
    {
        if (!_queryService.Exists(newEntity))
            throw new DatabaseOperationException($"Entity {newEntity} was not found in the database");

        await _crudService.UpdateAsync(newEntity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        if (!_queryService.Exists(entity))
            throw new DatabaseOperationException($"Entity {entity} was not found in the database");

        await _crudService.DeleteAsync(entity);
    }
}