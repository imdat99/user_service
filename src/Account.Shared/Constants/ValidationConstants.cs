namespace Account.Shared.Constants;
public static class ValidationConstants
{
    public static class Email
    {
        public const int MaxLength = 255;
        public const string Pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    }

    public static class Name
    {
        public const int MaxLength = 100;
        public const int MinLength = 2;
    }

    public static class Password
    {
        public const int MinLength = 8;
        public const int MaxLength = 128;
    }
}