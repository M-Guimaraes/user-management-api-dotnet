using FluentValidation;

using UserManagementApi.DTOs;

namespace UserManagementApi.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .MaximumLength(50);
        
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(254);
    }
}
