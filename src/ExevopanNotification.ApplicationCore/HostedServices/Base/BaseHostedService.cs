namespace ExevopanNotification.ApplicationCore.HostedServices.Base
{
    public class BaseHostedService
    {
        internal Timer _timer;

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // New Timer does not have a stop. 
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
