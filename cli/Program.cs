
using System.Text.Json;
using MyDataStructures;

namespace CliApp
{
    class Program
    {
        static int Main(string[] args)
        {
            string fileNameDictionary = "data/dictionary_compact.json";

            EnglishDictionary myDic = new EnglishDictionary(fileNameDictionary);
            bool flag_continue = true;

            string word = "";

            Console.Clear();
            Console.WriteLine("Please provide a word:");

            string[] prediction = [];
            do
            {
                if (!Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Enter || keyInfo.Key == ConsoleKey.Spacebar || keyInfo.Key == ConsoleKey.Escape)
                    {
                        flag_continue = false;
                    }
                    else if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        if (word.Length > 0)
                        {
                            word = word.Substring(0, word.Length - 1);
                        }
                    }
                    else
                    {
                        word += keyInfo.Key.ToString().ToLower();
                    }
                    // clear the console
                    if (prediction.Length > 0)
                    {
                        for (int i = 0; i < prediction.Length; i++)
                        {
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.WriteLine(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                        }
                    }
                }
                Console.WriteLine(word);

                prediction = myDic.getPrediction(word);

                if (prediction.Length > 0)
                {
                    if (prediction.Length > 10)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Console.WriteLine("-> " + prediction[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < prediction.Length; i++)
                        {
                            Console.WriteLine("-> " + prediction[i]);
                        }
                    }
                }
            } while (flag_continue);

            string definition = myDic.getDefinition(word);
            Console.WriteLine("Definition:");
            Console.WriteLine(definition);


            return 0;
        }
    }

    class EnglishDictionary
    {
        public Dictionary<string, string>? dic;
        private Trie trie;

        public EnglishDictionary(string fileName)
        {
            string filePath = Path.GetFullPath(fileName);
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                this.dic = JsonSerializer.Deserialize<Dictionary<string, string>>(text);
            }
            else
            {
                Console.WriteLine($"File {filePath} does not exist");
            }
            this.trie = new Trie();
            this.addWordsToTrie();
        }

        public string[] getPrediction(string word)
        {
            string prediction;
            TrieNode node;
            if (word != null)
            {
                (prediction, node) = this.trie.predict(word);
                if (prediction != string.Empty)
                {
                    List<string> words = new List<string>();
                    this.trie.reconstructWords(node, prediction, words);
                    return words.ToArray();
                }
            }
            return [""];
        }

        public string getDefinition(string word)
        {
            if (this.dic == null)
            {
                throw new NullReferenceException(" Dictionary can not be null");
            }
            return this.dic[word];
        }

        private void addWordsToTrie()
        {
            if (this.dic == null)
            {
                throw new NullReferenceException(" Dictionary can not be null");
            }
            List<string> words = new List<string>(this.dic.Keys);
            if (words.Count > 0)
            {
                foreach (string word in words)
                {
                    this.trie.add(word);
                }
            }
        }

    }
}
