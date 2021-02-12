#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 126. Word Ladder II
// URL: https://leetcode.com/problems/word-ladder-ii/

namespace LeetCode
{
    public class Graph
    {
        public IDictionary<int, IList<int>> AdjacencyList { get; } = new Dictionary<int, IList<int>>();

        public void AddVertex(int vertex)
        {
            AdjacencyList[vertex] = new List<int>();
        }

        public void AddUndirectedEdge(int v1, int v2)
        {
            AdjacencyList[v1].Add(v2);
            AdjacencyList[v2].Add(v1);
        }
    }

    public class Solution
    {
        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
//            return FindLaddersDFSBacktracking(beginWord, endWord, wordList);
//            return FindLaddersBFS(beginWord, endWord, wordList);
            return FindLaddersBFS2(beginWord, endWord, wordList);
        }

        public IList<IList<string>> FindLaddersBFS(string beginWord, string endWord, IList<string> wordList)
        {
            var g = CreateGraph(beginWord, wordList);

            IList<IList<int>> results = new List<IList<int>>();

            // SS: do BFS on graph
            // O(V + E)
            var q = new Queue<(int vertex, IList<int>, HashSet<int>)>();
            q.Enqueue((0, new List<int> {0}, new HashSet<int> {0}));

            var shortestDistance = -1;

            while (q.Any())
            {
                var (vertex, r, visited) = q.Dequeue();

                // SS: can we assume that the end word is always the last one in the list?
                if (vertex > 0 && wordList[vertex - 1] == endWord)
                {
                    if (shortestDistance == -1)
                    {
                        shortestDistance = r.Count;
                    }
                    else if (r.Count > shortestDistance)
                    {
                        // SS: Since we are doing BFS, as soon as we find a path longer than the
                        // shortest we have seen, we know that there can be no shorter or equally
                        // short paths.
                        break;
                    }

                    results.Add(r);
                    continue;
                }

                // SS: put all unvisited neighbors into the queue
                var adjList = g.AdjacencyList[vertex];
                for (var i = 0; i < adjList.Count; i++)
                {
                    var targetVertex = adjList[i];

                    if (visited.Contains(targetVertex))
                    {
                        continue;
                    }

                    var r2 = new List<int>(r) {targetVertex};
                    q.Enqueue((targetVertex, r2, new HashSet<int>(visited) {targetVertex}));
                }
            }

            // SS: convert from vertex to words
            IList<IList<string>> strResults = new List<IList<string>>();
            for (var i = 0; i < results.Count; i++)
            {
                var r = results[i];
                IList<string> rs = new List<string> {beginWord};
                for (var j = 1; j < r.Count; j++)
                {
                    var vertex = r[j] - 1;
                    var word = wordList[vertex];
                    rs.Add(word);
                }

                strResults.Add(rs);
            }

            return strResults;
        }

        public IList<IList<string>> FindLaddersBFS2(string beginWord, string endWord, IList<string> wordList)
        {
            // SS: BFS at O(V + E)
            // Creating the graph: O(V^2 * len(beginWord))
            // total runtime complexity: O(V^2 * len(beginWord))

            if (wordList.IndexOf(endWord) == -1)
            {
                return new List<IList<string>>();
            }

            var g = CreateGraphFast(beginWord, wordList);

            // SS: do BFS on graph
            var q = new Queue<int>();
            q.Enqueue(0);

            // SS: like in Djikstra's, to reconstruct the paths
            var parents = new Dictionary<int, HashSet<int>>
            {
                {0, new HashSet<int>()}
            };

            // SS: keep track of visited vertices and their distances
            var distance = new Dictionary<int, int>
            {
                {0, 0}
            };

            while (q.Any())
            {
                var vertex = q.Dequeue();

                // SS: can we assume that the end word is always the last one in the list?
                if (vertex > 0 && wordList[vertex - 1] == endWord)
                {
                    // SS: nothing to do
                    continue;
                }

                var adjList = g.AdjacencyList[vertex];
                for (var i = 0; i < adjList.Count; i++)
                {
                    var targetVertex = adjList[i];

                    if (distance.TryGetValue(targetVertex, out var d))
                    {
                        if (d < distance[vertex] + 1)
                        {
                            // SS: We have seen a shorter path through this vertex previously
                            continue;
                        }
                    }
                    else
                    {
                        distance[targetVertex] = distance[vertex] + 1;
                    }

                    // SS: keep track of parent
                    if (parents.TryGetValue(targetVertex, out var ps))
                    {
                        ps.Add(vertex);
                    }
                    else
                    {
                        parents[targetVertex] = new HashSet<int> {vertex};
                    }

                    q.Enqueue(targetVertex);
                }
            }

            // SS: convert from vertex to words
            var strResults = ReconstructPaths(parents, beginWord, endWord, wordList);
            return strResults;
        }

