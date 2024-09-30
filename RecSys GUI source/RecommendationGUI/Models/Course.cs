using RecommendationGUI.Models.StudentFeatures;

namespace RecommendationGUI.Models;

/// <summary>
/// Represents a course.
/// </summary>
/// <param name="id">Course ID.</param>
/// <param name="name">Course's name.</param>
/// <param name="time">When the course is taken (Daytime/Evening).</param>
public class Course(int id, string name, DaytimeEveningAttendance time) : IDisplayable
{
    /// <summary>
    /// Course ID.
    /// </summary>
    public int ID { get; } = id;

    /// <summary>
    /// Course's name.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// When the course is taken (Daytime/Evening).
    /// </summary>
    public DaytimeEveningAttendance Time { get; } = time;

    public string Display => $"({ID}) {Name}";

    /// <summary>
    /// Compares two courses by their IDs (to define course display order).
    /// </summary>
    /// <param name="a">First course to compare its ID.</param>
    /// <param name="b">Second course to compare its ID.</param>
    /// <returns>-1 if a's ID is lower, 1 if a's ID is greater, 0 if equal.</returns>
    public static int CompareIDs(Course a, Course b) => a.ID.CompareTo(b.ID);
}
