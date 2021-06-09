using System;
using System.Collections;
using System.Text;
using Xunit;

namespace ClassicComputerScienceProblems
{
    /*
     * Compression is the act of taking data and encoding it (changing its form) in such a way that it takes up less space.
     * Decompression is reversing the process, returning the data to its original form.
     *
     * It is more storage-efficient to compress data, but there is a trade-off between time and space as it takes time
     * to compress and decompress data.
     */
    public class L1_2_TrivialCompression
    {
        [Fact]
        public void CompressDecompressIdentity()
        {
            var gene = "ACGTACGT";

            Assert.Equal(gene, new CompressedGene(gene).GetDecompressed());
        }
        /*
         * Representing DNA which can be one of four nucleotides : "A", "C", "G, "T".
         * Each value as a string will take up to 16bits (UTF-16),
         * instead we can represent it with 2 bits. A -> 00, C -> 01, G -> 10, T -> 11
         */
        class CompressedGene
        {
            public int Length { get; private set; }
            public BitArray Bits { get; private set; }
            public CompressedGene(string gene)
            {
                Length = gene.Length;
                Bits = new BitArray(Length * 2, defaultValue: false);
                for (var i = 0; i < Length; i++)
                {
                    var iBit1 = i*2;
                    var iBit2 = i*2 + 1;
                    switch (gene[i])
                    {
                        case 'A':
                            Bits[iBit1] = false;
                            Bits[iBit2] = false;
                            break;
                        case 'C':
                            Bits[iBit1] = false;
                            Bits[iBit2] = true;
                            break;
                        case 'G':
                            Bits[iBit1] = true;
                            Bits[iBit2] = false;
                            break;
                        case 'T':
                            Bits[iBit1] = true;
                            Bits[iBit2] = true;
                            break;
                        default:
                            throw new Exception($"invalid nucleotide {gene[i]}");
                    }
                }
            }

            public string GetDecompressed()
            {
                // create a mutable place for characters with the right capacity
                StringBuilder builder = new StringBuilder(Length);
                for (int i = 0; i < (Length * 2); i += 2) {
                    var firstBit = Bits[i] ? 1 : 0;
                    var secondBit = Bits[i + 1] ? 1 : 0;
                    /*
                     * lastBits is made by shifting the first bit back one place, and then ORing (| operator) the result
                     * with the second bit. When a value is shifted, using the << operator, the space left behind is
                     * replaced with 0s. An OR says, “If either of these bits are a 1, put a 1.” There for ORing
                     * secondBit with a 0 will always just result in the value of secondBit
                     */
                    var lastBits = (firstBit << 1) | secondBit;
                    switch (lastBits) {
                        case 0b00: // 00 is 'A'
                            builder.Append('A');
                            break;
                        case 0b01: // 01 is 'C'
                            builder.Append('C');
                            break;
                        case 0b10: // 10 is 'G'
                            builder.Append('G');
                            break;
                        case 0b11: // 11 is 'T'
                            builder.Append('T');
                            break;
                    }
                }
                return builder.ToString();
            }
        }
    }
}