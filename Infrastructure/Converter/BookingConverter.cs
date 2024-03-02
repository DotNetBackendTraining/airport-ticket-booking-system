using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Infrastructure.Utility;

namespace AirportTicketBookingSystem.Infrastructure.Converter;

public class BookingConverter : ICsvEntityConverter<Booking>
{
    public Booking CsvToEntity(string csvLine)
    {
        var parts = Parser.SplitToLengthOrThrow(csvLine, 3);
        var flightId = Parser.ParseOrThrowInt(parts[0]);
        var passengerId = Parser.ParseOrThrowInt(parts[1]);
        var flightClass = FlightClassConversionHelper.ParseFullFlightClassOrThrow(parts[2]);
        return new Booking(flightId, passengerId, flightClass);
    }

    public string EntityToCsv(Booking entity)
    {
        const string prefix = CsvConstants.FlightClassPrefix;
        return $"{entity.FlightId},{entity.PassengerId},{prefix}{entity.BookingClass}";
    }
}