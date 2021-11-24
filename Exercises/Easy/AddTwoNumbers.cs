using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercises.Easy
{
    /// <summary>
    /// You are given two non-empty linked lists representing two non-negative.
    /// The digits are stored in reverse order, and each of their nodes contains a single digit.
    /// Add the two numbers and return the sum as a linked list.
    /// </summary>
    public class AddTwoNumbers
    {
        [Theory]
        [InlineData(new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 }, new int[] { 2, 2, 2 })]
        [InlineData(new int[] { 1, 1, 1 }, new int[] { 9, 1, 1 }, new int[] { 0, 3, 2 })]
        [InlineData(new int[] { 1, 1 }, new int[] { 9, 8 }, new int[] { 0, 0, 1 })]
        [InlineData(new int[] { 1, 1 }, new int[] { 9, 9 }, new int[] { 0, 1, 1 })]
        [InlineData(new int[] { 1, 1 }, new int[] { }, new int[] { 1, 1 })]
        [InlineData(new int[] { }, new int[] { 1, 1 }, new int[] { 1, 1 })]
        [InlineData(new int[] { 1, 1, 1 }, new int[] { 9 }, new int[] { 0, 2, 1 })]
        [InlineData(new int[] { }, new int[] { }, new int[] { })]
        public void Add2Numbers(int[] a, int[] b, int[] c)
        {
            var listA = new LinkedList<int>(a.ToList());
            var listB = new LinkedList<int>(b.ToList());
            var expected = new LinkedList<int>(c.ToList());

            var actual = AddTwoNumbersSolution.Solve(listA, listB);

            Assert.Equal(expected, actual);
        }
    }
}
