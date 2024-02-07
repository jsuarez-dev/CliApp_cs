
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
            textFile.addWordsToTrie();


            Console.WriteLine("Word :");
            string? word = Console.ReadLine();

            if (word == null)
            {
                Console.WriteLine("Please provide a word");
                return 1;
            }

            string[] prediction = textFile.getPrediction(word);

            if (prediction.Length > 0)
            {
                foreach (string w in prediction)
                {
                    Console.WriteLine(w);
                }
            }

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
            return [];
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
