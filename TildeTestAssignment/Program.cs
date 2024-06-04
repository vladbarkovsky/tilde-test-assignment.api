
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Display;

namespace TildeTestAssignment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((context, configuration) =>
                {
                    configuration
                        .WriteTo.File(
                            restrictedToMinimumLevel: LogEventLevel.Warning,
                            path: "Logs/.log",
                            formatter: new MessageTemplateTextFormatter("[{Timestamp:HH:mm:ss.fff} {Level:u3}] {SourceContext}: {Message}{NewLine}{Exception}"),
                            rollingInterval: RollingInterval.Day,
                            retainedFileCountLimit: 7)
                        .WriteTo.Console()
                        .Enrich.FromLogContext();
                })
                .Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            // TODO: Seeding.

            await host.RunAsync();
        }
    }
}
