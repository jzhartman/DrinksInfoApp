namespace DrinksInfo.Domain.Validation;

public static class Errors
{
    public static readonly Error None = Error.None;
    public static readonly Error MyErrorName = new("ErrorId", "ErrorDescription");
}
