namespace DrinksInfo.Domain.Validation;

public static class Errors
{
    public static readonly Error None = Error.None;
    public static readonly Error GenericNull = new("GenericNull", "A null value has been returned for this query.");
    public static readonly Error EmptyResponse = new("EmptyResponse", "The DrinkInfo API returned no data.");
    public static readonly Error NetworkError = new("NetworkError", "Could not connect to API.");
    public static readonly Error Timeout = new("Timeout", "The request to the API timed out.");
    public static readonly Error InvalidJson = new("InvalidJson", "The API response contained invalid JSON.");
}
