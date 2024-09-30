using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;

namespace RecommendationGUI.Utils
{
    /// <summary>
    /// Class used for deserializing a pair form JSON.
    /// </summary>
    /// <typeparam name="T1">Pair first element type.</typeparam>
    /// <typeparam name="T2">Pair second element type.</typeparam>
    internal class PairJsonConverter<T1, T2> : JsonConverter<Tuple<T1, T2>>
    {
        public override Tuple<T1, T2>? ReadJson(JsonReader reader, Type objectType, Tuple<T1, T2>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // parse JSON array
            var array = JArray.Load(reader);

            if (array.Count != 2)
            {
                throw new JsonSerializationException("Expected an array of two elements to deserialize into a Tuple.");
            }

            // deserialize each element of the array, handling nulls for reference types and nullable value types
            var item1 = array[0].Type == JTokenType.Null ? default : array[0].ToObject<T1>(serializer);
            var item2 = array[1].Type == JTokenType.Null ? default : array[1].ToObject<T2>(serializer);

            return Tuple.Create(item1, item2)!; // we know each item is not null by prev lines
        }

        public override void WriteJson(JsonWriter writer, Tuple<T1, T2>? value, JsonSerializer serializer)
        {
            // start writing JSON array for tuple
            writer.WriteStartArray();

            // handle null values explicitly for Item1 and Item2
            if (value is null)
            {
                // serialize entire tuple as null
                writer.WriteNull();
                writer.WriteNull();
            }
            else
            {
                serializer.Serialize(writer, value.Item1); // serialize Item1
                serializer.Serialize(writer, value.Item2); // serialize Item2
            }

            writer.WriteEndArray();
        }
    }
}
