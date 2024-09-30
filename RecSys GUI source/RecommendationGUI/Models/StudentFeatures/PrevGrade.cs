namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Static class holding previous grade utilities.
/// </summary>
public static class PrevGrade
{
    /// <summary>
    /// Default previous grade in % (to display before input).
    /// </summary>
    public static float DefaultPercentage { get; } = 80;

    /// <summary>
    /// Default previous grade in dataset form.
    /// </summary>
    public static float DefaultOption { get; } = BackendValFromPercentage(DefaultPercentage);

    /// <summary>
    /// Converts previous grade from percentage to dataset form.
    /// </summary>
    /// <param name="percentage">Previous grade in percentage.</param>
    /// <returns>Previous grade in the same form it would appear in the backend dataset.</returns>
    public static float BackendValFromPercentage(float percentage) => 2 * percentage;
}
