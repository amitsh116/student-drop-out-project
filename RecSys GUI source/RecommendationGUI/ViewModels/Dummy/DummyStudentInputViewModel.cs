using RecommendationGUI.BackEnd;

namespace RecommendationGUI.ViewModels.Dummy;

/// <summary>
/// Dummy version of `StudentInputViewModel`.
/// </summary>
public class DummyStudentInputViewModel() : StudentInputViewModel(new DummyBackEnd()) { }
