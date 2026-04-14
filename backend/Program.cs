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

record QuoteRequest(
    decimal KitchenSize,
    bool InstallationByMagnet,
    decimal WallArea,
    bool? HasDishwasher,
    int? TotalAppliances,
    int? Sockets
);

