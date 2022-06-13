using IO.TedTalk.Api;
using IO.TedTalk.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Elasticsearch;

public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        Console.WriteLine($"Migrating database started...");

        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IODbContext>();
            db.Database.Migrate();
        }

        Console.WriteLine($"Migrating database finished.!");

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                .ConfigureLogging((_, logging) => { logging.ClearProviders(); })
                        .UseSerilog((ctx, cfg) =>
                        {

                            cfg.ReadFrom.Configuration(ctx.Configuration)
                                .Enrich.FromLogContext()
                                .Enrich.WithProperty("service_name", nameof(IO.TedTalk.Api));


                            if (ctx.HostingEnvironment.IsDevelopment())
                            {
                                cfg.WriteTo.Async(sinkCfg => sinkCfg.Console());
                            }
                            else
                            {
                                cfg.WriteTo.Async(sinkCfg => sinkCfg.Console(new ElasticsearchJsonFormatter()));
                            }
                        });


            }
                );
}