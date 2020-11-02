#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// 10. Regular Expression Matching
// https://leetcode.com/problems/regular-expression-matching/

namespace L10
{
    public class Solution
    {
        public bool IsMatch(string s, string p)
        {
            if (s.Length == 0 && p.Length == 0)
            {
                return true;
            }

            var queue = new Queue<State>();

            var startState = new State
            {
                TransitionState = StateEnums.UNDEF
                , Char = (char) 0
                , POffset = 0
                , SOffset = 0
            };
            queue.Enqueue(startState);

            while (queue.Any())
            {
                var state = queue.Dequeue();
                if (state.TransitionState == StateEnums.UNDEF)
                {
                    var newState = CreateStateTransition(state, s, p);
                    queue.Enqueue(newState);
                    continue;
                }

                // pattern exhausted?
                if (state.POffset == p.Length)
                {
                    if (state.SOffset == s.Length)
                    {
                        return true;
                    }
                    continue;
                }
                
                if ((state.TransitionState & StateEnums.SUCCESS) > 0)
                {
                    return true;
                }

                if ((state.TransitionState & StateEnums.FAIL) > 0)
                {
                    continue;
                }
                
                // match a character in the input array s
                if ((state.TransitionState & StateEnums.KLEENE) > 0)
                {
                    // Kleene operator, so we need two states, one that loops and consumes a character
                    // of the input array s and another that skips the Kleene operator
                    var newState = new State
                    {
                        TransitionState = StateEnums.UNDEF
                        , Char = (char) 0
                        , SOffset = state.SOffset
                        , POffset = state.POffset + 2
                    };
                    queue.Enqueue(newState);
                }

                if (state.SOffset < s.Length)
                {
                    var c = s[state.SOffset];
                    if ((state.TransitionState & StateEnums.ANY) > 0 || state.Char == c)
                    {
                        var newState = new State
                        {
                            TransitionState = StateEnums.UNDEF
                            , POffset = state.POffset
                            , SOffset = state.SOffset + 1
                        };

                        if ((state.TransitionState & StateEnums.KLEENE) == 0)
                        {
                            newState.POffset++;
                        }

                        queue.Enqueue(newState);
                    }
                }
            }

            return false;
        }

        private State CreateStateTransition(State state, string s, string p)
        {
            if (state.POffset == p.Length)
            {
                var newState = new State
                {
                    TransitionState = state.SOffset == s.Length ? StateEnums.SUCCESS : StateEnums.FAIL
                };
                return newState;
            }

            if (state.TransitionState == StateEnums.UNDEF)
            {
                // transition to start state
                var (stateEnum, c, po) = NextChar(p, state.POffset);

                var newState = new State
                {
                    TransitionState = stateEnum
                    , Char = c
                    , SOffset = state.SOffset
                    , POffset = state.POffset
                };
                return newState;
            }

            throw new InvalidOperationException();
        }

        private (StateEnums, char, int) NextChar(string p, int pOffset)
        {
            if (pOffset == p.Length)
            {
                throw new ArgumentException();
            }

            StateEnums e = 0;

            var c = p[pOffset];
            var po = 1;

            if (c == '.')
            {
                e |= StateEnums.ANY;
            }

            if (pOffset <= p.Length - 2)
            {
                if (p[pOffset + 1] == '*')
                {
                    e |= StateEnums.KLEENE;
                    po++;
                }
            }

            return (e, c, po);
        }

        private struct State
        {
            public StateEnums TransitionState;
            public char Char;
            public int POffset;
            public int SOffset;
        }

        [Flags]
        private enum StateEnums
        {
            UNDEF = 1
            , ANY = 2
            , KLEENE = 4
            , SUCCESS = 8
            , FAIL = 16
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "aa";
                var p = "a";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(isMatch);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "aa";
                var p = "a*";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "ab";
                var p = ".*";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "aab";
                var p = "c*a*b";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "mississippi";
                var p = "mis*is*p*.";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(isMatch);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = "mississippi";
                var p = "mis*is*ip*.";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var s = "";
                var p = ".*";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }
 
            [Test]
            public void Test8()
            {
                // Arrange
                var s = "a";
                var p = "";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(isMatch);
            }

        }
    }
}