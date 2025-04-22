using ZenMonkey.Application.Telegram.Services;
using ZenMonkey.Application.Telegram.Services.Interfaces;
using ZenMonkey.Application.Trades.Services;
using ZenMonkey.Application.Trades.Services.Interfaces;
using ZenMonkey.Hub.Infrastructure.GeckoTerminal.Interface;
using ZenMonkey.Hub.Infrastructure.GeckoTerminal.Repository;
using ZenMonkey.Hub.Infrastructure.Logger;
using ZenMonkey.Hub.Infrastructure.Telegram.Interfaces;
using ZenMonkey.Hub.Infrastructure.Telegram.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
});

// Add services to the container.
builder.Services.AddHostedService<BotService>();

builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<ITelegramService, TelegramService>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<ITelegramRepository, TelegramRepository>();

builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
