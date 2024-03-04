namespace AirportTicketBookingSystem.Application.Result;

/// <summary>
/// Represents the result of an operation that deals with a single item of type <typeparamref name="TItem"/>.
/// </summary>
/// <typeparam name="TItem">The type of the item processed by the operation.</typeparam>
/// <param name="Success">Indicates whether the operation was successful.</param>
/// <param name="Message">Provides a message related to the operation outcome.</param>
/// <param name="Item">The item produced by the operation, if any.</param>
/// <remarks>
/// The success of an OperationResult does not indicate anything about the item, it might be empty.
/// </remarks>
public record OperationResult<TItem>(bool Success, string Message, TItem? Item)
    : Result(Success, Message) where TItem : notnull;