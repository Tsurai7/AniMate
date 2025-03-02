namespace AniMate_app.Utils
{
    public static class LinQExtensions
    {
        public static IEnumerable<T> Map<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (T value in values)
                action?.Invoke(value);

            return values;
        }

        public static IEnumerable<V> Map<T, V>(this IEnumerable<T> values, Func<T, V> converter)
        {
            int valueCount = values.Count();

            V[] newValues = new V[valueCount];

            for(int i = 0; i < valueCount; i++)
                newValues[i] = converter.Invoke(values.ElementAt(i));

            return newValues;
        }
    }
}
