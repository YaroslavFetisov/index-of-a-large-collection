using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            // Зчитування вхідного тексту з файлу
            string inputPath = "D:\\Collection";
            string inputText = File.ReadAllText(inputPath);

            // Розбиття вхідного тексту на окремі слова
            string[] words = Regex.Split(inputText, @"\W+");

            // Створення інвертованого індексу
            Dictionary<string, List<int>> invertedIndex = new Dictionary<string, List<int>>();
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i].ToLower();
                if (!invertedIndex.ContainsKey(word))
                {
                    invertedIndex[word] = new List<int>();
                }
                invertedIndex[word].Add(i);
            }

            // Зчитування запиту користувача
            Console.WriteLine("Enter your search query:");
            string queryString = Console.ReadLine();

            // Розбиття запиту на окремі слова
            string[] queryWords = Regex.Split(queryString, @"\W+");

            // Пошук відповідних індексів та виведення результатів
            List<int> results = null;
            foreach (string queryWord in queryWords)
            {
                string word = queryWord.ToLower();
                if (invertedIndex.ContainsKey(word))
                {
                    List<int> indices = invertedIndex[word];
                    if (results == null)
                    {
                        results = new List<int>(indices);
                    }
                    else
                    {
                        results = results.Intersect(indices).ToList();
                    }
                }
                else
                {
                    results = null;
                    break;
                }
            }

            // Виведення результатів
            if (results == null)
            {
                Console.WriteLine("No results found.");
            }
            else
            {
                Console.WriteLine($"Found {results.Count} results:");
                foreach (int resultIndex in results)
                {
                    Console.WriteLine(words[resultIndex]);
                }
            }

            // Виведення інформації про час та кількість результатів
            Console.WriteLine($"Search completed in {stopwatch.ElapsedMilliseconds*1000} s.");
            Console.ReadLine();
        }
    }
}
