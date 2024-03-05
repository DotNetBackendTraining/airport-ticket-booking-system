namespace AirportTicketBookingSystem.Presentation.Utility;

public static class Display
{
    public static void Items<T>(IEnumerable<T> items, string headerMessage = "--- Items ---")
        where T : notnull
    {
        Console.WriteLine($"\n{headerMessage}");
        var list = items.ToList();
        if (list.Count == 0) Console.WriteLine("No items found.");
        else list.ForEach(i => Console.WriteLine(i));
    }
}