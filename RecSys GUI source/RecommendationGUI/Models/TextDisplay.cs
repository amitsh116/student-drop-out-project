namespace RecommendationGUI.Models;

/// <summary>
/// `IDisplayable` of simple text.
/// </summary>
/// <param name="text">Text content to display.</param>
public class TextDisplay(string text) : IDisplayable
{
    /// <summary>
    /// Text content.
    /// </summary>
    public string Text { get; } = text;

    public string Display => Text;
}
