namespace UserManagementApi.Exceptions.Http;

public class BadRequestException(string message)
    : AppException(message, StatusCodes.Status400BadRequest)
{
}
