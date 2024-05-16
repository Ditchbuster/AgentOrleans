﻿using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

await Host.CreateDefaultBuilder(args)
    .UseOrleans(siloBuilder =>
    {
        siloBuilder
            .UseLocalhostClustering()
            .AddMemoryGrainStorage("PubSubStore")
            .UseInMemoryReminderService();
            //.AddMemoryStreams("chat");
    })
    .RunConsoleAsync();
