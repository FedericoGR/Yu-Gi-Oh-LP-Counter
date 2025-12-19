using YugiohLifeCounter.ViewModels;

namespace YugiohLifeCounter;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        this.InitializeComponent();
        this.BindingContext = vm;
    }
}