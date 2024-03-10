using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Infrastructure.Converter.Utility;

namespace AirportTicketBookingSystem.Infrastructure.Converter;

public class PassengerConverter : ICsvEntityConverter<Passenger>
{
    // using csvHelper is a good idea
    // it will make your life easier
    // and you will not have to write your own csv parser
    public Passenger CsvToEntity(string csvLine)
    {
        var id = csvLine.ParseOrThrowInt();
        return Passenger.Create(id);
    }

    public string EntityToCsv(Passenger passenger) => passenger.Id.ToString();
}