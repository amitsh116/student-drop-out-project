namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Represents a binary categorial feature (yes/no).
/// </summary>
public class YesNo : CategorialBase
{
    /// <summary>
    /// Constructor of `YesNo`.
    /// </summary>
    /// <param name="id">Representative numeric ID.</param>
    /// <param name="name">Display name.</param>
    protected YesNo(int id, string name) : base(id, name) { }

    /// <summary>
    /// `Yes` option.
    /// </summary>
    public static YesNo Yes { get; } = new(1, "Yes");

    /// <summary>
    /// `No` option.
    /// </summary>
    public static YesNo No { get; } = new(0, "No");

    /// <summary>
    /// All `YesNo` options (`Yes`, `No`).
    /// </summary>
    public static YesNo[] AllOptions { get; } = [Yes, No];
}