        private static IList<IList<string>> ReconstructPaths(Dictionary<int, HashSet<int>> parents, string beginWord, string endWord, IList<string> wordList)
        {
            IList<IList<string>> strResults = new List<IList<string>>();

            var endWordIdx = wordList.IndexOf(endWord);

            void Reconstruct(int wordIdx, IList<string> r)
            {
                // SS: begin word, i.e. start of path?
                if (wordIdx == 0)
                {
                    IList<string> item = new List<string>(r) {beginWord};
                    strResults.Add(item.Reverse().ToList());
                    return;
                }

                var word = wordList[wordIdx - 1];
                r.Add(word);

                if (parents.TryGetValue(wordIdx, out var p))
                {
                    foreach (var vertex in p)
                    {
                        Reconstruct(vertex, r);
                    }
                }

                r.RemoveAt(r.Count - 1);
            }

            Reconstruct(endWordIdx + 1, new List<string>());
            return strResults;
        }

        private static Graph CreateGraphFast(string beginWord, IList<string> wordList)
        {
            var g = new Graph();

            // SS: add begin word to word list
            var words = new List<string> {beginWord};
            words.AddRange(wordList);

            // SS: runtime complexity: O(#words * len(word)^2)
            var hashCode = new Dictionary<int, IList<int>>();
            for (var i = 0; i < words.Count; i++)
            {
                g.AddVertex(i);

                var word = words[i];
                for (var j = 0; j < word.Length; j++)
                {
                    var n = 0;
                    var fac = 1;
                    for (var k = 0; k < word.Length; k++)
                    {
                        // SS: shift "digit" to left
                        if (k != j)
                        {
                            var c = word[k];
                            n += (c - 'a' + 1) * fac;
                        }

                        fac *= 27;
                    }

                    if (hashCode.TryGetValue(n, out var wordIndices) == false)
                    {
                        wordIndices = new List<int>();
                        hashCode[n] = wordIndices;
                    }

                    wordIndices.Add(i);
                }
            }

            // SS: add edges from start vertex
            // SS: runtime complexity: O(#words^2)
            foreach (var vertices in hashCode.Values)
            {
                for (var i = 0; i < vertices.Count; i++)
                {
                    var v1 = vertices[i];

                    for (var j = i + 1; j < vertices.Count; j++)
                    {
                        var v2 = vertices[j];
                        g.AddUndirectedEdge(v1, v2);
                    }
                }
            }

            return g;
        }

        private static Graph CreateGraph(string beginWord, IList<string> wordList)
        {
            var g = new Graph();
            g.AddVertex(0);

            // SS: add edges from start vertex
            // O(V)
            for (var i = 0; i < wordList.Count; i++)
            {
                g.AddVertex(i + 1);

                var word = wordList[i];
                if (MaxDifference(beginWord, word))
                {
                    g.AddUndirectedEdge(0, i + 1);
                }
            }

            // SS: connect all other vertices
            // O(V^2)
            for (var i = 0; i < wordList.Count; i++)
            {
                var word1 = wordList[i];

                for (var j = i + 1; j < wordList.Count; j++)
                {
                    var word2 = wordList[j];
                    if (MaxDifference(word1, word2))
                    {
                        g.AddUndirectedEdge(i + 1, j + 1);
                    }
                }
            }

            return g;
        }

        public IList<IList<string>> FindLaddersDFSBacktracking(string beginWord, string endWord, IList<string> wordList)
        {
            // SS: implicit graph problem, we do DFS and record all paths.
            // In a next step, the sort based on path length and only
            // return the shortest ones.

            IList<IList<string>> results = new List<IList<string>>();

            if (wordList.IndexOf(endWord) == -1)
            {
                // SS: end word not contained
                return results;
            }

            var visitedWords = new int[wordList.Count];

            void DFS(IList<string> result)
            {
                var currentWord = result[^1];

                // SS: test if we can transition to end word
                if (MaxDifference(currentWord, endWord))
                {
                    var tr = new List<string>(result) {endWord};
                    results.Add(tr);
                    return;
                }

                for (var i = 0; i < visitedWords.Length; i++)
                {
                    if (visitedWords[i] > 0)
                    {
                        continue;
                    }

                    var word = wordList[i];

                    if (MaxDifference(currentWord, word))
                    {
                        visitedWords[i] = 1;
                        result.Add(word);
                        DFS(result);

                        // SS: backtracking
                        result.RemoveAt(result.Count - 1);
                        visitedWords[i] = 0;
                    }
                }
            }

            var r = new List<string> {beginWord};
            DFS(r);

            if (results.Any())
            {
                // SS: all paths are in results now, sort in ascending order
                results = results.OrderBy(item => item.Count).ToList();

                // SS: only keep the ones that have the smallest number of elements
                var tmpResults = new List<IList<string>> {results[0]};
                var minLength = results[0].Count;

                var i = 1;
                while (i < results.Count && results[i].Count == minLength)
                {
                    tmpResults.Add(results[i]);
                    i++;
                }

                results = tmpResults;
            }

            return results;
        }

