using DatabaseLibrary.Context;
using DatabaseLibrary.Repositories;
using DomainLibrary.Interfaces.Repositories;
using DomainLibrary.Models.LogModels;
using ObserverWorker;
using SingleLog.Interfaces;
using SingleLog;

var builder = Host.CreateApplicationBuilder(args);

//set only workerThreads
ThreadPool.SetMaxThreads(int.MaxValue, int.MaxValue);

builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<ISqlServerContext, SqlServerContext>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

builder.Services.AddScoped<ISingletonLogger<LogModel>, SingletonLogger<LogModel>>();

builder.Build().Run();
