using System.Text.Json;


namespace Desktop_Warranty_TSJ.Help
{
    public static class JSONCamelCase
    {
        public static JsonElement SerializeWithCamelCase(string jsonContent)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            var document = JsonDocument.Parse(jsonContent);
            var element = document.RootElement;

            var jsonString = JsonSerializer.Serialize(element, options);

            var camelCaseDocument = JsonDocument.Parse(jsonString);
            return camelCaseDocument.RootElement.Clone();
        }

    }
}
