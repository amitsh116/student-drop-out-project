namespace RecommendationGUI.Models;

/// <summary>
/// Represents an object that can be converted to a representative number.
/// </summary>
public interface INumConvertible
{
    /// <summary>
    /// Converts object to its representative number.
    /// </summary>
    /// <returns>Object's representative number.</returns>
    int ToNum();
}
