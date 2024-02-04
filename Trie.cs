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
