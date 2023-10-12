using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Чтение текста из файла с кодировкой UTF-8
        string filePath = "input.txt";
        string text = File.ReadAllText(filePath, Encoding.UTF8);
        int sum = 0;
        // Убрать знаки препинания и перевести в нижний регистр
        string cleanedText = Regex.Replace(text, @"[^\p{L}\s]+", "").ToLower();

        // Разделить текст на слова
        string[] words = cleanedText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Создать список для хранения уникальных слов и их количества
        List<WordCount> wordCounts = new List<WordCount>();

        foreach (string word in words)
        {
            // Пропустить слова, состоящие из одного символа
            if (word.Length > 1)
            {
                // Поиск слова в списке wordCounts
                WordCount existingWord = wordCounts.FirstOrDefault(w => w.Word == word);

                if (existingWord != null)
                {
                    // Увеличить количество повторений, если слово уже существует
                    existingWord.Count++;
                }
                else
                {
                    // Добавить новое слово в список
                    wordCounts.Add(new WordCount { Word = word, Count = 1 });
                }
            }
        }

        // Отсортировать слова в алфавитном порядке
        wordCounts = wordCounts.OrderBy(w => w.Word).ToList();

        // Создать или перезаписать файл "output.txt" и записать результаты в него
        using (StreamWriter writer = new StreamWriter("output.txt", false, Encoding.UTF8))
        {
            foreach (WordCount wc in wordCounts)
            {
                writer.WriteLine($"{wc.Word}: {wc.Count}");
                sum += wc.Count; 
            }
            writer.WriteLine($"Всего слов:{sum}");
        }

        Console.WriteLine("Результаты записаны в файл 'output.txt'.");
    }

    class WordCount
    {
        public string Word { get; set; }
        public int Count { get; set; }
    }
}