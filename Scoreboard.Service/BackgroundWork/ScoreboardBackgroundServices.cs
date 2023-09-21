using FluentAssertions.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Scoreboard.Service.Canvas.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Service.BackgroundWork
{
    public class ScoreboardBackgroundServices : BackgroundService
    {
        private readonly ILogger<ScoreboardBackgroundServices> _logger;

        public ScoreboardBackgroundServices(ILogger<ScoreboardBackgroundServices> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }
        public IServiceProvider Services { get; }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service running.");

            await Seed(stoppingToken);
        }

        private async Task Seed(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IStudentAppServices>();
                await scopedProcessingService.SeedData(stoppingToken);
            }
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
