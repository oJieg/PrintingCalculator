using System.Text.Json.Serialization;

namespace printing_calculator.Clients.DTO
{
    public class FieldsDetailUpdate
    {
        public double opportunity {  get; set; }
        public string title { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> DynamicFields { get; set; }
    }
}
