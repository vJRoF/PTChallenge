using EasyNetQ;
using PTChallenge.App1;
using PTChallenge.App2;
using PTChallenge.Common;
using PTChallenge.Common.Calculators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterEasyNetQ("host=localhost", register => register.EnableMicrosoftLogging());
builder.Services.AddScoped<IFibonacciCalculator, FibonacciLoopCalculator>();
builder.Services.AddScoped<Worker>();
builder.Services.AddScoped<INumberSender, RabbitSender>();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();