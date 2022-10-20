using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ExevopanNotification.Utils.Utils
{
    public static class ParseUtils
    {
        public static string ToJson<T>(this T data)
        {
            return JsonSerializer.Serialize(data);
        }

        public static StringContent ToStringContent<T>(this T data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, Application.Json);
        }
    }
}
