using RecommendationGUI.Models.StudentFeatures;

namespace RecommendationGUI.ViewModels.Dummy;

/// <summary>
/// Dummy version of `RecommendationViewModel`
/// </summary>
public class DummyRecommendationViewModel() : RecommendationViewModel(
    orgCourse: new(id: 3, name: "Gablpmoo", time: DaytimeEveningAttendance.Daytime),
    orgChance: 80,
    actionsRec: [new("Remove 1 course", 15), new("Add night courses", 5)],
    coursesRec: [new("Some Course (1)", 17), new("Another Course", 7)]
) { }
