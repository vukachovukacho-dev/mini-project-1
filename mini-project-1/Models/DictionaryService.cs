using System;
using System.Collections.Generic;
using System.Text;

namespace mini_project_1.Models
{
    public class DictionaryService
    {
        private readonly string _filePath;
        private readonly Dictionary<string, string> _words;

        public DictionaryService(string filePath)
        {
            _filePath = filePath;
            _words = new Dictionary<string, string>();
            LoadDictionary();
        }

        private void LoadDictionary()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, string.Empty, Encoding.UTF8);
                return;
            }

            string[] lines = File.ReadAllLines(_filePath, Encoding.UTF8);
            foreach (string line in lines)
            {
                if (line.Contains("="))
                {
                    string[] parts = line.Split(new[] { '=' }, 2);
                    string word = parts[0].Trim().ToLower();
                    string translation = parts[1].Trim();

                    if (!string.IsNullOrEmpty(word))
                    {
                        _words[word] = translation;
                    }
                }
            }
        }

        // Make sure this is marked 'public'
        public string Translate(string word)
        {
            string key = word.Trim().ToLower();
            return _words.TryGetValue(key, out string translation) ? translation : null;
        }

        // Make sure this is marked 'public'
        public void AddTranslation(string word, string translation)
        {
            string key = word.Trim().ToLower();
            string cleanTranslation = translation.Trim();

            _words[key] = cleanTranslation;

            string entry = $"{key}={cleanTranslation}{Environment.NewLine}";
            File.AppendAllText(_filePath, entry, Encoding.UTF8);
        }
    }
}

