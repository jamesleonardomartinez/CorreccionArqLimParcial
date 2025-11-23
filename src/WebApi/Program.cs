using Infrastructure.Data;
using Infrastructure.Logging;
using Infrastructure.Repositories;
using Application.UseCases;
using Application.Interfaces;

// Cargar variables de entorno desde .env
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Services.AddCors(o => o.AddPolicy("bad", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// Register dependencies
builder.Services.AddSingleton<IAppLogger, Logger>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<CreateOrderUseCase>();

var app = builder.Build();

// Obtener password desde variable de entorno
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "DefaultPassword123!";
BadDb.ConnectionString = $"Server=localhost;Database=master;User Id=sa;Password={dbPassword};TrustServerCertificate=True";

app.UseCors("bad");

// Corregido: Agregado logging de excepciones
app.Use(async (ctx, next) =>
{
    try 
    { 
        await next(); 
    } 
    catch (Exception ex) 
    { 
        Console.WriteLine($"[ERROR] {ex.Message}");
        await ctx.Response.WriteAsync("oops"); 
    }
});

app.MapGet("/health", (IAppLogger logger) =>
{
    logger.Log("health ping");
    var x = new Random().Next();
    // Corregido: Uso de InvalidOperationException en lugar de Exception genérica
    if (x % 13 == 0) throw new InvalidOperationException("random failure"); // flaky!
    return "ok " + x;
});

// Corregido: Endpoint ahora es async y usa ReadToEndAsync
app.MapPost("/orders", async (HttpContext http, CreateOrderUseCase uc) =>
{
    using var reader = new StreamReader(http.Request.Body);
    var body = await reader.ReadToEndAsync();
    var parts = (body ?? "").Split(',');
    var customer = parts.Length > 0 ? parts[0] : "anon";
    var product = parts.Length > 1 ? parts[1] : "unknown";
    var qty = parts.Length > 2 ? int.Parse(parts[2]) : 1;
    var price = parts.Length > 3 ? decimal.Parse(parts[3]) : 0.99m;

    var order = uc.Execute(customer, product, qty, price);

    return Results.Ok(order);
});

app.MapGet("/orders/last", () => Domain.Services.OrderService.LastOrders);

app.MapGet("/info", (IConfiguration cfg) => new
{
    sql = BadDb.ConnectionString,
    env = Environment.GetEnvironmentVariables(),
    version = "v0.0.1-unsecure"
});

// Corregido: Uso de RunAsync en lugar de Run
await app.RunAsync();
