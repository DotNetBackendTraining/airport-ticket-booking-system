using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Domain.Contract;

public interface ICsvEntityConverter<TEntity>
    where TEntity : IEntity
{
    /// <exception cref="FormatException">Thrown when unable to parse any of the csv fields.</exception>
    /// <exception cref="ValidationException">Thrown when any of the parsed fields do not satisfy validation attributes.</exception>
    
    // use CsvHelper to read and write csv files 
    public TEntity CsvToEntity(string csvLine);

    public string EntityToCsv(TEntity entity);
}