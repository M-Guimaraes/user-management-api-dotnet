namespace UserManagementApi.Exceptions.Http;

public class UnauthorizedException(string message)
    : AppException(message, StatusCodes.Status401Unauthorized)
{
}
