var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string FrontendCorsPolicy = "FrontendCors";
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

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
    var materials = request.KitchenSize * 120m + request.WallArea * 45m;
    var installation = request.InstallationByMagnet ? request.KitchenSize * 80m : 0m;

    var applianceWiring = request.InstallationByMagnet
        ? 0m
        : request.TotalAppliances.GetValueOrDefault() * 60m;
    var sockets = request.InstallationByMagnet
        ? 0m
        : request.Sockets.GetValueOrDefault() * 35m;
    var dishwasher = request.InstallationByMagnet
        ? 0m
        : (request.HasDishwasher == true ? 90m : 0m);

    var expenses = new Dictionary<string, decimal>
    {
        ["materials"] = Math.Round(materials, 2),
        ["installation"] = Math.Round(installation, 2),
        ["appliance wiring"] = Math.Round(applianceWiring, 2),
        ["sockets"] = Math.Round(sockets, 2),
        ["dishwasher"] = Math.Round(dishwasher, 2)
    };var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string FrontendCorsPolicy = "FrontendCors";
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

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
    var materials = request.KitchenSize * 120m + request.WallArea * 45m;
    var installation = request.InstallationByMagnet ? request.KitchenSize * 80m : 0m;

    var applianceWiring = request.InstallationByMagnet
        ? 0m
        : request.TotalAppliances.GetValueOrDefault() * 60m;
    var sockets = request.InstallationByMagnet
        ? 0m
        : request.Sockets.GetValueOrDefault() * 35m;
    var dishwasher = request.InstallationByMagnet
        ? 0m
        : (request.HasDishwasher == true ? 90m : 0m);

    var expenses = new Dictionary<string, decimal>
    {
        ["materials"] = Math.Round(materials, 2),
        ["installation"] = Math.Round(installation, 2),
        ["appliance wiring"] = Math.Round(applianceWiring, 2),
        ["sockets"] = Math.Round(sockets, 2),
        ["dishwasher"] = Math.Round(dishwasher, 2)
    };

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

