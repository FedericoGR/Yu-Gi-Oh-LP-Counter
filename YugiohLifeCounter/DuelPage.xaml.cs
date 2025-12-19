using CommunityToolkit.Mvvm.DependencyInjection;
using YugiohLifeCounter.ViewModels;

namespace YugiohLifeCounter.Views;

public partial class DuelPage : ContentPage
{
    private readonly MainViewModel viewModel;

    public DuelPage()
    {
        this.InitializeComponent();

        this.viewModel = Ioc.Default.GetRequiredService<MainViewModel>();
        this.BindingContext = this.viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.viewModel.Refresh();
    }
}