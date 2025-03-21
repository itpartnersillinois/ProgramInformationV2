using System.Text.Json.Serialization;

namespace ProgramInformationV2.Search.Models {
    public class GenericItem {

        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("isactive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = "";
    }
}
