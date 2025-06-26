
using log4net.Config;
using log4net;
using System.Reflection;
using FTD_Asia_Test.Middleware;
using FTD_Asia_Test.Extensions;

var builder = WebApplication.CreateBuilder(args);

var log4NetConfigFile = builder.Configuration["Logging:Log4NetConfigFile"];
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo(log4NetConfigFile));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
