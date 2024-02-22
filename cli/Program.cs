
using System.Text.Json;
using cli.Structures;

namespace cli.App
{
    class Program
    {

        static int Main(string[] args)
        {
            CLI app = new CLI();
            app.Run();

            return 0;
        }
    }

    class CLI
    {
        private const uint MAX_PRESDICTION_SHOW = 20;
        private const string FILE_NAME = "data/dictionary_compact.json";
        public EnglishDictionary? myDic;
        private int[] wordPosition = [0, 0];
        private int lastPosition = 0;

        public CLI()
        {
            this.myDic = new EnglishDictionary(FILE_NAME);
        }

        public void Run()
        {
            if (myDic == null)
            {
                throw new NullReferenceException("Dictionary can not be null");
            }

            bool flag_continue = true;

            string word = "";

            Console.Clear();
            Console.WriteLine("Please provide a word:");

            string[] prediction = [];
            do
            {
                try
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
                        else if (keyInfo.Key == ConsoleKey.Tab)
                        {
                            if (prediction.Length > 0)
                            {
                                word = prediction[0];
                            }
                        }
                        else
                        {
                            word += keyInfo.Key.ToString().ToLower();
                        }
                        // clear the console
                        this.clearPredictions(prediction);
                    }
                    Console.WriteLine(word);
                    this.wordPosition[0] = word.Length;
                    this.wordPosition[1] = Console.CursorTop - 1;

                    prediction = myDic.getPrediction(word);
                    this.printPredictions(prediction);
                    this.lastPosition = Console.CursorTop;
                    Console.SetCursorPosition(this.wordPosition[0], this.wordPosition[1]);
                }
                catch (System.InvalidOperationException e)
                {
                    string? stdIn = Console.ReadLine();
                    if (stdIn != null)
                    {
                        word = stdIn;
                    }
                    else
                    {
                        throw new NullReferenceException("stdIn can not be null");
                    }
                    flag_continue = false;
                }

            } while (flag_continue);

            this.clearPredictions(prediction);
            string definition = myDic.getDefinition(word);
            Console.WriteLine($"Definition of {word}:");
            Console.WriteLine(definition);
        }

        private void printPredictions(string[] prediction)
        {
            if (prediction.Length > 0)
            {
                if (prediction.Length > MAX_PRESDICTION_SHOW)
                {
                    for (int i = 0; i < MAX_PRESDICTION_SHOW; i++)
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
        }

        private void clearPredictions(string[] predictions)
        {
            if (predictions.Length > 0)
            {
                Console.SetCursorPosition(0, this.lastPosition);
                if (predictions.Length > MAX_PRESDICTION_SHOW)
                {
                    for (int i = 0; i < MAX_PRESDICTION_SHOW + 1; i++)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                    }
                }
                else
                {
                    for (int i = 0; i < predictions.Length + 1; i++)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                    }
                }
            }
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
                    List<Tuple<string, uint>> words = new List<Tuple<string, uint>>();
                    this.trie.reconstructWords(node, prediction, words);
                    words.Sort((x, y) => y.Item2.CompareTo(x.Item2));
                    return words.Select(x => x.Item1).ToArray();
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
            if (this.dic.ContainsKey(word))
            {
                return this.dic[word];
            }
            else
            {
                return $"|--- {word} not in the Dictionary ---|";

            }
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
