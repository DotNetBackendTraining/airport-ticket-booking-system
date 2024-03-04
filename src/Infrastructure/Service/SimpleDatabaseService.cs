using AirportTicketBookingSystem.Domain.Contract;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class SimpleDatabaseService<TEntity> : ISimpleDatabaseService<TEntity>
    where TEntity : IEntity
{
    private IFileService<TEntity> FileService { get; }
    private List<TEntity> Cache { get; }

    public SimpleDatabaseService(IFileService<TEntity> fileService)
    {
        FileService = fileService;
        Cache = LoadData().ToList();
    }

    private IEnumerable<TEntity> LoadData() => FileService.ReadAll();

    public IEnumerable<TEntity> GetAll() => Cache;

    public void Add(TEntity entity)
    {
        if (Cache.Any(e => e.Equals(entity)))
            throw new ArgumentException($"Entity {entity} already exists in the database");
        Cache.Add(entity);
        SaveChanges();
    }

    public void Update(TEntity newEntity)
    {
        var i = Cache.IndexOf(newEntity);
        if (i == -1)
            throw new KeyNotFoundException($"Entity {newEntity} was not found in the database");
        Cache[i] = newEntity;
        SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        if (Cache.Remove(entity)) SaveChanges();
        else throw new KeyNotFoundException($"Entity {entity} was not found in the database");
    }

    private void SaveChanges() => FileService.WriteAllAsync(Cache).Wait();
}