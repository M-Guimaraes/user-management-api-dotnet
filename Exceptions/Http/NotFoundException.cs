namespace UserManagementApi.Exceptions.Http;

public class NotFoundException(string message)
    : AppException(message, StatusCodes.Status404NotFound)
{
}