        private static bool MaxDifference(string word1, string word2)
        {
            var i = 0;
            var isDifferent = false;
            while (i < word1.Length)
            {
                if (word1[i] != word2[i])
                {
                    if (isDifferent)
                    {
                        return false;
                    }

                    isDifferent = true;
                }

                i++;
            }

            return isDifferent;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var beginWord = "hit";
                var endWord = "cog";
                var wordList = new[] {"hot", "dot", "dog", "lot", "log", "cog"};

                // Act
                var results = new Solution().FindLadders(beginWord, endWord, wordList);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {"hit", "hot", "dot", "dog", "cog"}, new[] {"hit", "hot", "lot", "log", "cog"}}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var beginWord = "hit";
                var endWord = "cog";
                var wordList = new[] {"hot", "dot", "dog", "lot", "log"};

                // Act
                var results = new Solution().FindLadders(beginWord, endWord, wordList);

                // Assert
                Assert.IsEmpty(results);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var beginWord = "qa";
                var endWord = "sq";
                var wordList = new[]
                {
                    "si", "go", "se", "cm", "so", "ph", "mt", "db", "mb", "sb", "kr", "ln", "tm", "le", "av", "sm", "ar", "ci", "ca", "br", "ti", "ba", "to", "ra", "fa", "yo", "ow", "sn", "ya", "cr"
                    , "po", "fe", "ho", "ma", "re", "or", "rn", "au", "ur", "rh", "sr", "tc", "lt", "lo", "as", "fr", "nb", "yb", "if", "pb", "ge", "th", "pm", "rb", "sh", "co", "ga", "li", "ha", "hz"
                    , "no", "bi", "di", "hi", "qa", "pi", "os", "uh", "wm", "an", "me", "mo", "na", "la", "st", "er", "sc", "ne", "mn", "mi", "am", "ex", "pt", "io", "be", "fm", "ta", "tb", "ni", "mr"
                    , "pa", "he", "lr", "sq", "ye"
                };

                // Act
                var results = new Solution().FindLadders(beginWord, endWord, wordList);

