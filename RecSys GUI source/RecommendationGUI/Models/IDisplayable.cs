namespace RecommendationGUI.Models;

/// <summary>
/// Represents an object that can be displayed in textual form.
/// </summary>
public interface IDisplayable
{
    /// <summary>
    /// Textual form of object.
    /// </summary>
    string Display { get; }
}
