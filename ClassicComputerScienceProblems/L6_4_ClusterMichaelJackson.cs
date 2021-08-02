using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using static ClassicComputerScienceProblems.L6_1_KMeansClustering;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    ///     Michael Jackson released 10 solo studio albums. In the following example,
    ///     we will cluster those albums by looking at two dimensions: album length (in minutes) and number of tracks.
    /// </summary>
    public class L6_4_ClusterMichaelJackson
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public L6_4_ClusterMichaelJackson(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ClusterMichaelJacksonAlbums()
        {
            var albums = new List<Album>();
            albums.Add(new Album("Got to Be There", 1972, 35.45, 10));
            albums.Add(new Album("Ben", 1972, 31.31, 10));
            albums.Add(new Album("Music & Me", 1973, 32.09, 10));
            albums.Add(new Album("Forever, Michael", 1975, 33.36, 10));
            albums.Add(new Album("Off the Wall", 1979, 42.28, 10));
            albums.Add(new Album("Thriller", 1982, 42.19, 9));
            albums.Add(new Album("Bad", 1987, 48.16, 10));
            albums.Add(new Album("Dangerous", 1991, 77.03, 14));
            albums.Add(new Album("HIStory: Past, Present and Future, Book I", 1995, 148.58, 30));
            albums.Add(new Album("Invincible", 2001, 77.05, 16));
            var kmeans = new KMeans<Album>(2, albums, _testOutputHelper.WriteLine);
            var clusters = kmeans.Run(100);
            for (var clusterIndex = 0; clusterIndex < clusters.Count; clusterIndex++)
                _testOutputHelper.WriteLine(
                    $"Cluster {clusterIndex}" +
                    $" Avg Length {clusters[clusterIndex].Centroid.Dimensions[0]}" +
                    $" Avg Tracks {clusters[clusterIndex].Centroid.Dimensions[1]}:" +
                    $" {string.Join(",", clusters[clusterIndex].Points)}");
        }

        public class Album : DataPoint
        {
            private readonly string name;
            private readonly int year;

            public Album(string name, int year, double length, double tracks)
                : base(new List<double> {length, tracks})
            {
                this.name = name;
                this.year = year;
            }

            public override string ToString()
            {
                return "(" + name + ", " + year + ")";
            }
        }
    }
}
