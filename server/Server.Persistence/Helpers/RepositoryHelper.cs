namespace Server.Persistence.Helpers;

public class RepositoryHelper
{
    public static string GetTableName<T>()
    {
        var entityType = typeof(T);
        return entityType.Name + "s";
    }

    public static IEnumerable<string> GetPropertyNames<T>()
    {
        return typeof(T).GetProperties().Select(p => p.Name);
    }
}
