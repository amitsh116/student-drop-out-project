namespace RecommendationGUI.Models;

/// <summary>
/// Base class of categorial feature.
/// </summary>
public class CategorialBase : IDisplayable, INumConvertible
{
    /// <summary>
    /// Categorial feature's numeric ID.
    /// </summary>
    private readonly int _id;

    /// <summary>
    /// Categorial feature's display name.
    /// </summary>
    private readonly string _name;

    /// <summary>
    /// Constructor of `CategorialBase`
    /// </summary>
    /// <param name="id">Categorial feature's representative numeric ID.</param>
    /// <param name="name">Categorial feature's display name.</param>
    protected CategorialBase(int id, string name)
    {
        _id = id;
        _name = name;
    }

    public string Display => _name;

    public int ToNum() => _id;
}
