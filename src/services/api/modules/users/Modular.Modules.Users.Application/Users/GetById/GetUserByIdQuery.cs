using Modular.Common.Application.Messaging;
using Modular.Modules.Users.Domain;
using Modular.Modules.Users.Domain.ValueObjects;

namespace Modular.Modules.Users.Application.Users.GetById;

/// <summary>
///     Defines a query to get a <see cref="User" /> by id.
/// </summary>
/// <param name="UserId"><see cref="UserId" /> strongly typed identifier.</param>
public sealed record GetUserByIdQuery(UserId UserId) : IQuery<User>;