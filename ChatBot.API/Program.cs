using ChatBot.Infra.IoC;
using StockBot.MessageBroker;
using StockBot.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();

var configuration = builder.Configuration;
builder.Services.RegisterDatabase(
                configuration.GetSection("DatabaseStrings")["CosmosDB"],
                configuration.GetSection("DatabaseStrings")["DatabaseName"]);
builder.Services.RegisterReceiveStockListener(
                configuration.GetSection("QueueStrings")["QueueEndpoint"],
                configuration.GetSection("QueueStrings")["QueueStockName"]);

// External Project - Bot Dependencies
builder.Services.AddSingleton<StockBotService, StockBotService>();
builder.Services.AddHttpClient<StockBotService>();
builder.Services.AddSingleton<ISendStockQueue, SendStockQueue>();

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
