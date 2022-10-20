namespace ExevopanNotification.Domain.Entities
{
    public class ServerData
    {
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public ServerLocation ServerLocation { get; set; }
        public PvpType PvpType { get; set; }
        public bool Battleye { get; set; }
        public bool Experimental { get; set; }
    }
}
