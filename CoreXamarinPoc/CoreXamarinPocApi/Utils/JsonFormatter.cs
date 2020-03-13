using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace poc.providers.api.Utils
{
    public static class JsonFormatter
    {
        public static string Serialize<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public static JObject SerializeJObject<T>(T entity)
        {
            return JObject.FromObject(entity, new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        /// <summary>
        /// Deserialize the specified text.
        /// </summary>
        /// <returns>The deserialize.</returns>
        /// <param name="text">Text.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Deserialize<T>(string text)
        {
            T deserializedObject = JsonConvert.DeserializeObject<T>(text, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None
            });
            return deserializedObject;
        }
    }
}
