using System.Collections.Generic;

namespace YugiohLifeCounter.Core.Models;

public sealed class GameState
{
    public int P1LifePoints { get; set; } = 8000;
    public int P2LifePoints { get; set; } = 8000;

    // Más reciente primero
    public LinkedList<LogEntry> Log { get; } = new();

    // Stack de acciones deshechas (para redo)
    public Stack<LogEntry> UndoStack { get; } = new();
}