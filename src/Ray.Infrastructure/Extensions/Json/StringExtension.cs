using System.IO;
using Newtonsoft.Json;

namespace Ray.Infrastructure.Extensions.Json
{
    public static class StringExtension
    {
        public static T JsonDeserialize<T>(this string str, bool useSystem = true)
        {
            return useSystem
                ? System.Text.Json.JsonSerializer.Deserialize<T>(str)
                : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>json格式化</summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string AsFormatJsonStr(this string str)
        {
            var jsonSerializer = new JsonSerializer();
            var jsonTextReader = new JsonTextReader((TextReader)new StringReader(str));
            object obj = jsonSerializer.Deserialize((JsonReader)jsonTextReader);
            if (obj == null) return str;
            var stringWriter = new StringWriter();
            var jsonTextWriter1 = new JsonTextWriter((TextWriter)stringWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            JsonTextWriter jsonTextWriter2 = jsonTextWriter1;
            jsonSerializer.Serialize((JsonWriter)jsonTextWriter2, obj);
            return stringWriter.ToString();
        }
    }
}
