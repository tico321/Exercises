using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Exercises.Medium
{
#nullable enable
    /// <summary>
    /// given the root of the binary tree and two nodes of the tree. We need to find the lowest common ancestor of
    /// these two nodes. The Lowest Common Ancestor(LCA) of two nodes  p and q is the lowest node in the Binary Tree
    /// that has both  p and q as its descendants.
    ///
    /// Input:  root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
    /// Output: 3
    ///          3
    ///       /    \
    ///      5      1
    ///     / \    / \
    ///    6   2   0  8
    ///       / \
    ///      7   4
    /// </summary>
    public sealed class LowestCommonAncestorOfABinaryTree
    {
        [Fact]
        public void Test()
        {
            List<(Tree, int, int, int)> testCases = new()
            {
                (new Tree(3,
                    new Tree(5,
                        new Tree(6, null, null),
                        new Tree(2,
                            new Tree(7, null, null),
                            new Tree(4, null, null))),
                    new Tree(1,
                        new Tree(0, null, null),
                        new Tree(8, null, null))),
                5,
                1,
                3)
            };

            foreach((Tree root, int p, int q, int expected) in testCases)
            {
                Assert.Equal(expected, Solve(root, p, q, 0));
            }
        }

        private int Solve(Tree? root, int p, int q, int answer)
        {
            if (root == null) return 0;
            if (answer > 0) return answer;
            if (root.Value == p) return p;
            if (root.Value == q) return q;

            int left = Solve(root?.Left, p, q, answer);
            int right = Solve(root?.Right, p, q, answer);

            return left == p || left == q && right == p || right == q
                ? root!.Value
                : 0;
        }
    }

    sealed record Tree(int Value, Tree? Left, Tree? Right);
}
