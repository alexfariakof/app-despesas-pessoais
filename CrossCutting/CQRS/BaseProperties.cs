using System.Reflection;

namespace CrossCutting.CQRS;
public abstract class BaseProperties<T> where T : class, new()
{
    protected BaseProperties(T entity)
    {
        CopyProperties(entity, this);
    }

    private void CopyProperties(T source, object destination)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(source);
            PropertyInfo destProperty = destination.GetType().GetProperty(property.Name);
            if (destProperty != null && destProperty.CanWrite)
            {
                destProperty.SetValue(destination, value);
            }
        }
    }
}
