using WeatherService.Api.Client;
using WeatherService.Api.Controllers;
using WeatherService.Api.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<WeatherServiceSettings>
        (builder.Configuration.GetSection("Settings"));

// Add services to the container.


builder.Services.AddHttpClient();
builder.Services.AddTransient<WeatherClient>();
builder.Services.AddTransient<StatusController>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
