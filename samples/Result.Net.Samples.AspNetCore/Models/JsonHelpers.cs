using ResultNet;
using System.Text.Json.Serialization.Metadata;

namespace Result.Net.Samples.AspNetCore
{
    public class JsonHelpers
    {
        /// <summary>
        /// ignore the exception prop on the <see cref="ResultError"/> type
        /// </summary>
        /// <param name="typeInfo">the json type info</param>
        public static void IgnoreResultErrorExceptionProp(JsonTypeInfo typeInfo)
        {
            if (typeInfo.Type != typeof(ResultError)) return;

            var properties = typeInfo.Properties
                .Where(propertyInfo => 
                    propertyInfo.Name.Equals(nameof(ResultError.IsExceptionError), StringComparison.InvariantCultureIgnoreCase) ||
                    propertyInfo.Name.Equals(nameof(ResultError.MetaData), StringComparison.InvariantCultureIgnoreCase)
                );

            foreach (JsonPropertyInfo propertyInfo in properties)
                propertyInfo.ShouldSerialize = static (obj, value) => false;
        }
    }
}
