using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Infrastructure.Converter.Utility;

namespace AirportTicketBookingSystem.Infrastructure.Converter;

public class BookingConverter : ICsvEntityConverter<Booking>
{
    public Booking CsvToEntity(string csvLine)
    {
        var parts = Parser.SplitToLengthOrThrow(csvLine, 3);
        var flightId = Parser.ParseOrThrowInt(parts[0]);
        var passengerId = Parser.ParseOrThrowInt(parts[1]);
        var flightClass = Parser.ParseFlightClassOrThrow(parts[2]);
        return Booking.Create(flightId, passengerId, flightClass);
    }

    public string EntityToCsv(Booking entity) =>
        $"{entity.FlightId},{entity.PassengerId},{entity.BookingClass}";
}