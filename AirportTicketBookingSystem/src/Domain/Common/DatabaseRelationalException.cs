namespace AirportTicketBookingSystem.Domain.Common;

/// <summary>
/// Represents errors that occur due to relation constrains violations.
/// </summary>
public class DatabaseRelationalException : DatabaseException
{
    public DatabaseRelationalException()
    {
    }

    public DatabaseRelationalException(string? message) : base(message)
    {
    }

    public DatabaseRelationalException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}