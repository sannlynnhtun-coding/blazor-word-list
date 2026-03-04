using System.Net.Http.Json;

namespace BlazorWordList.Features.DictionaryLoader
{
    public class DictionaryLoaderService
    {
        private readonly HttpClient _httpClient;
        private readonly List<string> _availableFiles = new() 
        { 
            "data/က.list", "data/ခ.list", "data/ဂ.list", "data/ဃ.list", "data/င.list",
            "data/စ.list", "data/ဆ.list", "data/ဇ.list", "data/ဈ.list", "data/ည.list",
            "data/ဋ.list", "data/ဌ.list", "data/ဍ.list", "data/ဎ.list", "data/ဏ.list",
            "data/တ.list", "data/ထ.list", "data/ဒ.list", "data/ဓ.list", "data/န.list",
            "data/ပ.list", "data/ဖ.list", "data/ဗ.list", "data/ဘ.list", "data/မ.list",
            "data/ယ.list", "data/ရ.list", "data/လ.list", "data/ဝ.list", "data/သ.list",
            "data/ဟ.list", "data/ဠ.list", "data/အ.list"
        };

        private HashSet<string>? _cachedWords;

        public DictionaryLoaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HashSet<string>> LoadAllWordsAsync()
        {
            if (_cachedWords != null) return _cachedWords;

            var allWords = new HashSet<string>(StringComparer.Ordinal);
            
            foreach (var fileName in _availableFiles)
            {
                try 
                {
                    var content = await _httpClient.GetStringAsync(fileName);
                    var words = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words)
                    {
                        var trimmedWord = word.Trim();
                        if (!string.IsNullOrWhiteSpace(trimmedWord))
                        {
                            allWords.Add(trimmedWord);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading {fileName}: {ex.Message}");
                }
            }

            _cachedWords = allWords;
            return allWords;
        }

        public async Task<List<string>> SearchWordsAsync(string query, int maxResults = 50)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<string>();

            var allWords = await LoadAllWordsAsync();
            return allWords
                .Where(w => w.StartsWith(query, StringComparison.Ordinal))
                .OrderBy(w => w.Length)
                .ThenBy(w => w)
                .Take(maxResults)
                .ToList();
        }
    }
}
