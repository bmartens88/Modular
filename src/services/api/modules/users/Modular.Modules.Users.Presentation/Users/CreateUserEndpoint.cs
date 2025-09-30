using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using Modular.Common.Domain.Monads;
using Modular.Common.Presentation.Endpoints;
using Modular.Common.Presentation.Results;
using Modular.Modules.Users.Application.Users.Create;
using Modular.Modules.Users.Domain;

namespace Modular.Modules.Users.Presentation.Users;

/// <summary>
///     <see cref="IEndpoint" /> implementation for creating a new user.
/// </summary>
internal sealed class CreateUserEndpoint : IEndpoint
{
    /// <inheritdoc />
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async Task<Results<ProblemHttpResult, Created<CreateUser.Response>>> (
                [FromBody] CreateUser.Command command,
                [FromServices] ISender sender,
                CancellationToken cancellationToken = default) =>
            {
                CreateUserCommand cmd = new(command.FirstName, command.LastName, command.Email);
                Result<User> result = await sender.Send(cmd, cancellationToken);
                if (result.IsFailure)
                {
                    return ApiResults.Problem(result);
                }

                User user = result.Value;
                Guid id = user.Id.Value;
                CreateUser.Response response = new(id, user.FirstName, user.LastName, user.Email);
                return TypedResults.Created($"/users/{id}", response);
            })
            .WithTags(Tags.Users);
    }
}