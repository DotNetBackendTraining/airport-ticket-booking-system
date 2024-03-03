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
        var flightClass = FlightClassParser.ParseFullFlightClassOrThrow(parts[2]);
        return Booking.Create(flightId, passengerId, flightClass);
    }

    public string EntityToCsv(Booking entity)
    {
        const string prefix = CsvConstants.FlightClassPrefix;
        return $"{entity.FlightId},{entity.PassengerId},{prefix}{entity.BookingClass}";
    }
}