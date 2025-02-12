using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexConnector.Core.Converters
{
    public class DecimalFormatConverter : JsonConverter
    {

        private readonly int _precision;
        public DecimalFormatConverter(int precision) => _precision = precision;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var d = (decimal)value;
            writer.WriteValue(Math.Round(d, _precision));
        }
    }
}
