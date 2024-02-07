using System;

namespace MyDataStructures
{
    public class Trie
    {
        public TrieNode root;

        public Trie()
        {
            this.root = new TrieNode('.');
        }

        public void add(string word)
        {
            this.addWord(word, this.root);
        }

        public (string, TrieNode) predict(string word)
        {
            string prediction = string.Empty;
            TrieNode node = this.root;
            foreach (char letter in word)
            {
                if (node.children.ContainsKey(letter))
                {
                    node = node.children[letter];
                    prediction += node.letter;
                }
                else
                {
                    return (string.Empty, new TrieNode('.'));
                }
            }
            return (prediction, node);

        }

        public void reconstructWords(TrieNode node, string baseWord, List<string> words)
        {
            if (node.isWordEnd)
            {
                words.Add(baseWord);
            }
            if (node.children.Count == 0)
            {
                return;
            }
            foreach (KeyValuePair<char, TrieNode> child in node.children)
            {
                string newBaseWord = baseWord + child.Value.letter;
                this.reconstructWords(child.Value, newBaseWord, words);
            }
        }


        private void addWord(string word, TrieNode node)
        {
            if (word.Equals(string.Empty))
            {
                node.isWordEnd = true;
            }
            else
            {
                char letter = word[0];
                if (node.children.ContainsKey(letter))
                {
                    node.children[letter].count++;
                    this.addWord(word.Substring(1), node.children[letter]);
                }
                else
                {
                    TrieNode newNode = new TrieNode(letter);
                    node.children.Add(letter, newNode);
                    this.addWord(word.Substring(1), newNode);
                }
            }
        }
    }

    public class TrieNode
    {
        public char letter;
        public Dictionary<char, TrieNode> children;
        public uint count;
        public bool isWordEnd;

        public TrieNode(char letter)
        {
            this.letter = letter;
            this.children = new Dictionary<char, TrieNode>();
            this.count = 1;
            this.isWordEnd = false;
        }
    }

}
