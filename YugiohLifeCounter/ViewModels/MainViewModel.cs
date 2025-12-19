using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YugiohLifeCounter.Application.Services;
using YugiohLifeCounter.Core.Models;
using YugiohLifeCounter.Navigation;

namespace YugiohLifeCounter.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly GameService game;

    public ObservableCollection<LogEntry> Log { get; } = new();

    [ObservableProperty] private int p1LifePoints;
    [ObservableProperty] private int p2LifePoints;

    public MainViewModel(GameService game)
    {
        this.game = game;
        this.SyncFromState();
    }
    public void Refresh()
    {
        this.SyncFromState();
    }

    [RelayCommand]
    private void P1Minus100() { this.game.AddDelta(PlayerId.P1, -100); this.SyncFromState(); }

    [RelayCommand]
    private void P2Minus100() { this.game.AddDelta(PlayerId.P2, -100); this.SyncFromState(); }

    [RelayCommand]
    private void Undo() { this.game.Undo(); this.SyncFromState(); }

    [RelayCommand]
    private void Redo() { this.game.Redo(); this.SyncFromState(); }

    [RelayCommand]
    private async Task OpenCalculatorAsync(PlayerId player)
    {
        await Shell.Current.GoToAsync($"{Routes.Calculator}?player={player}");
    }

    private void SyncFromState()
    {
        this.P1LifePoints = this.game.State.P1LifePoints;
        this.P2LifePoints = this.game.State.P2LifePoints;

        this.Log.Clear();
        foreach (var e in this.game.State.Log)
        {
            this.Log.Add(e);
        }
    }
}