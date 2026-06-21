namespace UserManagementApi.Exceptions.Http;

public class ForbiddenException(string message)
    : AppException(message, StatusCodes.Status403Forbidden)
{
}
