using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database;

/// <summary>
/// Provides basic CRUD operations for managing entities in the database. This service enforces simple
/// constraints such as uniqueness and existence, but does not manage relational constraints or dependencies.
/// </summary>
/// <typeparam name="TEntity">The type of the entity this service manages.</typeparam>
public class DatabaseService<TEntity>(IFileService<TEntity> fileService)
    : IDatabaseService<TEntity> where TEntity : IEntity
{
    private IFileService<TEntity> FileService { get; } = fileService;

    public IEnumerable<TEntity> GetAll() => FileService.ReadAll();

    private bool Exists(TEntity entity) => GetAll().Any(e => e.Equals(entity));

    public async Task Add(TEntity entity)
    {
        if (Exists(entity)) throw new ArgumentException($"Entity {entity} already exists in the database");
        await FileService.AppendAllAsync(Enumerable.Repeat(entity, 1));
    }

    public async Task Update(TEntity newEntity)
    {
        if (!Exists(newEntity))
            throw new KeyNotFoundException($"Entity {newEntity} was not found in the database");
        var cache = GetAll().ToList();
        var changes = cache.Select(e => e.Equals(newEntity) ? newEntity : e);
        await FileService.WriteAllAsync(changes);
    }

    public async Task Delete(TEntity entity)
    {
        if (!Exists(entity))
            throw new KeyNotFoundException($"Entity {entity} was not found in the database");
        var cache = GetAll().ToList();
        var changes = cache.Where(e => !e.Equals(entity));
        await FileService.WriteAllAsync(changes);
    }
}