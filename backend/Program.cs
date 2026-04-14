var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string FrontendCorsPolicy = "FrontendCors";
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
c
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        if (allowedOrigins is { Length: > 0 })
        {
            policy.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(FrontendCorsPolicy);

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/api/customers", () =>
{
    var customers = new[]
    {
        new Customer(1, "Alice Johnson"),
        new Customer(2, "Brian Smith"),
        new Customer(3, "Carla Martinez"),
        new Customer(4, "Dimitris Papadopoulos")
    };

    return customers;
})
.WithName("GetCustomers")
.WithOpenApi();

app.MapPost("/api/quote/calculate", (QuoteRequest request) =>
{
    var flooring = request.KitchenSize * 120m;
    var plastering = request.WallArea * 50m;

    var expenses = new Dictionary<string, decimal>
    {
        ["Flooring"] = Math.Round(flooring, 2),
        ["Plastering"] = Math.Round(plastering, 2),
    };

    if (!request.InstallationByMagnet)
    {
        var applianceWiring = request.TotalAppliances.GetValueOrDefault() * 300m;
        var sockets = request.Sockets.GetValueOrDefault() * 150m;
        var plumbing = request.HasDishwasher.GetValueOrDefault() == true ? 120m : 0m;
        var installation = request.KitchenSize * 250m;

        expenses.Add("Appliance wiring", Math.Round(applianceWiring, 2));
        expenses.Add("Electricity", Math.Round(sockets, 2));
        expenses.Add("Plumbing", Math.Round(plumbing, 2));
        expenses.Add("Installation", Math.Round(installation, 2));
    }

    return Results.Ok(expenses);
})
.WithName("CalculateQuote")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record Customer(int Id, string Name);

record QuoteRequest(
    decimal KitchenSize,
    bool InstallationByMagnet,
    decimal WallArea,
    bool? HasDishwasher,
    int? TotalAppliances,
    int? Sockets
);

