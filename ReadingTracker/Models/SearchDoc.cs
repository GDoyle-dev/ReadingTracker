using System.Text.Json.Serialization;

namespace ReadingTracker.Models
{
    internal class SearchDoc
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("author_name")]
        public List<string>? AuthorName { get; set; }

        [JsonPropertyName("first_publish_year")]
        public int? FirstPublishYear { get; set; }

        [JsonPropertyName("cover_i")]
        public int? CoverId { get; set; }

        [JsonPropertyName("edition_count")]
        public int? EditionCount { get; set; }


        [JsonIgnore]
        public string DisplayTitle
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Title))
                    return "Untitled";
                return Title;
            }
        }

        [JsonIgnore]
        public string DisplayAuthors
        {
            get
            {
                if (AuthorName != null && AuthorName.Count > 0)
                    return string.Join(", ", AuthorName);
                return "Unknown author";
            }
        }

        [JsonIgnore]
        public string DisplayYear
        {
            get
            {
                if (FirstPublishYear != null)
                    return FirstPublishYear.ToString();
                return "Year unknown";
            }
        }

        [JsonIgnore]
        public string DisplayEditions
        {
            get
            {
                if (EditionCount != null)
                    return $"Editions: {EditionCount}";
                return "Editions: unknown";
            }
        }

        [JsonIgnore]
        public string? CoverUrl
        {
            get
            {
                if (CoverId != null)
                    return $"https://covers.openlibrary.org/b/id/{CoverId}-M.jpg";
                return null;
            }
        }
    }
}
