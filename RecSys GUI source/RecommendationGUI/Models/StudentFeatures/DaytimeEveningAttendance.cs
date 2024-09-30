namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Represents attendance time.
/// </summary>
public class DaytimeEveningAttendance : CategorialBase
{
    /// <summary>
    /// Consturctor of `DaytimeEveningAttendance`.
    /// </summary>
    /// <param name="id">Numeric ID of attendance time.</param>
    /// <param name="name">Display name of attendance time.</param>
    protected DaytimeEveningAttendance(int id, string name) : base(id, name) { }

    /// <summary>
    /// Daytime attendance.
    /// </summary>
    public static DaytimeEveningAttendance Daytime { get; } = new(1, "Daytime");

    /// <summary>
    /// Evening attendance.
    /// </summary>
    public static DaytimeEveningAttendance Evening { get; } = new(0, "Evening");

    /// <summary>
    /// Default attendance time (to display before input).
    /// </summary>
    public static DaytimeEveningAttendance DefaultOption { get; } = Daytime;

    /// <summary>
    /// All attendance time options.
    /// </summary>
    public static DaytimeEveningAttendance[] AllOptions { get; } =
    [
        Daytime, Evening
    ];
}
