using Avalonia.Controls;

namespace RecommendationGUI.Views;

/// <summary>
/// Loading screen view, displayed while backend is loading recommendations.
/// </summary>
public partial class LoadingScreenView : UserControl
{
    /// <summary>
    /// Constructs a `LoadingScreenView`.
    /// </summary>
    public LoadingScreenView()
    {
        InitializeComponent();
    }
}