using ExevopanNotification.Domain.Enums;
using System.Text.Json.Serialization;

namespace ExevopanNotification.Domain.Entities
{
    public class PvpType
    {
        [JsonPropertyName("string")]
        public string Description { get; set; }
        public PvpEnum Type { get; set; }
    }
}
