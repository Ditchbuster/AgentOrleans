using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

await Host.CreateDefaultBuilder(args)
    .UseOrleans(siloBuilder =>
    {
        siloBuilder
            .UseLocalhostClustering()
            .AddMemoryGrainStorage("Store")
            .UseInMemoryReminderService()
            .UseDashboard();
            //.AddMemoryStreams("chat");
    })
    .RunConsoleAsync();
