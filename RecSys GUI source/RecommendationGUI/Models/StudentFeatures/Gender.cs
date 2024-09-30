namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Represents student's gender, as appears in dataset.
/// </summary>
public class Gender : CategorialBase
{
    /// <summary>
    /// Constructor of `Gender`.
    /// </summary>
    /// <param name="id">Representative numeric ID.</param>
    /// <param name="name">Display name.</param>
    protected Gender(int id, string name) : base(id, name) { }

    public static Gender Female { get; } = new(0, "Female");
    public static Gender Male { get; } = new(1, "Male");

    /// <summary>
    /// Default gender option (to display before input).
    /// </summary>
    public static Gender DefaultOption { get; } = Female;

    /// <summary>
    /// All gender options as provided in dataset.
    /// </summary>
    public static Gender[] AllOptions { get; } =
    [
        Female, Male
    ];
}
