using System.Text.Json.Serialization;

namespace ReadingTracker.Models
{
    internal class SearchResponse
    {
        [JsonPropertyName("numFound")]
        public int NumFound { get; set; }

        [JsonPropertyName("docs")]
        public List<SearchDoc>? Docs { get; set; }
    }
}
