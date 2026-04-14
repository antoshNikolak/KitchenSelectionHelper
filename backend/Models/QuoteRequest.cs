public record QuoteRequest(
    decimal KitchenSize,
    bool InstallationByMagnet,
    decimal WallArea,
    bool? HasDishwasher,
    int? TotalAppliances,
    int? Sockets
);