using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

using Modular.Modules.Users.Data.Contexts;
using Modular.Modules.Users.Services.MigrationService;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddDbContextPool<UsersDbContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("usersDb"),
            npgsqlOptions => npgsqlOptions
                .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
        .UseSnakeCaseNamingConvention());

IHost host = builder.Build();
host.Run();