
public class QuoteCalculator
{
    public Dictionary<string, decimal> Calculate(QuoteRequest request)
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

        return expenses;

    }
}