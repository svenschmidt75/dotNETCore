using NUnit.Framework;

namespace L301;

// Problem: 301. Remove Invalid Parentheses
// URL: https://leetcode.com/problems/remove-invalid-parentheses/

public class Solution
{
    public IList<string> RemoveInvalidParentheses(string s)
    {
        return RemoveInvalidParentheses1(s);
    }

    private IList<string> RemoveInvalidParentheses1(string s)
    {
        var solutions = new HashSet<string>();
        char[] partialSolution = new char[s.Length];
        
        void Solve(string s, int arrayPos, int parenthesisCount)
        {
            // SS: terminating condition
            if (s.Length == 0)
            {
                if (parenthesisCount == 0)
                {
                    var item = new string(partialSolution, 0, arrayPos);
                    solutions.Add(item);
                }

                return;
            }

            char c = s[0];

            if (c == '(')
            {
                // SS: include (
                partialSolution[arrayPos] = '(';
                Solve(s[1..], arrayPos + 1, parenthesisCount + 1);

                
                // SS: ignore (
                Solve(s[1..], arrayPos, parenthesisCount);
            }
            else if (c == ')')
            {
                // SS: include ) if we can
                if (parenthesisCount > 0)
                {
                    partialSolution[arrayPos] = ')';
                    Solve(s[1..], arrayPos + 1, parenthesisCount - 1);
                }
                
                // SS: ignore )
                Solve(s[1..], arrayPos, parenthesisCount);
            }
            else
            {
                // SS: include char
                partialSolution[arrayPos] = c;
                Solve(s[1..], arrayPos + 1, parenthesisCount);
            }
        }

        Solve(s, 0, 0);
        return solutions.GroupBy(k => k.Length).First().ToList();
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var s = "()())()";

            // Act
            var result = new Solution().RemoveInvalidParentheses(s);

            // Assert
            CollectionAssert.AreEquivalent(new[]{"(())()","()()()"}, result);
        }
    
        [Test]
        public void Test2()
        {
            // Arrange
            var s = "(a)())()";

            // Act
            var result = new Solution().RemoveInvalidParentheses(s);

            // Assert
            CollectionAssert.AreEquivalent(new[]{"(a())()","(a)()()"}, result);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var s = ")(";

            // Act
            var result = new Solution().RemoveInvalidParentheses(s);

            // Assert
            CollectionAssert.AreEquivalent(new[]{""}, result);
        }

    }
    
}

