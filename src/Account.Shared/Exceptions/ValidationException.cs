namespace Account.Shared.Exceptions;
public class ValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException(Dictionary<string, string[]> errors)
        : base("One or more validation failures have occurred.")
    {
        Errors = errors;
    }

    public ValidationException(string property, string error)
        : this(new Dictionary<string, string[]> { { property, new[] { error } } })
    {
    }
}