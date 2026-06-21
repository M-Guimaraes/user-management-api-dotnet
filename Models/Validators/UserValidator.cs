using FluentValidation;

namespace UserManagementApi.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage( "Nome é obrigatório")
            .MinimumLength(3)
            .MaximumLength(50);
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(254);
        
        RuleFor(x => x.PasswordHash)
            .NotEmpty()
            .MaximumLength(100);
    }
}
