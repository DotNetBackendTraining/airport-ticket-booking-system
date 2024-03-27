namespace AirportTicketBookingSystem.Application.Result;

/// <summary>
/// Represents the result of a search operation, containing a collection of items of type <typeparamref name="TItem"/>.
/// </summary>
/// <typeparam name="TItem">The type of items in the search results.</typeparam>
/// <param name="Success">Indicates whether the search operation was successful.</param>
/// <param name="Message">Provides a message related to the search outcome.</param>
/// <param name="Items">The collection of items resulting from the search.</param>
/// <remarks>
/// The success of an OperationResult does not indicate anything about the item collection, it might be empty.
/// </remarks>
public record SearchResult<TItem>(bool Success, string Message, IEnumerable<TItem> Items)
    : Result(Success, Message)
{
}