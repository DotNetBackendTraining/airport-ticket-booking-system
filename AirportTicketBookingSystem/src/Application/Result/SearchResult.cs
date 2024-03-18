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
    /// <summary>
    /// Retrieves a specific page of items from the search results.
    /// </summary>
    /// <param name="pageNumber">The one-based page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A subset of items corresponding to the specified page.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if pageNumber or pageSize are less than 1.</exception>
    public IEnumerable<TItem> GetPage(int pageNumber, int pageSize)
    {
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber),
                "Page number should be greater than or equal to 1.");

        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size should be greater than or equal to 1.");

        return Items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}