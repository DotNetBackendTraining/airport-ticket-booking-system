using AirportTicketBookingSystem.Domain.Contract;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class CsvFileService<TEntity> : IFileService<TEntity>, IDisposable
{
    public CsvFileService(string filepath,
        ICsvEntityConverter<TEntity> csvEntityConverter)
    {
        Filepath = filepath;
        Converter = csvEntityConverter;
        Cache = LoadData();
    }

    private string Filepath { get; }
    private ICsvEntityConverter<TEntity> Converter { get; }
    private List<TEntity> Cache { get; }
    private bool CacheIsDirty { get; set; }

    private List<TEntity> LoadData()
    {
        if (!File.Exists(Filepath))
            throw new FileNotFoundException($"The file {Filepath} was not found.");

        return File.ReadLines(Filepath)
            .Skip(1) // Skip header
            .Select(line => Converter.CsvToEntity(line))
            .ToList();
    }

    public IEnumerable<TEntity> ReadAll() => Cache;

    public Task AppendAsync(IEnumerable<TEntity> entities)
    {
        Cache.AddRange(entities);
        CacheIsDirty = true;
        return Task.CompletedTask;
    }

    public void Append(TEntity entity)
    {
        Cache.Add(entity);
        CacheIsDirty = true;
    }

    public bool Replace(TEntity oldEntity, TEntity newEntity)
    {
        var i = Cache.IndexOf(oldEntity);
        if (i == -1) return false;
        Cache[i] = newEntity;
        return CacheIsDirty = true;
    }

    public bool Remove(TEntity entity)
    {
        if (Cache.Remove(entity))
            return CacheIsDirty = true;
        return false;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing && CacheIsDirty)
            SaveChangesAsync().Wait();
    }

    private async Task SaveChangesAsync()
    {
        var csvLines = Cache.Select(entity => Converter.EntityToCsv(entity));
        await File.WriteAllLinesAsync(Filepath, csvLines);
        CacheIsDirty = false;
    }
}