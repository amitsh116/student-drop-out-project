namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Represents student's marital status.
/// </summary>
public class MaritalStatus : CategorialBase
{
    /// <summary>
    /// Constructor of `MaritalStatus`.
    /// </summary>
    /// <param name="id">Representative numeric ID.</param>
    /// <param name="name">Display name.</param>
    protected MaritalStatus(int id, string name) : base(id, name) { }

    public static MaritalStatus Single { get; } = new(1, "Single");
    public static MaritalStatus Married { get; } = new(2, "Married");
    public static MaritalStatus Widower { get; } = new(3, "Widower");
    public static MaritalStatus Divorced { get; } = new(4, "Divorced");
    public static MaritalStatus FactoUnion { get; } = new(5, "Facto Union");
    public static MaritalStatus LegallySeparated { get; } = new(6, "Legally Separated");

    /// <summary>
    /// Default marital status option (to display before input).
    /// </summary>
    public static MaritalStatus DefaultOption { get; } = Single;

    /// <summary>
    /// All marital status options.
    /// </summary>
    public static MaritalStatus[] AllOptions { get; } =
    [
        Single, Married, Widower, Divorced, FactoUnion, LegallySeparated
    ];
}
