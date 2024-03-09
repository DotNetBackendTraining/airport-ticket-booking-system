using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class SimpleDatabaseService<TEntity> : ISimpleDatabaseService<TEntity> where TEntity : IEntity
{
    private readonly IFileService<TEntity> _fileService;

    public SimpleDatabaseService(IFileService<TEntity> fileService)
    {
        _fileService = fileService;
    }

    public IEnumerable<TEntity> GetAll() => _fileService.ReadAll();

    private bool Exists(TEntity entity) => GetAll().Any(e => e.Equals(entity));

    public async Task Add(TEntity entity)
    {
        if (Exists(entity)) throw new ArgumentException($"Entity {entity} already exists in the database");
        await _fileService.AppendAllAsync(Enumerable.Repeat(entity, 1));
    }

    public async Task Update(TEntity newEntity)
    {
        if (!Exists(newEntity))
            throw new KeyNotFoundException($"Entity {newEntity} was not found in the database");
        var cache = GetAll().ToList();
        var changes = cache.Select(e => e.Equals(newEntity) ? newEntity : e);
        await _fileService.WriteAllAsync(changes);
    }

    public async Task Delete(TEntity entity)
    {
        if (!Exists(entity))
            throw new KeyNotFoundException($"Entity {entity} was not found in the database");
        var cache = GetAll().ToList();
        var changes = cache.Where(e => !e.Equals(entity));
        await _fileService.WriteAllAsync(changes);
    }

    public IEnumerable<Task> BatchAdd(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }
}