using Account.Application.Common;
using Account.Application.Interfaces;
using Account.Application.Services;
using Account.Domain.Repositories;
using Account.Infrastructure.Data;
using Account.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

// using Account.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Add more repositories as needed

// Application Services
builder.Services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
// Add more application services here

// MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Result<>).Assembly);
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(Result<>).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => c.RouteTemplate = "api/swagger/{documentname}/swagger.json");
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "api/swagger";
        c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "ACMID API");
    });
    app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
    {
        var routes = endpointSources
            .SelectMany(es => es.Endpoints)
            .OfType<RouteEndpoint>()
            .Select(e => new {
                Pattern = e.RoutePattern.RawText,
                Methods = e.Metadata
                    .OfType<HttpMethodMetadata>()
                    .FirstOrDefault()?
                    .HttpMethods
            });

        return Results.Json(routes);
    });


}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
