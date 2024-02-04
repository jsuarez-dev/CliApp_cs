
using System;
using System.IO;
using MyDataStructures;

namespace CliApp
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0 || args[0] == null)
            {
                Console.WriteLine("Please provide a file name");
                return 1;
            }
            string fileName = args[0];

            TextFile textFile = new TextFile(fileName);

            return 0;
        }
    }

    class TextFile
    {
        public string[]? words;
        public string fileName { get; set; }
        public string? fileContent { get; set; }
        private string filePath;
        public Trie trie;

        public TextFile(string fileName)
        {
            this.fileName = fileName;
            this.filePath = Path.GetFullPath(fileName);
            if (File.Exists(this.filePath))
            {
                this.fileContent = File.ReadAllText(this.filePath);
                this.getWords();
            }
            else
            {
                Console.WriteLine($"File {this.filePath} does not exist");
            }
            this.trie = new Trie();
        }

        public void addWordsToTrie()
        {
            if (this.words != null)
            {
                foreach (string word in this.words)
                {
                    this.trie.add(word);
                }
            }
        }

        private void getWords()
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '[', ']', '\n', '\r', '\'', '\"', '(', ')', '{', '}' };
            if (fileContent != null)
            {
                string fileContentFiltered = fileContent.Replace(@"\[\d+\]", "").ToLower();
                this.words = fileContentFiltered.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            }
        }


        public void PrintContent()
        {
            if (fileContent != null)
            {
                Console.WriteLine(fileContent);
            }
        }
    }
}
