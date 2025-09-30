using FluentValidation;

namespace Modular.Modules.Users.Application.Users.GetById;

/// <summary>
///     Defines a validator for the <see cref="GetUserByIdQuery" /> class.
/// </summary>
internal sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    /// <summary>
    ///     Constructor of the <see cref="GetUserByIdQueryValidator" /> class.
    /// </summary>
    public GetUserByIdQueryValidator()
    {
        RuleFor(q => q.UserId)
            .NotNull()
            .NotEmpty()
            .ChildRules(userId =>
                userId.RuleFor(id => id.Value)
                    .NotNull()
                    .NotEmpty()
                    .NotEqual(Guid.Empty));
    }
}