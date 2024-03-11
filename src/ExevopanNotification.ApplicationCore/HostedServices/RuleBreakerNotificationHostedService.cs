using ExevopanNotification.ApplicationCore.HostedServices.Base;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExevopanNotification.ApplicationCore.HostedServices
{
    public class RuleBreakerNotificationHostedService : BaseHostedService, IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RuleBreakerNotificationHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ExecuteProcess, null, TimeSpan.Zero, GetInterval());

            return Task.CompletedTask;
        }

        public async void ExecuteProcess(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var ruleBreakerService = scope.ServiceProvider.GetService<IRuleBreakerService>()!;

                await ruleBreakerService.FindAndNotify();
            }
        }

        private static TimeSpan GetInterval()
        {
            return TimeSpan.FromHours(12);
        }
    }
}
