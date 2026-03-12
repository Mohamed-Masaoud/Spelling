using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Spelling.Sentences
{
    public class SentenceFromTextFile : ISentenceGetter
    {
        private static readonly List<string> _sentences = LoadSentences();

        private static List<string> LoadSentences()
        {
            string path = "Resources/sentences.txt";

            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show(
                        $"Sentence file not found:\n{path}",
                        "File Missing",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return new List<string>();
                }

                return File.ReadLines(path).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error loading sentences:\n{ex.Message}",
                    "Load Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return new List<string>();
            }
        }

        public string GetSentence()
        {
            if (_sentences.Count == 0)
                return "No sentences found.";

            return _sentences[Random.Shared.Next(_sentences.Count)];
        }
    }
}