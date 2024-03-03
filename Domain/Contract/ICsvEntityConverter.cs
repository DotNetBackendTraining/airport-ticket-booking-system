using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Domain.Contract;

public interface ICsvEntityConverter<T>
{
    /// <exception cref="FormatException">Thrown when unable to parse any of the csv fields.</exception>
    /// <exception cref="ValidationException">Thrown when any of the parsed fields do not satisfy validation attributes.</exception>
    public T CsvToEntity(string csvLine);

    public string EntityToCsv(T entity);
}