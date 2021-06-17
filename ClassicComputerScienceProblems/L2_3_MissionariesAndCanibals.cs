using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ClassicComputerScienceProblems
{
    public class L2_3_MissionariesAndCanibals
    {
        /*
         * Three missionaries and three cannibals are on the west bank of a river. They have a canoe that can
         * hold two people, and they all must cross to the east bank of the river. There may never be more
         * cannibals than missionaries on either side of the river, or the cannibals will eat the missionaries.
         * Further, the canoe must have at least one person on board to cross the river. What sequence of crossings
         * will successfully take the entire party across the river?
         */
        [Fact]
        public void SolutionSuccess()
        {
            var mc = new MCState(3, 3, true).GetSolution();

            Assert.True(mc.Count > 0);
        }

        public record MCState
        {
            public readonly int Max = 3;

            // we only need west missionaries and cannibals, with them we can calculate the east ones
            public MCState(int westMissionaries, int westCannibals, bool isBoatOnWestBank)
            {
                WestMissionaries = westMissionaries;
                WestCannibals = westCannibals;
                EastMissionaries = Max - WestMissionaries;
                EastCannibals = Max - WestCannibals;
                IsBoatOnWestBank = isBoatOnWestBank;
            }

            public int WestMissionaries { get; }
            public int WestCannibals { get; }
            public int EastMissionaries { get; }
            public int EastCannibals { get; }
            public bool IsBoatOnWestBank { get; }

            public override string ToString()
            {
                var boat = IsBoatOnWestBank ? "west" : "east";
                return
                    @$"On the west bank there are {WestMissionaries} missionaries and {WestCannibals} cannibals.%n
                    On the east bank there are {EastMissionaries} missionaries and {EastCannibals} cannibals.%n
                    The boat is on the {boat} bank.";
            }

            // all the missionaries and cannibals are on the east cost
            public bool GoalTest(MCState mcs) => mcs.IsLegal() && mcs.EastCannibals == Max && mcs.EastMissionaries == Max;

            // To create a successors function, it is necessary to go through all of the possible moves that can be made
            // from one bank to another and then check if each of those moves will result in a legal state.
            public static List<MCState> Successors(MCState mcs)
            {
                var sucs = new List<MCState>();
                if (mcs.IsBoatOnWestBank)
                {
                    // boat on west bank
                    if (mcs.WestMissionaries > 1) sucs.Add(new MCState(mcs.WestMissionaries - 2, mcs.WestCannibals, !mcs.IsBoatOnWestBank));
                    if (mcs.WestMissionaries > 0) sucs.Add(new MCState(mcs.WestMissionaries - 1, mcs.WestCannibals, !mcs.IsBoatOnWestBank));
                    if (mcs.WestCannibals > 1) sucs.Add(new MCState(mcs.WestMissionaries, mcs.WestCannibals - 2, !mcs.IsBoatOnWestBank));
                    if (mcs.WestCannibals > 0) sucs.Add(new MCState(mcs.WestMissionaries, mcs.WestCannibals - 1, !mcs.IsBoatOnWestBank));
                    if (mcs.WestCannibals > 0 && mcs.WestMissionaries > 0)
                        sucs.Add(new MCState(mcs.WestMissionaries - 1, mcs.WestCannibals - 1, !mcs.IsBoatOnWestBank));
                }
                else
                {
                    // boat on east bank
                    if (mcs.EastMissionaries > 1) sucs.Add(new MCState(mcs.WestMissionaries + 2, mcs.WestCannibals, !mcs.IsBoatOnWestBank));
                    if (mcs.EastMissionaries > 0) sucs.Add(new MCState(mcs.WestMissionaries + 1, mcs.WestCannibals, !mcs.IsBoatOnWestBank));
                    if (mcs.EastCannibals > 1) sucs.Add(new MCState(mcs.WestMissionaries, mcs.WestCannibals + 2, !mcs.IsBoatOnWestBank));
                    if (mcs.EastCannibals > 0) sucs.Add(new MCState(mcs.WestMissionaries, mcs.WestCannibals + 1, !mcs.IsBoatOnWestBank));
                    if (mcs.EastCannibals > 0 && mcs.EastMissionaries > 0)
                        sucs.Add(new MCState(mcs.WestMissionaries + 1, mcs.WestCannibals + 1, !mcs.IsBoatOnWestBank));
                }

                sucs = sucs.Where(s => s.IsLegal()).ToList();
                return sucs;
            }

            public bool IsLegal()
            {
                if (WestMissionaries < WestCannibals && WestMissionaries > 0) return false;
                if (EastMissionaries < EastCannibals && EastMissionaries > 0) return false;
                return true;
            }

            public List<MCState> GetSolution()
            {
                var node = L2_2_2_GenericSearch.DeepFirstSearch(this, GoalTest, Successors);
                var path = L2_2_2_GenericSearch.NodeToPath(node);
                return path;
            }
        }
    }
}