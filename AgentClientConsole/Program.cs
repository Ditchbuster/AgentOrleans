using Spectre.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Runtime;

// See https://aka.ms/new-console-template for more information
 AnsiConsole.Markup("[underline red]Hello[/] World!\n");

 using var host = new HostBuilder()
    .UseOrleansClient(clientBuilder =>
    {
        clientBuilder.UseLocalhostClustering();
            //.AddMemoryStreams("chat");
    })
    .Build();

var client = host.Services.GetRequiredService<IClusterClient>();

ClientContext context = new(client);
await StartAsync(host);
context = context with
{
    UserName = AnsiConsole.Ask<string>("What is your [aqua]name[/]?")
};
await ProcessLoopAsync(context);
await StopAsync(host);


static Task StartAsync(IHost host) =>
    AnsiConsole.Status().StartAsync("Connecting to server", async ctx =>
    {
        ctx.Spinner(Spinner.Known.Dots);
        ctx.Status = "Connecting...";

        await host.StartAsync();

        ctx.Status = "Connected!";
    });
static Task StopAsync(IHost host) =>
    AnsiConsole.Status().StartAsync("Disconnecting...", async ctx =>
    {
        ctx.Spinner(Spinner.Known.Dots);
        await host.StopAsync();
    });

static async Task ProcessLoopAsync(ClientContext context)
{
    string? input = null;
    do
    {
        input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            continue;
        }
        else if (AnsiConsole.Confirm("Do you really want to exit?"))
        {
            break;
        }


    } while (input is not "/exit");
}
