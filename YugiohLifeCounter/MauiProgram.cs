using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using YugiohLifeCounter.Application.Services;
using YugiohLifeCounter.Core.Calculator;
using YugiohLifeCounter.ViewModels;

namespace YugiohLifeCounter;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        // Services
        builder.Services.AddSingleton<GameService>();
        builder.Services.AddSingleton<LifePointCalculator>();

        // ViewModels
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<CalculatorViewModel>();

        var app = builder.Build();

        // Bridge for CommunityToolkit.Mvvm Ioc
        Ioc.Default.ConfigureServices(app.Services);

        return app;
    }
}