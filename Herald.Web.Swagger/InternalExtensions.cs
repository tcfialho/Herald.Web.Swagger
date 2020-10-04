namespace Herald.Web.Swagger
{
    internal static class InternalExtensions
    {
        public static void CopyTo<T>(this T from, T to) where T : class
        {
            foreach (var toPropInfo in typeof(T).GetProperties())
            {
                var fromValue = toPropInfo.GetValue(from);
                toPropInfo.SetValue(to, fromValue, null);
            }
        }
    }
}
