using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Infrastructure.Converter.Utility;

namespace AirportTicketBookingSystem.Infrastructure.Converter;

public class PassengerConverter : ICsvEntityConverter<Passenger>
{
    public Passenger CsvToEntity(string csvLine)
    {
        var id = Parser.ParseOrThrowInt(csvLine);
        return new Passenger(id);
    }

    public string EntityToCsv(Passenger passenger) => passenger.Id.ToString();
}