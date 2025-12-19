namespace YugiohLifeCounter;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        this.InitializeComponent();
        this.MainPage = new AppShell();
    }
}