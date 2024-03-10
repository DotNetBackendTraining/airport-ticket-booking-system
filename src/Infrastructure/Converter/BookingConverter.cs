using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Infrastructure.Converter.Utility;

namespace AirportTicketBookingSystem.Infrastructure.Converter;

public class BookingConverter : ICsvEntityConverter<Booking>
{
    // using csvHelper is a good idea
    // it will make your life easier
    // and you will not have to write your own csv parser
    public Booking CsvToEntity(string csvLine)
    {
        var parts = csvLine.SplitToLengthOrThrow(3);
        var flightId = parts[0].ParseOrThrowInt();
        var passengerId = parts[1].ParseOrThrowInt();
        var flightClass = parts[2].ParseFlightClassOrThrow();
        return Booking.Create(flightId, passengerId, flightClass);
    }

    public string EntityToCsv(Booking entity) =>
        $"{entity.FlightId},{entity.PassengerId},{entity.BookingClass}";
}