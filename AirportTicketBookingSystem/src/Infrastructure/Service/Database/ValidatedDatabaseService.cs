using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Common;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Interfaces.Service;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database;

/// <summary>
/// Enhances a database service with additional validation logic. This service validates entities before
/// performing CRUD operations and propagates exceptions from the underlying database service.
/// </summary>
/// <typeparam name="TEntity">The type of the entity this service manages.</typeparam>
public class ValidatedDatabaseService<TEntity> : IDatabaseService<TEntity>
    where TEntity : IEntity
{
    private readonly IDatabaseService<TEntity> _databaseService;
    private readonly IValidationService _validationService;

    public ValidatedDatabaseService(
        IDatabaseService<TEntity> databaseService,
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

    public bool Exists(TEntity entity)
    {
        return _databaseService.Exists(entity);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _databaseService.GetAll();
    }

    public async Task Add(TEntity entity)
    {
        ValidateOrThrow(entity);
        await _databaseService.Add(entity);
    }

    public async Task Update(TEntity newEntity)
    {
        ValidateOrThrow(newEntity);
        await _databaseService.Update(newEntity);
    }

    public async Task Delete(TEntity entity)
    {
        await _databaseService.Delete(entity);
    }
}