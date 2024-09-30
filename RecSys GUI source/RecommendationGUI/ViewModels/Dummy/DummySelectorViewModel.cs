using RecommendationGUI.Models;

namespace RecommendationGUI.ViewModels.Dummy;

/// <summary>
/// Dummy version of `SelectorViewModel`.
/// </summary>
public class DummySelectorViewModel : SelectorViewModel<TextDisplay>
{
    /// <summary>
    /// Dummy selector option `A`.
    /// </summary>
    private static TextDisplay A { get; } = new("a");

    /// <summary>
    /// Dummy selector option `B`.
    /// </summary>
    private static TextDisplay B { get; } = new("b");

    /// <summary>
    /// Dummy selector option `C`.
    /// </summary>
    private static TextDisplay C { get; } = new("c");

    /// <summary>
    /// Constructs a new `DummySelectorViewModel` with dummy options `A`, `B`, `C`.
    /// </summary>
    public DummySelectorViewModel() : base([ A, B, C ], A) { }
}
