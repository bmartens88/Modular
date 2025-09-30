using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using Modular.Common.Domain.Monads;
using Modular.Common.Presentation.Endpoints;
using Modular.Common.Presentation.Results;
using Modular.Modules.Users.Application.Users.GetById;
using Modular.Modules.Users.Domain;
using Modular.Modules.Users.Domain.ValueObjects;

namespace Modular.Modules.Users.Presentation.Users.GetById;

/// <summary>
///     <see cref="IEndpoint" /> implementation for getting a user by id.
/// </summary>
internal sealed class GetUserByIdEndpoint : IEndpoint
{
    /// <inheritdoc />
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:guid}", async Task<Results<ProblemHttpResult, Ok<GetUserById.Response>>> (
                Guid id,
                [FromServices] ISender sender,
                CancellationToken cancellationToken = default) =>
            {
                GetUserByIdQuery query = new(UserId.Create(id));
                Result<User> result = await sender.Send(query, cancellationToken);
                if (result.IsFailure)
                {
                    return ApiResults.Problem(result);
                }

                User user = result.Value;
                GetUserById.Response response = new(id, user.FirstName, user.LastName, user.Email);
                return TypedResults.Ok(response);
            })
            .WithTags(Tags.Users);
    }
}