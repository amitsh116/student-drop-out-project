using RecommendationGUI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RecommendationGUI.ViewModels;

/// <summary>
/// ViewModel used for displaying recommendations in textual form.
/// </summary>
/// <param name="title">Title for textual recommendations ("suggestions").</param>
/// <param name="suggestions">An IEnumerable of recommendations to display in textual form.</param>
public class SuggestionsViewModel(string title, IEnumerable<Recommendation> suggestions) : ViewModelBase
{
    /// <summary>
    /// Title for textual recommendation.
    /// </summary>
    public string Title { get; } = title;

    /// <summary>
    /// Whether or not has any suggestions to display.
    /// </summary>
    public bool HasSuggestions => Suggestions.Count > 0;

    /// <summary>
    /// Recommendations to display in textual form.
    /// </summary>
    public ObservableCollection<Recommendation> Suggestions { get; } = new(suggestions);
}
