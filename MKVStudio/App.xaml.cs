using Microsoft.Extensions.DependencyInjection;
using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
//using Forms = System.Windows.Forms;

namespace MKVStudio;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        IServiceProvider serviceProvider = CreateServiceProvider();

        MainWindow mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    private static IServiceProvider CreateServiceProvider()
    {
        IServiceCollection services = new ServiceCollection();

        _ = services.AddSingleton<IExternalLibrariesService, ExternalLibrariesService>();
        _ = services.AddSingleton<IUtilitiesService, UtilitiesService>();

        _ = services.AddSingleton<MainVM>();

        _ = services.AddSingleton(f => new MainWindow(f.GetRequiredService<MainVM>()));

        return services.BuildServiceProvider();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        DirectoryInfo di = new(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $"\\temp");
        if (di.Exists)
        {
            di.Delete(true);
        }
        base.OnExit(e);
    }
}
