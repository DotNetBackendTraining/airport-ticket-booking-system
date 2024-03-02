namespace AirportTicketBookingSystem.Domain.Contract;

public interface ICsvEntityConverter<T>
{
    public T CsvToEntity(string csvLine);
    public string EntityToCsv(T entity);
}