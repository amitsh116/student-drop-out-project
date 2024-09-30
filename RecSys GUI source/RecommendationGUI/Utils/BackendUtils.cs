using RecommendationGUI.BackEnd;

namespace RecommendationGUI.Utils;

/// <summary>
/// Static class used for storing IBackEnd instancing in one place.
/// </summary>
public static class BackendUtils
{
    /// <summary>
    /// `IBackEnd` instance used by application.
    /// </summary>
    private static readonly IBackEnd _backend = new PythonBackEnd();

    /// <summary>
    /// Retreives `IBackEnd` instance used by application.
    /// </summary>
    /// <returns>`IBackEnd` instance used by application.</returns>
    public static IBackEnd RetreiveBackEnd() => _backend;
}
