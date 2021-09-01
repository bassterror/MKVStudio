using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MKVStudio.Models;
using MKVStudio.ViewModels;
using System;
using System.IO;
using System.Windows;

namespace MKVStudio
{
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
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
#if DEBUG
            builder.AddJsonFile("appsettings.Dev.json", optional: false, reloadOnChange: true);
#else
            builder.AddJsonFile("appsettings.Prod.json", optional: false, reloadOnChange: true);
#endif

            IConfiguration configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            _ = services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

            _ = services.AddScoped<MainWindowViewModel>();
            _ = services.AddScoped(f => new MainWindow(f.GetRequiredService<MainWindowViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
