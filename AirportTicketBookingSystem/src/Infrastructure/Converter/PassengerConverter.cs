using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Converter.Utility;

namespace AirportTicketBookingSystem.Infrastructure.Converter;

public class PassengerConverter : ICsvEntityConverter<Passenger>
{
    public Passenger CsvToEntity(string csvLine)
    {
        var id = csvLine.ParseOrThrowInt();
        return new Passenger(id);
    }

    public string EntityToCsv(Passenger passenger) => passenger.Id.ToString();
}