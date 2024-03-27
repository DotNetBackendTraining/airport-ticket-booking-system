using AirportTicketBookingSystem.Application.Result;

namespace AirportTicketBookingSystem.Presentation.Utility;

public static class Display
{
    public static void OperationResult<TItem>(OperationResult<TItem> operationResult,
        string headerMessage = "Operation Result") where TItem : notnull
    {
        var borderLine = new string('-', 40);
        var innerBorderLine = new string('-', 38);

        Console.WriteLine($"\n{borderLine}");
        Console.WriteLine($"| {headerMessage,-38} |");
        Console.WriteLine($"{borderLine}");

        Console.WriteLine($"| Success: {operationResult.Success.ToString(),-31} |");

        Console.WriteLine($"| {innerBorderLine} |");
        Console.WriteLine($"| Message: {operationResult.Message,-30} |");

        Console.WriteLine($"| {innerBorderLine} |");
        var itemDisplay = operationResult.Item?.ToString() ?? "None";
        Console.WriteLine($"| Item: {itemDisplay,-34} |");

        Console.WriteLine($"{borderLine}\n");
    }

    public static void SearchResult<TItem>(SearchResult<TItem> searchResult, string headerMessage = "Search Result")
    {
        var borderLine = new string('-', 40);
        var innerBorderLine = new string('-', 38);

        Console.WriteLine($"\n{borderLine}");
        Console.WriteLine($"| {headerMessage,-38} |");
        Console.WriteLine($"{borderLine}");

        Console.WriteLine($"| Success: {searchResult.Success.ToString(),-31} |");

        Console.WriteLine($"| {innerBorderLine} |");
        Console.WriteLine($"| Message: {searchResult.Message,-30} |");

        Console.WriteLine($"| {innerBorderLine} |");
        if (searchResult.Items.Any())
            foreach (var item in searchResult.Items)
                Console.WriteLine($"| Item: {item?.ToString()?.PadRight(34)} |");
        else
            Console.WriteLine("| Items: None available.                 |");

        Console.WriteLine($"{borderLine}\n");
    }

    public static void BatchOperationResults<TItem>(IEnumerable<OperationResult<TItem>> operationResults,
        string headerMessage = "Batch Operation Results")
        where TItem : notnull
    {
        Console.WriteLine($"\n--- {headerMessage} ---\n");
        var list = operationResults.ToList();
        if (list.Count == 0)
        {
            Console.WriteLine("No operation results to display.");
            return;
        }

        var count = 1;
        foreach (var operationResult in list)
        {
            Console.WriteLine($"Result {count}:");
            OperationResult(operationResult);
            count++;
        }
    }
}