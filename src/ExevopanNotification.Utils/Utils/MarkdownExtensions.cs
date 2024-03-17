namespace ExevopanNotification.Utils.Utils
{
    public static class MarkdownExtensions
    {
        public static string ToBold(this string value) => $"*{value}*";
        public static string ToItalic(this string value) => $"_{value}_";

    }
}
