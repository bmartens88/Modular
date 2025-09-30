using FluentValidation;

using Modular.Modules.Users.Domain.Const;

namespace Modular.Modules.Users.Application.Users.Create;

/// <summary>
///     Defines a validator for the <see cref="CreateUserCommand" /> class.
/// </summary>
internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    ///     Constructor of the <see cref="CreateUserCommandValidator" /> class.
    /// </summary>
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constants.Users.FirstNameMaxLength);

        RuleFor(c => c.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constants.Users.LastNameMaxLength);

        RuleFor(c => c.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(Constants.Users.EmailMaxLength);
    }
}