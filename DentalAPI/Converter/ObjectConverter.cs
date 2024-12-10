using System.Reflection;

public static class ObjectConverter
{
    public static TTarget ConvertTo<TSource, TTarget>(TSource source) where TTarget : TSource, new()
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        // Create an instance of the target type
        TTarget target = new TTarget();

        // Get properties of the source object type
        var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Copy properties from source to target
        foreach (var property in sourceProperties)
        {
            if (property.CanRead && property.CanWrite)
            {
                var value = property.GetValue(source);
                property.SetValue(target, value);
            }
        }

        // Leave additional properties of the target as null
        return target;
    }
}
