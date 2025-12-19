using YugiohLifeCounter.Core;
using YugiohLifeCounter.Core.Models;

namespace YugiohLifeCounter.Application.Services;

public sealed class GameService
{
    private readonly GameEngine engine = new();

    public GameState State { get; } = new();

    public void AddDelta(PlayerId player, int delta) => this.engine.ApplyDelta(this.State, player, delta);
    public bool Undo() => this.engine.Undo(this.State);
    public bool Redo() => this.engine.Redo(this.State);
    public void Reset(int startingLp) => this.engine.Reset(this.State, startingLp);
}