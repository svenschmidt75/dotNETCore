using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace JohnProblem14
{
    public class GoogleProblem14
    {
        public int FindOneTimeIndex(bool[] input)
        {
            // SS: extract intervals
            int bestPriority = 0;
            int index = -1;

            // SS: O(n)
            int i = 0;
            while (i < input.Length)
            {
                if (input[i] == false)
                {
                    int j = i + 1;
                    while (j < input.Length && input[j] == false)
                    {
                        j++;
                    }

                    int priority;

                    // SS: special case when interval start at 0 or ends at end
                    if (j == input.Length)
                    {
                        priority = j - i;
                        if (priority > bestPriority)
                        {
                            bestPriority = priority;
                            index = j - 1;
                        }
                    } else if (i == 0)
                    {
                        priority = j;
                        if (priority > bestPriority)
                        {
                            bestPriority = priority;
                            index = i;
                        }
                    }
                    else
                    {
                        priority = Math.Max(1, (j - i) / 2);
                        if (priority > bestPriority)
                        {
                            bestPriority = priority;
                            index = i + priority;
                        }
                    }

                    // SS: a true follows after this false
                    i = j + 1;
                }
                else
                {
                    i++;
                }
            }
            
            return index;
        }
        
        
        public int FindOneTimeIndex2(bool[] input)
        {
            // SS: extract intervals
            (int priority, int start, int end) bestInterval = (0, -1, -1);

            // SS: O(n)
            int i = 0;
            while (i < input.Length)
            {
                if (input[i] == false)
                {
                    int j = i + 1;
                    while (j < input.Length && input[j] == false)
                    {
                        j++;
                    }

                    int priority;

                    // SS: special case when interval start at 0 or ends at end
                    if (j == input.Length)
                    {
                        priority = j - i;
                    } else if (i == 0)
                    {
                        priority = j;
                    }
                    else
                    {
                        priority = Math.Max(1, (j - i) / 2);
                    }
                    
                    var interval = (priority, i, j);
                    if (priority > bestInterval.priority)
                    {
                        bestInterval = interval;
                    }

                    // SS: a true follows after this false
                    i = j + 1;
                }
                else
                {
                    i++;
                }
            }
            
            
            
            
            return 0;
        }
        
    }

    [TestFixture]
    public class GoogleProblem14Test
    {
        [Test]
        public void TestOneTime()
        {
            // Arrange
            var problem = new GoogleProblem14();
            var input = new bool[] {true, false, false, true, false, false, false, false, true, false, true, false, false, false, false};
            
            // Act
            var index = problem.FindOneTimeIndex(input);

            // Assert
            Assert.AreEqual(14, index);
        }
        
    }
}