using System;

namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Static class holding semester grade utilities.
/// </summary>
public static class SemGrade
{
    /// <summary>
    /// Default semester grade in % (to display before input).
    /// </summary>
    public static int DefaultPercentage { get; } = 80;

    /// <summary>
    /// Default semester grade in dataset form.
    /// </summary>
    public static int DefaultOption { get; } = BackendValFromPercentage(DefaultPercentage);

    /// <summary>
    /// Converts semester grade from percentage to dataset form.
    /// </summary>
    /// <param name="percentage">Semester grade in percentage.</param>
    /// <returns>Semester grade in the same form it would appear in the backend dataset.</returns>
    public static int BackendValFromPercentage(float percentage) => (int)Math.Round(percentage / 5);
}
