namespace DrinksInfo.Domain.Validation;

public static class Errors
{
    public static readonly Error None = Error.None;
    public static readonly Error GenericNull = new("GenericNull", "A null value has been returned for this query.");

    public static readonly Error EmptyApiResponse = new("EmptyApiResponse", "The DrinkInfo API returned no data.");
    public static readonly Error NetworkError = new("NetworkError", "Could not connect to API.");
    public static readonly Error Timeout = new("Timeout", "The request to the API timed out.");
    public static readonly Error InvalidJson = new("InvalidJson", "The API response contained invalid JSON.");

    public static readonly Error EmptyDbResponse = new("EmptyDbResponse", "The database returned no records.");
    public static readonly Error NoRecordById = new("NoRecordById", "No record matching that Id exists.");
    public static readonly Error AddFailed = new("AddFailed", "Failed to add new record.");
    public static readonly Error DeleteFailed = new("DeleteFailed", "Failed to delete record.");
    public static readonly Error FavoriteExists = new("FavoriteExists", "Selected drink already exists in the favorites list.");
    public static readonly Error FavoriteDoesNotExist = new("FavoriteDoesNotExist", "Selected drink does not exist in the favorites list.");

}
