using System.Text;
using mini_project_1.Models;

namespace mini_project_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;

            var languages = new Dictionary<string, LanguagePair>
            {
                { "1", new LanguagePair("Georgian - English", "ka_en.txt") },
                { "2", new LanguagePair("English - Georgian", "en_ka.txt") },
                { "3", new LanguagePair("Georgian - Russian", "ka_ru.txt") },
                { "4", new LanguagePair("Russian - Georgian", "ru_ka.txt") }
            };

            while (true)
            {
                Console.WriteLine("\n--- Translator ---");
                Console.WriteLine("Select a language pair:");

                foreach (var item in languages)
                {
                    Console.WriteLine($"{item.Key}. {item.Value.Name}");
                }
                Console.WriteLine("0. Exit");

                Console.Write("Your choice: ");
                string choice = Console.ReadLine()?.Trim();

                if (choice == "0")
                {
                    Console.WriteLine("Program exited.");
                    break;
                }

                if (string.IsNullOrEmpty(choice) || !languages.ContainsKey(choice))
                {
                    Console.WriteLine("Invalid choice, please try again.");
                    continue;
                }

                LanguagePair selectedLang = languages[choice];
                DictionaryService dictionaryService = new DictionaryService(selectedLang.FileName);

                Console.WriteLine($"\nSelected: {selectedLang.Name} (Type 'back' to return to the menu)");

                while (true)
                {
                    Console.Write("\nEnter a word or phrase: ");
                    string word = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(word))
                        continue;

                    if (word.ToLower() == "back")
                        break;

                    string translation = dictionaryService.Translate(word);

                    if (translation != null)
                    {
                        Console.WriteLine($"Translation: {translation}");
                    }
                    else
                    {
                        Console.WriteLine($"The word '{word}' was not found in the dictionary.");
                        Console.Write("Would you like to add its translation? (yes/no): ");
                        string addChoice = Console.ReadLine()?.Trim().ToLower();

                        if (addChoice == "yes" || addChoice == "y")
                        {
                            Console.Write($"Enter the translation for '{word}': ");
                            string newTranslation = Console.ReadLine()?.Trim();

                            if (!string.IsNullOrEmpty(newTranslation))
                            {
                                dictionaryService.AddTranslation(word, newTranslation);
                                Console.WriteLine("Successfully added to the dictionary!");
                            }
                        }
                    }
                }

            }
        }
    }
}
