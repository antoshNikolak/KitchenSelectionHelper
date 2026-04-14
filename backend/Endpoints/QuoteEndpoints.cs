public static class QuoteEndpoints
{
    public static void MapQuoteEndpoints(this WebApplication app)
    {
        app.MapPost("/api/quote/calculate", CalculateQuote)
            .WithName("CalculateQuote")
            .WithOpenApi();
    }

    private static IResult CalculateQuote(QuoteRequest request)
    {
        QuoteCalculator quoteCalculator = new QuoteCalculator();
        var expenses = quoteCalculator.Calculate(request);
        return Results.Ok(expenses);
    }

}