                // Assert
                Assert.AreEqual(51, results.Count);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var beginWord = "cet";
                var endWord = "ism";
                var wordList = new[]
                {
                    "kid", "tag", "pup", "ail", "tun", "woo", "erg", "luz", "brr", "gay", "sip", "kay", "per", "val", "mes", "ohs", "now", "boa", "cet", "pal", "bar", "die", "war", "hay", "eco", "pub"
                    , "lob", "rue", "fry", "lit", "rex", "jan", "cot", "bid", "ali", "pay", "col", "gum", "ger", "row", "won", "dan", "rum", "fad", "tut", "sag", "yip", "sui", "ark", "has", "zip"
                    , "fez", "own", "ump", "dis", "ads", "max", "jaw", "out", "btu", "ana", "gap", "cry", "led", "abe", "box", "ore", "pig", "fie", "toy", "fat", "cal", "lie", "noh", "sew", "ono"
                    , "tam", "flu", "mgm", "ply", "awe", "pry", "tit", "tie", "yet", "too", "tax", "jim", "san", "pan", "map", "ski", "ova", "wed", "non", "wac", "nut", "why", "bye", "lye", "oct"
                    , "old", "fin", "feb", "chi", "sap", "owl", "log", "tod", "dot", "bow", "fob", "for", "joe", "ivy", "fan", "age", "fax", "hip", "jib", "mel", "hus", "sob", "ifs", "tab", "ara"
                    , "dab", "jag", "jar", "arm", "lot", "tom", "sax", "tex", "yum", "pei", "wen", "wry", "ire", "irk", "far", "mew", "wit", "doe", "gas", "rte", "ian", "pot", "ask", "wag", "hag"
                    , "amy", "nag", "ron", "soy", "gin", "don", "tug", "fay", "vic", "boo", "nam", "ave", "buy", "sop", "but", "orb", "fen", "paw", "his", "sub", "bob", "yea", "oft", "inn", "rod"
                    , "yam", "pew", "web", "hod", "hun", "gyp", "wei", "wis", "rob", "gad", "pie", "mon", "dog", "bib", "rub", "ere", "dig", "era", "cat", "fox", "bee", "mod", "day", "apr", "vie"
                    , "nev", "jam", "pam", "new", "aye", "ani", "and", "ibm", "yap", "can", "pyx", "tar", "kin", "fog", "hum", "pip", "cup", "dye", "lyx", "jog", "nun", "par", "wan", "fey", "bus"
                    , "oak", "bad", "ats", "set", "qom", "vat", "eat", "pus", "rev", "axe", "ion", "six", "ila", "lao", "mom", "mas", "pro", "few", "opt", "poe", "art", "ash", "oar", "cap", "lop"
                    , "may", "shy", "rid", "bat", "sum", "rim", "fee", "bmw", "sky", "maj", "hue", "thy", "ava", "rap", "den", "fla", "auk", "cox", "ibo", "hey", "saw", "vim", "sec", "ltd", "you"
                    , "its", "tat", "dew", "eva", "tog", "ram", "let", "see", "zit", "maw", "nix", "ate", "gig", "rep", "owe", "ind", "hog", "eve", "sam", "zoo", "any", "dow", "cod", "bed", "vet"
                    , "ham", "sis", "hex", "via", "fir", "nod", "mao", "aug", "mum", "hoe", "bah", "hal", "keg", "hew", "zed", "tow", "gog", "ass", "dem", "who", "bet", "gos", "son", "ear", "spy"
                    , "kit", "boy", "due", "sen", "oaf", "mix", "hep", "fur", "ada", "bin", "nil", "mia", "ewe", "hit", "fix", "sad", "rib", "eye", "hop", "haw", "wax", "mid", "tad", "ken", "wad"
                    , "rye", "pap", "bog", "gut", "ito", "woe", "our", "ado", "sin", "mad", "ray", "hon", "roy", "dip", "hen", "iva", "lug", "asp", "hui", "yak", "bay", "poi", "yep", "bun", "try"
                    , "lad", "elm", "nat", "wyo", "gym", "dug", "toe", "dee", "wig", "sly", "rip", "geo", "cog", "pas", "zen", "odd", "nan", "lay", "pod", "fit", "hem", "joy", "bum", "rio", "yon"
                    , "dec", "leg", "put", "sue", "dim", "pet", "yaw", "nub", "bit", "bur", "sid", "sun", "oil", "red", "doc", "moe", "caw", "eel", "dix", "cub", "end", "gem", "off", "yew", "hug"
                    , "pop", "tub", "sgt", "lid", "pun", "ton", "sol", "din", "yup", "jab", "pea", "bug", "gag", "mil", "jig", "hub", "low", "did", "tin", "get", "gte", "sox", "lei", "mig", "fig"
                    , "lon", "use", "ban", "flo", "nov", "jut", "bag", "mir", "sty", "lap", "two", "ins", "con", "ant", "net", "tux", "ode", "stu", "mug", "cad", "nap", "gun", "fop", "tot", "sow"
                    , "sal", "sic", "ted", "wot", "del", "imp", "cob", "way", "ann", "tan", "mci", "job", "wet", "ism", "err", "him", "all", "pad", "hah", "hie", "aim", "ike", "jed", "ego", "mac"
                    , "baa", "min", "com", "ill", "was", "cab", "ago", "ina", "big", "ilk", "gal", "tap", "duh", "ola", "ran", "lab", "top", "gob", "hot", "ora", "tia", "kip", "han", "met", "hut"
                    , "she", "sac", "fed", "goo", "tee", "ell", "not", "act", "gil", "rut", "ala", "ape", "rig", "cid", "god", "duo", "lin", "aid", "gel", "awl", "lag", "elf", "liz", "ref", "aha"
                    , "fib", "oho", "tho", "her", "nor", "ace", "adz", "fun", "ned", "coo", "win", "tao", "coy", "van", "man", "pit", "guy", "foe", "hid", "mai", "sup", "jay", "hob", "mow", "jot"
                    , "are", "pol", "arc", "lax", "aft", "alb", "len", "air", "pug", "pox", "vow", "got", "meg", "zoe", "amp", "ale", "bud", "gee", "pin", "dun", "pat", "ten", "mob"
                };

                // Act
                var results = new Solution().FindLadders(beginWord, endWord, wordList);

                // Assert
                CollectionAssert.AreEquivalent(new[]
                {
                    new[] {"cet", "cot", "con", "ion", "inn", "ins", "its", "ito", "ibo", "ibm", "ism"}
                    , new[] {"cet", "cat", "can", "ian", "inn", "ins", "its", "ito", "ibo", "ibm", "ism"}
                    , new[] {"cet", "get", "gee", "gte", "ate", "ats", "its", "ito", "ibo", "ibm", "ism"}
                }, results);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var beginWord = "hot";
                var endWord = "dog";
                var wordList = new[] {"hot", "dog"};

                // Act
                var results = new Solution().FindLadders(beginWord, endWord, wordList);

                // Assert
                Assert.IsEmpty(results);
            }
        }
    }
}