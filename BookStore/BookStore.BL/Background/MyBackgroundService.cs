using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Background
{
    public class MyBackgroundService : IHostedService
    {
        private readonly ILogger<MyBackgroundService> _logger; 
        private int executionCount = 0;

        public MyBackgroundService(ILogger<MyBackgroundService> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Neshto si start......................");
            await DoWork(cancellationToken);
        }
        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation(
                    "Scoped Processing Service is working. Count: {Count}", executionCount);

                await Task.Delay(500, stoppingToken);
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Neshto si stop......................");
            return Task.CompletedTask;
        }
    }
}
