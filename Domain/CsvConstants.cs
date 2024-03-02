namespace AirportTicketBookingSystem.Domain;

public static class CsvConstants
{
    // Standard date and time format for flight departure times in CSV files
    public const string FlightDepartureDateTimeFormat = "yyyy-MM-dd HH:mm";

    // All airport names should be quoted with it in CSV files
    public const char AirportNameQuoteChar = '`';

    // All flight class fields should be prefixed with this string
    public const string FlightClassPrefix = "Class:";

    // All flight class - price pairs should be combined with it in CSV files
    public const char FlightClassPriceSplitterChar = ':';
}