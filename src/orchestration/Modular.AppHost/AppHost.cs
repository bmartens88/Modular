using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresDatabaseResource> usersDb = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin()
    .AddDatabase("usersDb");

IResourceBuilder<ProjectResource> usersMigrations = builder
    .AddProject<Modular_Modules_Users_Services_MigrationService>("migrations")
    .WithReference(usersDb)
    .WaitFor(usersDb)
    .WithParentRelationship(usersDb);

builder.AddProject<Modular_WebApi>("api")
    .WithReference(usersDb)
    .WithReference(usersMigrations)
    .WaitForCompletion(usersMigrations);

builder.Build().Run();