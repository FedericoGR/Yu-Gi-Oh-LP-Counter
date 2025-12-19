using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using YugiohLifeCounter.Application;
using YugiohLifeCounter.ViewModels;

namespace YugiohLifeCounter;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        builder.Services.AddSingleton<GameService>();
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();

        return builder.Build();
    }
}