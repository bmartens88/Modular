using Modular.Common.Application;
using Modular.Common.Infrastructure;
using Modular.Common.Presentation.Endpoints;
using Modular.Modules.Users.Application;
using Modular.Modules.Users.Infrastructure;
using Modular.WebApi.Middleware;

using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

builder.Services.AddApplication(AssemblyReference.Assembly);

builder.Services.AddInfrastructure();

builder.Services.AddUserModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();
app.MapDefaultEndpoints();

app.Run();