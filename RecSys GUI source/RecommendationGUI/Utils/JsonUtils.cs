using Newtonsoft.Json;
using RecommendationGUI.BackEnd;

namespace RecommendationGUI.Utils;

/// <summary>
/// Static class holding JSON-related utilities.
/// </summary>
public static class JsonUtils
{
    private static readonly JsonSerializerSettings _settings;

    /// <summary>
    /// Initializes JSON utils (presets JSON serialization settings).
    /// </summary>
    static JsonUtils()
    {
        _settings = new();
        _settings.Converters.Add(new PairJsonConverter<CourseCliJsonOutput, bool>());
        _settings.Converters.Add(new PairJsonConverter<string, float>());

        // as of now, this app only uses pair of courseDict,bool and string,float.
        // add more pairs here if needed
    }

    /// <summary>
    /// Deserializes an object from JSON.
    /// </summary>
    /// <typeparam name="T">Type of object to desirealize.</typeparam>
    /// <param name="json">JSON string to deserialize from.</param>
    /// <returns>Deserialized object.</returns>
    public static T? DeserializeJson<T>(string json)
        => JsonConvert.DeserializeObject<T>(json, _settings);
}
