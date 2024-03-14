using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Config;
using Microsoft.Extensions.DependencyInjection;

namespace ExevopanNotification.ApplicationCore.CronJob
{
    public class RuleBreakerNotificationCronJob : CronJobService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RuleBreakerNotificationCronJob(IScheduleConfig<RuleBreakerNotificationCronJob> config, IServiceScopeFactory serviceScopeFactory) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var ruleBreakerService = scope.ServiceProvider.GetService<IRuleBreakerService>()!;

            await ruleBreakerService.FindAndNotify();
        }

    }
}
