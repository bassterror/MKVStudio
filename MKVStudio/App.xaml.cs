using Microsoft.Extensions.DependencyInjection;
using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace MKVStudio;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        IServiceProvider serviceProvider = CreateServiceProvider();

        if (UtilitiesService.CheckMKVStudioRegistryKey())
        {
            UtilitiesService.CreateMKVStudioRegistryKeys();
        }

        MainWindow mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    private static IServiceProvider CreateServiceProvider()
    {
        IServiceCollection services = new ServiceCollection();

        _ = services.AddSingleton<IUtilitiesService, UtilitiesService>();
        _ = services.AddSingleton<IExternalLibrariesService, ExternalLibrariesService>();

        _ = services.AddScoped<MainVM>();
        _ = services.AddScoped(f => new MainWindow(f.GetRequiredService<MainVM>()));

        return services.BuildServiceProvider();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        DirectoryInfo di = new(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"\\temp");

        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            dir.Delete(true);
        }
        base.OnExit(e);
    }
}
