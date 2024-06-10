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
//Guid agentId = Guid.NewGuid();
var client = host.Services.GetRequiredService<IClusterClient>();
ClientContext context = new(client);
await StartAsync(host);
context = context with
{
    UserName = AnsiConsole.Ask<string>("What is your [aqua]name[/]?")
};
Guid agentId = context.Client.GetGrain<IUser>(context.UserName).GetInfo().Result.primaryAgentId;
AnsiConsole.MarkupLine("{0}",agentId);
await ProcessLoopAsync(context);
await StopAsync(host);

//*******END************
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

async Task ProcessLoopAsync(ClientContext context)
{
    string? input = null;
    do
    {
        var fruit = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("[green]Main Menu[/]?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices(new[] {
            "Hello Agent","Hello User","Agent Info","Task","Reminder","Exit",
        }));
        //input = Console.ReadLine();
        /* if (string.IsNullOrWhiteSpace(input) && AnsiConsole.Confirm("Do you really want to exit?"))
        {
            break;
        } */
        if (fruit switch
        {
            "Hello Agent" => SayHelloAgent(context),
            "Hello User" => SayHelloUser(context),
            "Agent Info" => DisplayAgentInfo(context),
            "Task" => DisplayTaskInfo(context),
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

async Task<ClientContext> SayHelloAgent(ClientContext context)
{
    var zone = context.Client.GetGrain<IAgent>(agentId);
    string msg = await zone.SayHello(context.UserName);
    AnsiConsole.MarkupLine("[bold aqua]{0}[/]",msg);
    return context;

}
async Task<ClientContext> SayHelloUser(ClientContext context)
{
    var zone = context.Client.GetGrain<IUser>(context.UserName);
    UserData userData = await zone.GetInfo();

    AnsiConsole.MarkupLine("[bold aqua]{0}[/]",userData.primaryAgentId);
    return context;

}

async Task<ClientContext> TestReminder(ClientContext context)
{
    var zone = context.Client.GetGrain<ITask>(Guid.NewGuid());
    await zone.SetTaskInfo(agentId.ToString(),2);
    string msg = await zone.SayHello(context.UserName);
    AnsiConsole.MarkupLine("[bold aqua]{0}[/]",msg);
    await zone.StartTask();
    return context;

}

async Task<ClientContext> DisplayAgentInfo(ClientContext context)
{
    AgentData agentData = await context.Client.GetGrain<IAgent>(agentId).GetInfo();
    AnsiConsole.MarkupLine("[bold aqua]{0}:[/] [green]{1}[/]",agentData.name,agentData.xp);
    return context;
}
async Task<ClientContext> DisplayTaskInfo(ClientContext context)
{
    IZone zone = context.Client.GetGrain<IZone>("0.0.0"); //TODO use zone located in
    var taskIds = zone.GetTasks().Result.Select(g => g.ToString());
    var taskSelect = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("[green]Which task to start?[/]?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices(taskIds));
    ITask task = context.Client.GetGrain<ITask>(Guid.Parse(taskSelect));
    var agent = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("[green]Which agent to send?[/]?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices(new[] {agentId.ToString()
        }));
    await task.SetTaskInfo(agent,2);
    await task.StartTask();
    AnsiConsole.MarkupLine("[bold aqua]{0}:[/] [green]{1}[/]",taskSelect,agent);
    
    return context;
}