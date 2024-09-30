using LiveChartsCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RecommendationGUI.ViewModels;

/// <summary>
/// ViewModel used for displaying a recommendations compare pie chart.
/// </summary>
/// <param name="title">Display title.</param>
/// <param name="recommendations">IEnumerable of recommendations effectiveness series.</param>
public class RecChartViewModel(string title, IEnumerable<ISeries> recommendations) : ViewModelBase
{
    /// <summary>
    /// Display title.
    /// </summary>
    public string Title { get; } = title;

    /// <summary>
    /// Recommendations effectiveness series (chart data).
    /// </summary>
    public ObservableCollection<ISeries> Recommendations { get; } = new(recommendations);
}
