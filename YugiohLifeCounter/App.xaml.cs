namespace YugiohLifeCounter;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App(MainPage page)
    {
        this.InitializeComponent();
        this.MainPage = page;
    }
}