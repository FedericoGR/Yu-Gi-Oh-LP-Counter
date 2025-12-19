using YugiohLifeCounter.Navigation;
using YugiohLifeCounter.Views;

namespace YugiohLifeCounter;

public partial class AppShell : Shell
{
    public AppShell()
    {
        this.InitializeComponent();

        Routing.RegisterRoute(Routes.Calculator, typeof(CalculatorPage));
    }
}