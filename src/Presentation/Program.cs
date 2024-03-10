﻿using AirportTicketBookingSystem.Presentation;
using AirportTicketBookingSystem.Presentation.Controller;
using AirportTicketBookingSystem.Presentation.Utility;

LoadEnvVariables();

if (PromptHelper.PromptYesNo("Do You Want to Enter Manager Mode (y/n):  "))
{
    var provider = DependencyInjector.CreateManagerServiceProvider();
    new ManagerController(provider).Start();
}
else
{
    var provider = DependencyInjector.CreateClientServiceProvider();
    new ClientController(provider).Start();
}

return;

void LoadEnvVariables()
{
    // try to use dotenv built-in package
    // it will help you to handle .env file
    
    var current = new DirectoryInfo(Directory.GetCurrentDirectory());
    while (current != null && current.GetFiles("*.csproj").Length == 0)
        current = current.Parent;

    if (current == null)
    {
        Console.WriteLine("Project root could not be found.");
        return;
    }

    var filepath = Path.Combine(current.FullName, ".env");
    Console.WriteLine($"Looking for .env at: {filepath}");

    if (!File.Exists(filepath))
    {
        Console.WriteLine(".env file not found.");
        return;
    }

    foreach (var line in File.ReadAllLines(filepath))
    {
        var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2) continue;
        Environment.SetEnvironmentVariable(parts[0], parts[1]);
    }
}