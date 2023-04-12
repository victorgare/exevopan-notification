namespace ExevopanNotification.Domain.Entities
{
    public class FilterLimits
    {
        public FilterLimits(int minLevel, int maxLevel)
        {
            MinLevel = minLevel;
            MaxLevel = maxLevel;
        }

        public int MinLevel { get; private set; }
        public int MaxLevel { get; private set; }
    }
}
