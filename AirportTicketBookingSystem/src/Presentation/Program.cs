using AirportTicketBookingSystem.Presentation;
using AirportTicketBookingSystem.Presentation.Controller;
using AirportTicketBookingSystem.Presentation.Utility;
using dotenv.net;
using Microsoft.Extensions.DependencyInjection;

DotEnv.Load();

if (PromptHelper.PromptYesNo("Do You Want to Enter Manager Mode (y/n):  "))
{
    var provider = DependencyInjector.CreateManagerServiceProvider();
    provider.GetRequiredService<ManagerController>().Start();
}
else
{
    var provider = DependencyInjector.CreateClientServiceProvider();
    provider.GetRequiredService<ClientController>().Start();
}