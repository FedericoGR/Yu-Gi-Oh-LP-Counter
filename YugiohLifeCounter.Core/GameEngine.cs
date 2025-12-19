using YugiohLifeCounter.Core.Models;

namespace YugiohLifeCounter.Core;

public sealed class GameEngine
{
    public const int MaxLogEntries = 1000;

    public void ApplyDelta(GameState state, PlayerId player, int delta)
    {
        int newLp = Math.Max(0, GetLp(state, player) + delta);
        SetLp(state, player, newLp);

        // Nuevo cambio invalida redo
        state.UndoStack.Clear();

        state.Log.AddFirst(new LogEntry(DateTime.UtcNow, player, delta, newLp));
        TrimLog(state);
    }

    public bool Undo(GameState state)
    {
        if (state.Log.First is null) return false;

        var last = state.Log.First.Value;
        state.Log.RemoveFirst();

        // inversa
        int prev = Math.Max(0, last.NewLifePoints - last.Delta);
        SetLp(state, last.Player, prev);

        state.UndoStack.Push(last);
        return true;
    }

    public bool Redo(GameState state)
    {
        if (state.UndoStack.Count == 0) return false;

        var entry = state.UndoStack.Pop();
        SetLp(state, entry.Player, entry.NewLifePoints);

        state.Log.AddFirst(entry);
        TrimLog(state);
        return true;
    }

    public void Reset(GameState state, int startingLp)
    {
        state.P1LifePoints = startingLp;
        state.P2LifePoints = startingLp;
        state.Log.Clear();
        state.UndoStack.Clear();
    }

    private static int GetLp(GameState state, PlayerId player)
        => player == PlayerId.P1 ? state.P1LifePoints : state.P2LifePoints;

    private static void SetLp(GameState state, PlayerId player, int value)
    {
        if (player == PlayerId.P1) state.P1LifePoints = value;
        else state.P2LifePoints = value;
    }

    private static void TrimLog(GameState state)
    {
        while (state.Log.Count > MaxLogEntries)
        {
            state.Log.RemoveLast();
        }
    }
}