using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Common;

public class PaginationQuery
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
    
    public int Skip => (Page - 1) * PageSize;
}