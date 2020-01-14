using NUnit.Framework;

namespace Trie.Test
{
    [TestFixture]
    public class TrieTest
    {
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
    }
}