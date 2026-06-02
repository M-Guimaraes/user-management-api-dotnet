using UserManagementApi.Common;

namespace UserManagementApi.DTOs;

public class UserQueryDto : PaginationQuery
    {
        public string? Name { get; set; }

        public string? Email { get; set; }
}
