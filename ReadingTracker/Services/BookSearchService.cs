using System.Text.Json;
using ReadingTracker.Models;

namespace ReadingTracker.Services
{
    internal class BookSearchService
    {
        private const string Fields = "key,title,author_name,first_publish_year,cover_i,edition_count";

        public async Task<List<SearchDoc>> SearchByTitleAsync(string title)
        {
            string url = $"https://openlibrary.org/search.json?title={Uri.EscapeDataString(title)}&fields={Fields}&limit=20";

            using var response = await App.Http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            SearchResponse? data = JsonSerializer.Deserialize<SearchResponse>(json);

            if (data == null || data.Docs == null)
                return new List<SearchDoc>();

            return data.Docs;
        }
    }
}
