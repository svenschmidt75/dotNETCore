using System.Linq;
using NUnit.Framework;

namespace Trie.Test
{
    [TestFixture]
    public class TrieTest
    {
        [Test]
        public void TestGetWords()
        {
            // Arrange
            var trie = new Trie();
            trie.Insert("are");
            trie.Insert("as");
            trie.Insert("dot");
            trie.Insert("news");
            trie.Insert("new");
            trie.Insert("not");
            trie.Insert("zen");

            // Act
            var result = trie.GetWords("new");

            // Assert
            Assert.AreEqual(2, result.Count());
            CollectionAssert.AreEquivalent(new[] {"new", "news"}, result);
        }

        [Test]
        public void TestInsert1()
        {
            // Arrange
            var trie = new Trie();

            // Act
            trie.Insert("word");

            // Assert
            Assert.True(trie.Find("word"));
        }

        [Test]
        public void TestInsert2()
        {
            // Arrange
            var trie = new Trie();

            // Act
            trie.Insert("news");
            trie.Insert("new");

            // Assert
            Assert.True(trie.Find("new"));
            Assert.True(trie.Find("news"));
        }
    }
}