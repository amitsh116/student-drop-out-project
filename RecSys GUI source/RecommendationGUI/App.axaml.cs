using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using RecommendationGUI.Utils;
using RecommendationGUI.ViewModels;
using RecommendationGUI.Views;
using System.IO;

namespace RecommendationGUI;

/// <summary>
/// Main application class.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes application.
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Runs after application initialization is complete.
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        if (File.Exists("scripts_folder.txt"))
            ScriptUtils.ScriptsFolder = File.ReadAllText("scripts_folder.txt");

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(BackendUtils.RetreiveBackEnd())
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(BackendUtils.RetreiveBackEnd())
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
