using System.Text.Json;
using System.Text.Json.Serialization;

namespace ResultNet
{
    public class ResultConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(Result) ||
                   (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Result<>));
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(Result))
                return new BaseResultConverter<Result>();

            if (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Result<>))
            {
                Type elementType = typeToConvert.GetGenericArguments()[0];
                Type converterType = typeof(ResultGenericConverter<>).MakeGenericType(elementType);
                return (JsonConverter)Activator.CreateInstance(converterType)!;
            }

            throw new NotSupportedException($"Unsupported type {typeToConvert}");
        }

        private sealed class ResultGenericConverter<T> : BaseResultConverter<Result<T>>
        {
            protected override void WriteCustom(Utf8JsonWriter writer, Result<T> value, JsonSerializerOptions options)
            {
                if (value.Data is not null)
                {
                    writer.WritePropertyName("data");
                    JsonSerializer.Serialize(writer, value.Data, options);
                }
            }
        }

        private class BaseResultConverter<TResult> : JsonConverter<TResult> where TResult : Result
        {
            public override TResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, TResult value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                WriteCustom(writer, value, options);

                writer.WriteString("status", value.Status.ToString());

                if (!string.IsNullOrWhiteSpace(value.Message))
                    writer.WriteString("message", value.Message);

                if (!string.IsNullOrWhiteSpace(value.Code))
                    writer.WriteString("code", value.Code);

                if (!string.IsNullOrWhiteSpace(value.LogTraceCode))
                    writer.WriteString("logTraceCode", value.LogTraceCode);

                if (value.Errors?.Count > 0)
                {
                    writer.WritePropertyName("errors");
                    JsonSerializer.Serialize(writer, value.Errors, options);
                }

                if (value.MetaData?.Count > 0)
                {
                    writer.WritePropertyName("metaData");
                    JsonSerializer.Serialize(writer, value.MetaData, options);
                }

                writer.WriteEndObject();
            }

            protected virtual void WriteCustom(Utf8JsonWriter writer, TResult value, JsonSerializerOptions options) { }
        }
    }
}
