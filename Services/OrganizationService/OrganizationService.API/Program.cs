using Microsoft.EntityFrameworkCore;
using OrganizationService.API.Extensions;
using OrganizationService.API.Middlewares;
using OrganizationService.Application;
using OrganizationService.Infrastructure;
using OrganizationService.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppExtensions(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        await context.Database.MigrateAsync();
        
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex.Message);
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
app.MapControllers();

app.Run();
