using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Infrastructure.Converter.Utility;

public static class FlightClassParser
{
    private static string RemoveFlightClassPrefixOrThrow(string flightClassStr)
    {
        const string prefix = CsvConstants.FlightClassPrefix;
        if (!flightClassStr.StartsWith(prefix))
            throw new FormatException($"The string '{flightClassStr}' is expected to start with '{prefix}'");
        return flightClassStr[prefix.Length..];
    }

    private static FlightClass ParseFlightClassOrThrow(string flightClassStr)
    {
        if (!Enum.TryParse<FlightClass>(flightClassStr, true, out var flightClass))
            throw new FormatException($"Unable to determine flight class of '{flightClassStr}'");
        return flightClass;
    }

    public static FlightClass ParseFullFlightClassOrThrow(string flightClassStr)
    {
        flightClassStr = RemoveFlightClassPrefixOrThrow(flightClassStr);
        return ParseFlightClassOrThrow(flightClassStr);
    }

    public static (FlightClass, decimal) ParseFullFlightClassAndPriceOrThrow(string flightClassStr)
    {
        flightClassStr = RemoveFlightClassPrefixOrThrow(flightClassStr);
        var parts = Parser.SplitToLengthOrThrow(
            flightClassStr, 2, CsvConstants.FlightClassPriceSplitterChar);
        var flightClass = ParseFlightClassOrThrow(parts[0]);
        var price = Parser.ParseOrThrowDecimal(parts[1]);
        return (flightClass, price);
    }
}