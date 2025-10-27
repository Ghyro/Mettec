using MettecService.API;
using MettecService.API.Extensions;
using MettecService.API.Healthchecks;
using MettecService.API.StartupExtensions;
using MettecService.Core.Services;
using MettecService.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("Database connection");

builder.Services.AddScoped<IMetterService, MettecService.Core.Services.MettecService>();
builder.Services.AddScoped<IMetterRepository, MetterRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health",
new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = HealthCheckExtensions.HealthCheckResponseWriter.WriteResponse
});

app.ApplyDatabaseMigrationsIfNeeded();

app.Run();