namespace UserManagementApi.Exceptions.Validation;

public class ValidationException(string message)
    : AppException(message, StatusCodes.Status400BadRequest)
{
}
