using EBTCO;
using EBTCO.Core.ServiceRegister;
using EBTCO.DB;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToursYard.ServicesRegisteration;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetEntryAssembly();

if (assembly != null)
{
    builder.Configuration.AddUserSecrets(assembly).AddEnvironmentVariables();
}
MediatorServiceRegistration.AddApplicationServices(builder.Services);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DatabaseServiceRegistration.AddApplicationServices(builder.Services, builder.Configuration);
IdentityServiceRegistration.AddApplicationServices(builder.Services, builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
