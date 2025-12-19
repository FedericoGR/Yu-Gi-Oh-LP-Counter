using CommunityToolkit.Mvvm.DependencyInjection;
using YugiohLifeCounter.Core.Models;
using YugiohLifeCounter.ViewModels;

namespace YugiohLifeCounter.Views;

[QueryProperty(nameof(Player), "player")]
public partial class CalculatorPage : ContentPage
{
    private readonly CalculatorViewModel viewModel;

    public string Player { get; set; } = "P1";

    public CalculatorPage()
    {
        this.InitializeComponent();
        this.viewModel = Ioc.Default.GetRequiredService<CalculatorViewModel>();
        this.BindingContext = this.viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (!Enum.TryParse<PlayerId>(this.Player, out var playerId))
        {
            playerId = PlayerId.P1;
        }

        this.viewModel.Initialize(playerId);
    }
}