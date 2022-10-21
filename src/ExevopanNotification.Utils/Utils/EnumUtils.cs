namespace ExevopanNotification.Utils.Utils
{
    public static class EnumUtils
    {
        public static string GetEnumName<T>(T data) where T : Enum
        {
            return Enum.GetName(typeof(T), data)!;
        }
    }
}
