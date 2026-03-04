using System.Collections.Generic;
using System.Linq;

namespace BlazorWordList.Features.SpellCheck
{
    public class SpellCheckService
    {
        private HashSet<string> _dictionary = new();
        private bool _isLoaded = false;

        public bool IsLoaded => _isLoaded;

        public void Initialize(HashSet<string> dictionary)
        {
            _dictionary = dictionary;
            _isLoaded = true;
        }

        public bool IsWordCorrect(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return true;
            
            // Skip English/Alphanumeric words
            if (word.All(c => c < 128)) return true;

            return _dictionary.Contains(word);
        }

        public List<string> GetSuggestions(string word, int maxSuggestions = 5)
        {
            if (string.IsNullOrWhiteSpace(word)) return new List<string>();

            // Simple Levenshtein-based search
            // In a real high-performance app, we might use a Trie or BK-Tree
            // But for a dictionary of ~20k-50k words, this might be OK for WASM if optimized
            
            return _dictionary
                .Select(d => new { Word = d, Distance = CalculateLevenshteinDistance(word, d) })
                .Where(x => x.Distance <= 2) // Only suggest words with distance 1 or 2
                .OrderBy(x => x.Distance)
                .ThenBy(x => x.Word.Length)
                .Take(maxSuggestions)
                .Select(x => x.Word)
                .ToList();
        }

        private int CalculateLevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source)) return target.Length;
            if (string.IsNullOrEmpty(target)) return source.Length;

            int n = source.Length;
            int m = target.Length;
            int[,] d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
                    d[i, j] = System.Math.Min(
                        System.Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }
    }
}
