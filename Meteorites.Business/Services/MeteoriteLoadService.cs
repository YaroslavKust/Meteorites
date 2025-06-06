using Meteorites.Business.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Meteorites.Business.Services
{
    public class MeteoriteLoadService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var meteoriteService = scope.ServiceProvider.GetRequiredService<IMeteoriteService>();

                    try
                    {
                        await meteoriteService.UpdateMeteorites();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Background Service Error: {ex.Message}");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(BackgoundServiceConstants.TimeoutMinutes), stoppingToken);
            }
        }
    }
}
