namespace ExevopanNotification.Domain.Config
{
    public class TelegramConfig
    {
        public string Key { get; set; } = null!;
        public string GroupId { get; set; } = null!;
        public string RuleBreakerGroupId { get; set; } = null!;
    }
}
