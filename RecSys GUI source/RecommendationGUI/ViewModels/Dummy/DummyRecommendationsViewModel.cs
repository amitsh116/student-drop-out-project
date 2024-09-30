using RecommendationGUI.BackEnd;

namespace RecommendationGUI.ViewModels.Dummy;

/// <summary>
/// Dummy version of `RecommendationsViewModel`.
/// </summary>
public class DummyRecommendationsViewModel : RecommendationsViewModel
{
    public DummyRecommendationsViewModel() : base(new DummyBackEnd())
    {
        Update(new());
    }
}
