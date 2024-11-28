using DatabaseLibrary.Context;
using DatabaseLibrary.Repositories;
using DomainLibrary.Interfaces.Repositories;
using DomainLibrary.Interfaces.Services;
using DomainLibrary.Models.LogModels;
using Microsoft.OpenApi.Models;
using Observer.Constants;
using Observer.Services;
using SingleLog;
using SingleLog.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification API", Version = "v1", Description = SwaggerDocumentation.SwaggerDescription });
    c.IncludeXmlComments(string.Format(@"{0}\Observer.xml", AppContext.BaseDirectory));
    
});

//Dependence injection configuration

builder.Services.AddScoped<ISqlServerContext, SqlServerContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();

builder.Services.AddScoped<ISingletonLogger<LogModel>, SingletonLogger<LogModel>>();

//Configure database connection

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();