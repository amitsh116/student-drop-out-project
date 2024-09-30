namespace RecommendationGUI.Models;

/// <summary>
/// Represents a recommendation.
/// </summary>
/// <param name="description">Recommendation description.</param>
/// <param name="improveRate">How much the recommendation is estimated to make an improvement (%).</param>
public class Recommendation(string description, float improveRate)
{
    /// <summary>
    /// Recommendation description.
    /// </summary>
    public string Description { get; } = description;

    /// <summary>
    /// How much the recommendation is estimated to make an improvement (%).
    /// </summary>
    public float ImproveRate { get; } = improveRate;
}
