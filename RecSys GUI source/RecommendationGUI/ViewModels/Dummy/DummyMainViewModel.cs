using RecommendationGUI.BackEnd;

namespace RecommendationGUI.ViewModels.Dummy;

/// <summary>
/// Dummy version of `MainViewModel`, used dummy backend.
/// </summary>
public class DummyMainViewModel() : MainViewModel(new DummyBackEnd()) { }
