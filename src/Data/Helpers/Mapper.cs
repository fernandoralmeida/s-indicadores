namespace IDN.Data.Helpers;

public class Mapper
{
    public static T Map<T>(object source)
    {
        var type = source.GetType();
        var properties = type.GetProperties();

        var instance = Activator.CreateInstance<T>();

        foreach (var property in properties)
        {
            var sourcePropertyValue = property.GetValue(source);
            var instanceProperty = instance?.GetType().GetProperty(property.Name);

            instanceProperty?.SetValue(instance, sourcePropertyValue);
        }

        return instance;
    }
}