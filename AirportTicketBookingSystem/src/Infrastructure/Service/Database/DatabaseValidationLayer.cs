using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database;

/// <summary>
/// Validates attributes of entities before performing CRUD operations
/// and propagates any exceptions from the underlying crud database service.
/// </summary>
/// <typeparam name="TEntity">Type of domain entity.</typeparam>
public class DatabaseValidationLayer<TEntity> : ICrudDatabaseService<TEntity>
    where TEntity : IEntity
{
    private readonly ICrudDatabaseService<TEntity> _databaseService;
    private readonly IValidationService _validationService;

    public DatabaseValidationLayer(
        ICrudDatabaseService<TEntity> databaseService,
        IValidationService validationService)
    {
        _databaseService = databaseService;
        _validationService = validationService;
    }

    private void ValidateOrThrow(TEntity entity)
    {
        try
        {
            _validationService.ValidateEntityOrThrow(entity);
        }
        catch (ValidationException e)
        {
            throw new DatabaseOperationException(e.Message);
        }
    }

    public async Task AddAsync(TEntity entity)
    {
        ValidateOrThrow(entity);
        await _databaseService.AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity newEntity)
    {
        ValidateOrThrow(newEntity);
        await _databaseService.UpdateAsync(newEntity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await _databaseService.DeleteAsync(entity);
    }
}