namespace UserManagementApi.Exceptions.Http;

public class ConflictException(string message)
    : AppException(message, StatusCodes.Status409Conflict)
{
}
