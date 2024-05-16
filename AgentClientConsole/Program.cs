using Spectre.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Runtime;
using AgentGrainInterfaces;

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
        var fruit = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("What's your [green]favorite fruit[/]?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
        .AddChoices(new[] {
            "Exit","Hello","Reminder",
        }));
        //input = Console.ReadLine();
        /* if (string.IsNullOrWhiteSpace(input) && AnsiConsole.Confirm("Do you really want to exit?"))
        {
            break;
        } */
        if (fruit switch
        {
            "Hello" => SayHello(context),
            //"/l" => LeaveChannel(context),
            "Reminder" => TestReminder(context),
            _ => null
        } is Task<ClientContext> cxtTask)
        {
            context = await cxtTask;
            continue;
        }
        
        if (fruit == "Exit"&& AnsiConsole.Confirm("Do you really want to exit?")){
            break;
        }

    } while (input is not "/exit");
}

static async Task<ClientContext> SayHello(ClientContext context)
{
    var zone = context.Client.GetGrain<IZone>("0");
    string msg = await zone.SayHello(context.UserName);
    AnsiConsole.MarkupLine("[bold aqua]{0}[/]",msg);
    return context;

}

static async Task<ClientContext> TestReminder(ClientContext context)
{
    var zone = context.Client.GetGrain<ITask>(Guid.NewGuid());
    string msg = await zone.SayHello(context.UserName);
    AnsiConsole.MarkupLine("[bold aqua]{0}[/]",msg);
    await zone.StartTask("0");
    return context;

}