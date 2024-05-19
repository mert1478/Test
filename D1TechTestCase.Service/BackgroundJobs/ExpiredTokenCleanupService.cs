using D1TechTestCase.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.BackgroundJobs
{
    public class ExpiredTokenCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ExpiredTokenCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                using var scope = _serviceProvider.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var currentTime = DateTime.Now;

                var tokensToRemove = await context.UserSessions
                    .Where(token => token.Expiration < currentTime).ToListAsync(stoppingToken);
                if (tokensToRemove.Any())
                {
                    context.UserSessions.RemoveRange(tokensToRemove);
                    await context.SaveChangesAsync(stoppingToken);
                }

            }
        }
    }
}
