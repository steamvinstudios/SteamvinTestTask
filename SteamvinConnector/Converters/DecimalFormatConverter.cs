using System;
using Newtonsoft.Json;

namespace BitfinexConnector.Core.Converters
{
    public class DecimalFormatConverter : JsonConverter
    {
        private readonly int _precision;
        public DecimalFormatConverter(int precision) => _precision = precision;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(decimal?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return 0m;

            if (decimal.TryParse(reader.Value.ToString(), out decimal result))
                return Math.Round(result, _precision);
            return 0m;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var d = (decimal)value;
            writer.WriteValue(Math.Round(d, _precision));
        }
    }
}
