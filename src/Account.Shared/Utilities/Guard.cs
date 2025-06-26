namespace Account.Shared.Utilities;
public static class Guard
{
    public static void Against<TException>(bool condition, string message)
        where TException : Exception, new()
    {
        if (condition)
        {
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
    }

    public static void AgainstNull<T>(T value, string parameterName) where T : class
    {
        if (value == null)
            throw new ArgumentNullException(parameterName);
    }

    public static void AgainstNullOrEmpty(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or empty", parameterName);
    }
}