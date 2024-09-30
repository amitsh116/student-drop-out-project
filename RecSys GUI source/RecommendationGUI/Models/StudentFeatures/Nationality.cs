namespace RecommendationGUI.Models.StudentFeatures;

/// <summary>
/// Represents student's nationality.
/// </summary>
public class Nationality : CategorialBase
{
    /// <summary>
    /// Constructor of `Nationality`.
    /// </summary>
    /// <param name="id">Representative numeric ID.</param>
    /// <param name="name">Display name.</param>
    protected Nationality(int id, string name) : base(id, name) { }

    public static Nationality Portuguese { get; } = new(1, "Portuguese");
    public static Nationality German { get; } = new(2, "German");
    public static Nationality Spanish { get; } = new(6, "Spanish");
    public static Nationality Italian { get; } = new(11, "Italian");
    public static Nationality Dutch { get; } = new(13, "Dutch");
    public static Nationality English { get; } = new(14, "English");
    public static Nationality Lithuanian { get; } = new(17, "Lithuanian");
    public static Nationality Angolan { get; } = new(21, "Angolan");
    public static Nationality CapeVerdean { get; } = new(22, "Cape Verdean");
    public static Nationality Guinean { get; } = new(24, "Guinean");
    public static Nationality Mozambican { get; } = new(25, "Mozambican");
    public static Nationality Santomean { get; } = new(26, "Santomean");
    public static Nationality Turkish { get; } = new(32, "Turkish");
    public static Nationality Brazilian { get; } = new(41, "Brazilian");
    public static Nationality Romanian { get; } = new(62, "Romanian");
    public static Nationality Moldovan { get; } = new(100, "Moldova (Republic of)");
    public static Nationality Mexican { get; } = new(101, "Mexican");
    public static Nationality Ukrainian { get; } = new(103, "Ukrainian");
    public static Nationality Russian { get; } = new(105, "Russian");
    public static Nationality Cuban { get; } = new(108, "Cuban");
    public static Nationality Colombian { get; } = new(109, "Colombian");

    /// <summary>
    /// Default nationality option (to display before input).
    /// </summary>
    public static Nationality DefaultOption { get; } = English;

    /// <summary>
    /// All nationality options.
    /// </summary>
    public static Nationality[] AllOptions { get; } =
    [
        Portuguese, German, Spanish, Italian, Dutch, English, Lithuanian, Angolan, CapeVerdean, Guinean, Mozambican,
        Santomean, Turkish, Brazilian, Romanian, Moldovan, Mexican, Ukrainian, Russian, Cuban, Colombian
    ];
}
