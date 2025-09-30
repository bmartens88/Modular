var builder = DistributedApplication.CreateBuilder(args);

var usersDb = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin()
    .AddDatabase("usersDb");

builder.AddProject<Projects.Modular_WebApi>("api")
    .WithReference(usersDb)
    .WaitFor(usersDb);

builder.Build().Run();