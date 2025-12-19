namespace YugiohLifeCounter.Core.Models;

public sealed record LogEntry(
    DateTime Utc,
    PlayerId Player,
    int Delta,
    int NewLifePoints);