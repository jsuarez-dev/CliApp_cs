using cli.Structures;

namespace cli.test;

public class UnitTest1
{
    [Fact]
    public void TriePrediction()
    {
        Trie trie = new Trie();
        trie.add("car");
        trie.add("carbone");
        (var pred, var node) = trie.predict("ca");

        Assert.Equal("ca", pred);
        Assert.Equal((uint)2, node.count);
    }

    [Fact]
    public void TrieReconstructWords()
    {
        Trie trie = new Trie();
        trie.add("car");
        trie.add("carbone");
        List<Tuple<string, uint>> words = new List<Tuple<string, uint>>();
        trie.reconstructWords(trie.root, string.Empty, words);

        Assert.Equal(2, words.Count);
        Assert.Equal("car", words[0].Item1);
        Assert.Equal((uint)2, words[0].Item2);
        Assert.Equal("carbone", words[1].Item1);
        Assert.Equal((uint)1, words[1].Item2);
    }
}
