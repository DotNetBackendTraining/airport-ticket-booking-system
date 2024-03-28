using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database;

/// <summary>
/// Provides a persistence service for entities of type <typeparamref name="TEntity"/>,
/// implementing both CRUD and query functionalities using a file-based storage mechanism.
/// </summary>
/// <typeparam name="TEntity">The type of the entity this service manages.</typeparam>
public class DatabasePersistenceService<TEntity> : IQueryDatabaseService<TEntity>, ICrudDatabaseService<TEntity>
    where TEntity : IEntity
{
    private readonly IFileService<TEntity> _fileService;
    public DatabasePersistenceService(IFileService<TEntity> fileService) => _fileService = fileService;

    public IEnumerable<TEntity> GetAll() => _fileService.ReadAll();

    public bool Exists(TEntity entity) => GetAll().Any(e => e.Equals(entity));

    public async Task Add(TEntity entity)
    {
        await _fileService.AppendAllAsync(Enumerable.Repeat(entity, 1));
    }

    public async Task Update(TEntity newEntity)
    {
        var cache = GetAll().ToList();
        var changes = cache.Select(e => e.Equals(newEntity) ? newEntity : e);
        await _fileService.WriteAllAsync(changes);
    }

    public async Task Delete(TEntity entity)
    {
        var cache = GetAll().ToList();
        var changes = cache.Where(e => !e.Equals(entity));
        await _fileService.WriteAllAsync(changes);
    }
}