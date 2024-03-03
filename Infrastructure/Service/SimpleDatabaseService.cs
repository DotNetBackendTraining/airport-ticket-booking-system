using AirportTicketBookingSystem.Domain.Contract;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class SimpleDatabaseService<TEntity> : ISimpleDatabaseService<TEntity>
{
    private IFileService<TEntity> FileService { get; }
    private List<TEntity> Cache { get; }
    private bool CacheIsDirty { get; set; }

    public SimpleDatabaseService(IFileService<TEntity> fileService)
    {
        FileService = fileService;
        Cache = LoadData().ToList();
    }

    private IEnumerable<TEntity> LoadData() => FileService.ReadAll();

    public IEnumerable<TEntity> GetAll() => Cache;

    public void Add(TEntity entity)
    {
        if (Cache.Any(e => e != null && e.Equals(entity)))
            throw new ArgumentException($"Entity {entity} already exists in the database");
        Cache.Add(entity);
        CacheIsDirty = true;
    }

    public void Update(TEntity newEntity)
    {
        var i = Cache.IndexOf(newEntity);
        if (i == -1)
            throw new KeyNotFoundException($"Entity {newEntity} was not found in the database");
        Cache[i] = newEntity;
        CacheIsDirty = true;
    }

    public bool Delete(TEntity entity)
    {
        if (Cache.Remove(entity))
            return CacheIsDirty = true;
        return false;
    }

    public void Dispose()
    {
        if (CacheIsDirty) SaveChangesAsync().Wait();
    }

    protected virtual async Task SaveChangesAsync()
    {
        if (!CacheIsDirty) return;
        await FileService.WriteAllAsync(Cache);
        CacheIsDirty = false;
    }
}