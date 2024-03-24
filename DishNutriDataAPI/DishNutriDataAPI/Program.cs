using DishNutriDataAPI.Controllers;
using DishNutriDataAPI.Properties;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<NutritionalDataPredictionController>());
var app = builder.Build();

// Load Environment Variables
DotEnv.Load();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();
