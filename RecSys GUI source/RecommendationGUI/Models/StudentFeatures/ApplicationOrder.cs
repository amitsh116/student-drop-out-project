namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Represents student's application order.
/// </summary>
public class ApplicationOrder : CategorialBase
{
    /// <summary>
    /// Constructor of `ApplicationOrder`.
    /// </summary>
    /// <param name="id">Representative numeric ID.</param>
    /// <param name="name">Display name.</param>
    protected ApplicationOrder(int id, string name) : base(id, name) { }

    public static ApplicationOrder FirstChoice { get; } = new(0, "First Choice");
    public static ApplicationOrder SecondChoice { get; } = new(1, "Second Choice");
    public static ApplicationOrder ThirdChoice { get; } = new(2, "Third Choice");
    public static ApplicationOrder FourthChoice { get; } = new(3, "Fourth Choice");
    public static ApplicationOrder FifthChoice { get; } = new(4, "Fifth Choice");
    public static ApplicationOrder SixthChoice { get; } = new(5, "Sixth Choice");
    public static ApplicationOrder SeventhChoice { get; } = new(6, "Seventh Choice");
    public static ApplicationOrder EighthChoice { get; } = new(7, "Eighth Choice");
    public static ApplicationOrder NinthChoice { get; } = new(8, "Ninth Choice");
    public static ApplicationOrder LastChoice { get; } = new(9, "Last Choice");

    /// <summary>
    /// Default application order option (to display before input).
    /// </summary>
    public static ApplicationOrder DefaultOption { get; } = FirstChoice;

    /// <summary>
    /// All application order options.
    /// </summary>
    public static ApplicationOrder[] AllOptions { get; } =
    [
        FirstChoice, SecondChoice, ThirdChoice, FourthChoice, FifthChoice,
        SixthChoice, SeventhChoice, EighthChoice, NinthChoice, LastChoice
    ];
}
