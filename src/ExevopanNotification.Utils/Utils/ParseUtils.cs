using System.Collections;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace ExevopanNotification.Utils.Utils
{
    public static class ParseUtils
    {
        public static string ToJson<T>(this T data)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(data, options);
        }

        public static T? ParseJson<T>(this string data)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(data, options);
        }

        public static StringContent ToStringContent<T>(this T data)
        {
            return new StringContent(data.ToJson(), Encoding.UTF8, Application.Json);
        }

        public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static long ToUnixTimeSeconds(this DateTime date)
        {
            return ((DateTimeOffset)date).ToUnixTimeSeconds();
        }

        public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }

                if (valueElemType.IsEnum)
                {
                    var enumerable = properties[key] as IEnumerable;
                    var enumValues = enumerable.OfType<Enum>().ToList().Select(x => x.GetHashCode());
                    properties[key] = string.Join(separator, enumValues);
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x =>
                {
                    var value = x.Value?.ToString();
                    switch (Type.GetTypeCode(x.Value?.GetType()))
                    {
                        case TypeCode.Boolean:
                            value = value?.ToLower();
                            break;

                    }

                    return string.Concat(Uri.EscapeDataString(JsonNamingPolicy.CamelCase.ConvertName(x.Key)), "=", Uri.EscapeDataString(value!));
                }));
        }
    }
}
