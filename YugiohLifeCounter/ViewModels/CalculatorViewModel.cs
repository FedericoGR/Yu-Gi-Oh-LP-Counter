using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YugiohLifeCounter.Application.Services;
using YugiohLifeCounter.Core.Calculator;
using YugiohLifeCounter.Core.Models;

namespace YugiohLifeCounter.ViewModels;

public partial class CalculatorViewModel : ObservableObject
{
    private readonly GameService gameService;
    private readonly LifePointCalculator calculator;

    private PlayerId player;

    [ObservableProperty] private string header = "Calculator";
    [ObservableProperty] private string current = string.Empty;
    [ObservableProperty] private string preview = string.Empty;
    [ObservableProperty] private string expression = string.Empty;
    [ObservableProperty] private string error = string.Empty;

    public CalculatorViewModel(GameService gameService, LifePointCalculator calculator)
    {
        this.gameService = gameService;
        this.calculator = calculator;
    }

    public void Initialize(PlayerId playerId)
    {
        this.player = playerId;

        int lp = this.player == PlayerId.P1 ? this.gameService.State.P1LifePoints : this.gameService.State.P2LifePoints;

        this.Header = this.player == PlayerId.P1 ? "Player 1" : "Player 2";
        this.Current = $"Current: {lp}";
        this.Expression = string.Empty;
        this.Error = string.Empty;
        this.UpdatePreview();
    }

    partial void OnExpressionChanged(string value) => this.UpdatePreview();

    [RelayCommand]
    private void Quick(string value)
    {
        this.Expression = value;
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task ApplyAsync()
    {
        try
        {
            this.Error = string.Empty;

            int currentLp = this.player == PlayerId.P1 ? this.gameService.State.P1LifePoints : this.gameService.State.P2LifePoints;
            int newLp = this.calculator.Apply(this.Expression, currentLp);

            int delta = newLp - currentLp;
            if (delta != 0)
            {
                this.gameService.AddDelta(this.player, delta);
            }

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            this.Error = ex.Message;
        }
    }

    private void UpdatePreview()
    {
        int currentLp = this.player == PlayerId.P1 ? this.gameService.State.P1LifePoints : this.gameService.State.P2LifePoints;

        if (string.IsNullOrWhiteSpace(this.Expression))
        {
            this.Preview = "Preview: -";
            return;
        }

        try
        {
            int newLp = this.calculator.Apply(this.Expression, currentLp);
            this.Preview = $"Preview: {newLp}";
            this.Error = string.Empty;
        }
        catch
        {
            this.Preview = "Preview: -";
        }
    }
}