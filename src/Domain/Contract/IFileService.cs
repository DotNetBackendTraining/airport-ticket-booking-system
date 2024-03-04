namespace AirportTicketBookingSystem.Domain.Contract;

public interface IFileService<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Reads all entities from file.
    /// </summary>
    /// <returns>A collection of all entities read from the file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file is not found.</exception>
    public IEnumerable<TEntity> ReadAll();

    /// <summary>
    /// Writes all entities to the file, overwriting any existing content.
    /// </summary>
    /// <param name="entities">The collection of entities to write to the file.</param>
    /// <returns>A task representing the asynchronous write operation.</returns>
    public Task WriteAllAsync(IEnumerable<TEntity> entities);
}