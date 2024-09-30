namespace RecommendationGUI.ViewModels.Dummy;

/// <summary>
/// Dummy version of `SuggestionsViewModel`.
/// </summary>
public class DummySuggestionsViewModel()
    : SuggestionsViewModel("Suggestions", [new("Some suggestion", 10)]) { }
