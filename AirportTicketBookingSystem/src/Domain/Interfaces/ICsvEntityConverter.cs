namespace AirportTicketBookingSystem.Domain.Interfaces;

public interface ICsvEntityConverter<TEntity>
    where TEntity : IEntity
{
    /// <exception cref="FormatException">Thrown when unable to parse any of the csv fields.</exception>
    public TEntity CsvToEntity(string csvLine);

    public string EntityToCsv(TEntity entity);
}