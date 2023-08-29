using ULSolutions.Factories;
using ULSolutions.Services;
using ULSolutions.Services.ExpressionStrategies;
using ULSolutions.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services
builder.Services.AddTransient<IExpressionCalculationService, ExpressionCalculationService>();
builder.Services.AddTransient<IBinaryOperatorStrategy, AddOperatorStrategy>();
builder.Services.AddTransient<IBinaryOperatorStrategy, DivideOperatorStrategy>();
builder.Services.AddTransient<IBinaryOperatorStrategy, SubtractOperatorStrategy>();
builder.Services.AddTransient<IBinaryOperatorStrategy, MultiplyOperatorStrategy>();

//Factories
builder.Services.AddTransient<IOperatorStrategyFactory, OperatorStrategyFactory>();

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

public partial class Program {}