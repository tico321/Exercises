using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ClassicComputerScienceProblems
{
    /*
     * This exercise explores binary search.
     *
     * Genes are commonly represented in computer software as a sequence of the characters A, C, G, and T.
     * Each letter represents a nucleotide, and the combination of three nucleotides is called a codon.
     * A codon codes for a specific amino acid that together with other amino acids can form a protein.
     * A classic task in bioinformatics software is to find a particular codon within a gene.
     */
    public class L2_1_DNASearch
    {
        public enum Nucleotide
        {
            A,
            C,
            G,
            T
        }

        private readonly string GeneStr = "ACGTGGCTCTCTAACGTACGTACGTACGGGGTTTATATATACCCTAGGACTCCCTTT";

        [Fact]
        public void GenCreation()
        {
            var geneStr = "ACGTGG";
            var gene = new Gene(geneStr);

            Assert.Equal(2, gene.Codons.Count);
            var c1 = gene.Codons.First();
            Assert.Equal(c1.N1, Nucleotide.A);
            Assert.Equal(c1.N2, Nucleotide.C);
            Assert.Equal(c1.N3, Nucleotide.G);
            var c2 = gene.Codons.Skip(1).First();
            Assert.Equal(c2.N1, Nucleotide.T);
            Assert.Equal(c2.N2, Nucleotide.G);
            Assert.Equal(c2.N2, Nucleotide.G);
        }

        [Theory]
        [InlineData("TTT", true)]
        [InlineData("AAA", false)]
        public void CodonLinearSearch(string codonStr, bool expected)
        {
            // "ACGTGGCTCTCTAACGTACGTACGTACGGGGTTTATATATACCCTAGGACTCCCTTT"
            var codon = new Codon(codonStr);
            var gene = new Gene(GeneStr);

            var actual = gene.ContainsLinear(codon);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("TTT", true)]
        [InlineData("AAA", false)]
        public void CodonBinarySearch(string codonStr, bool expected)
        {
            // "ACGTGGCTCTCTAACGTACGTACGTACGGGGTTTATATATACCCTAGGACTCCCTTT"
            var codon = new Codon(codonStr);
            var gene = new Gene(GeneStr);

            var actual = gene.ContainsBinary(codon);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("TTT", true)]
        [InlineData("AAA", false)]
        public void CodonGenericSearch(string codonStr, bool expected)
        {
            // "ACGTGGCTCTCTAACGTACGTACGTACGGGGTTTATATATACCCTAGGACTCCCTTT"
            var codon = new Codon(codonStr);
            var gene = new Gene(GeneStr);

            var actualLinear = gene.Codons.GenericLinearSearch(codon);
            var actualBinary = gene.Codons.GenericBinarySearch(codon);

            Assert.Equal(expected, actualLinear);
            Assert.Equal(expected, actualBinary);
        }

        public class Codon : IEquatable<Codon>, IComparable<Codon>
        {
            public Codon(string seq)
            {
                if (string.IsNullOrEmpty(seq) || seq.Length != 3) throw new ArgumentException("Codons have 3 nucleotides");
                if (!Enum.TryParse(seq[0].ToString(), out Nucleotide n1) ||
                    !Enum.TryParse(seq[1].ToString(), out Nucleotide n2) ||
                    !Enum.TryParse(seq[2].ToString(), out Nucleotide n3))
                    throw new ArgumentException("Only A,C,T,G are valid Nucleotides");
                N1 = n1;
                N2 = n2;
                N3 = n3;
            }

            public Nucleotide N1 { get; }
            public Nucleotide N2 { get; }
            public Nucleotide N3 { get; }

            public int CompareTo(Codon other)
            {
                Func<Nucleotide, Nucleotide, int> eq = (a, b) => a.CompareTo(b);
                var a = eq(N1, other.N1);
                var b = eq(N2, other.N2);
                var c = eq(N3, other.N3);
                if (a != 0) return a;
                if (b != 0) return b;
                if (c != 0) return c;
                return 0;
            }

            public bool Equals(Codon other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return N1 == other.N1 && N2 == other.N2 && N3 == other.N3;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((Codon) obj);
            }

            public override int GetHashCode() => HashCode.Combine((int) N1, (int) N2, (int) N3);

            public static bool operator ==(Codon obj1, Codon obj2)
            {
                if (ReferenceEquals(obj1, obj2)) return true;
                if (ReferenceEquals(obj1, null)) return false;
                if (ReferenceEquals(obj2, null)) return false;

                return obj1.Equals(obj2);
            }

            public static bool operator !=(Codon obj1, Codon obj2) => !(obj1 == obj2);
        }

        public class Gene
        {
            public Gene(string geneStr)
            {
                if (geneStr.Length % 3 != 0) throw new ArgumentException("Genes are groups of codons");
                Codons = geneStr
                    .Select((c, i) => new { Char = c, Indx = i })
                    .GroupBy(c => c.Indx / 3)
                    .Select(group => group.Aggregate("", (codon, c) => codon + c.Char))
                    .Select(codonStr => new Codon(codonStr))
                    .ToList();
            }

            public List<Codon> Codons { get; }

            /*
             * One basic operation we may want to perform on a gene is to search it for a particular codon.
             * A scientist may want to do this to see if it codes for a particular amino acid. The goal is to simply
             * find out whether the codon exists within the gene or not.
             *
             * In the worst case, a linear search will require going through every element in a data structure,
             * so it is of O(n) complexity, where n is the number of elements in the structure.
             */
            public bool ContainsLinear(Codon key)
            {
                // imperative:
                // instead we could just return Codons.Contains(key);
                foreach (var codon in Codons)
                    if (codon == key)
                        return true;

                return false;
            }

            /*
             * A binary search works by looking at the middle element in a sorted range of elements,
             * comparing it to the element sought, reducing the range by half based on that comparison,
             * and starting the process over again.
             * A binary search continually reduces the search space by half, so it has a worst-case runtime of O(lg n)
             */
            public bool ContainsBinary(Codon key)
            {
                // binary search only works on sorted collections
                var sortedCodons = new List<Codon>(Codons);
                sortedCodons.Sort();

                var low = 0;
                var high = sortedCodons.Count - 1;
                while (low <= high) // while there is still a search space
                {
                    var middle = (low + high) / 2;
                    var comparison = sortedCodons[middle].CompareTo(key);
                    switch (comparison)
                    {
                        case < 0:
                            low = middle + 1; // middle codon is less than key
                            break;
                        case > 0:
                            high = middle - 1; // middle codon is > key
                            break;
                        default:
                            return true;
                    }
                }

                return false;
            }
        }
    }

    public static class GenericExtensions
    {
        /*
         * The methods linearContains() and binaryContains() can be generalized to work with almost any List.
         */
        public static bool GenericLinearSearch<T>(this List<T> arr, T key) where T : IComparable<T>
        {
            foreach (var item in arr)
                if (item.CompareTo(key) == 0)
                    return true;

            return false;
        }

        public static bool GenericBinarySearch<T>(this List<T> arr, T key) where T : IComparable<T>
        {
            // binary search only works on sorted collections
            var sortedArr = new List<T>(arr);
            sortedArr.Sort();

            var low = 0;
            var high = sortedArr.Count - 1;
            while (low <= high) // while there is still a search space
            {
                var middle = (low + high) / 2;
                var comparison = sortedArr[middle].CompareTo(key);
                switch (comparison)
                {
                    case < 0:
                        low = middle + 1; // middle codon is less than key
                        break;
                    case > 0:
                        high = middle - 1; // middle codon is > key
                        break;
                    default:
                        return true;
                }
            }

            return false;
        }
    }
}