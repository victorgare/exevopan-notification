namespace ExevopanNotification.ApplicationCore.Interfaces.Services
{
    public interface IRuleBreakerService
    {
        Task FindAndNotify();
    }
}
