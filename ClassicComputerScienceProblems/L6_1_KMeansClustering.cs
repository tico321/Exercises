using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace ClassicComputerScienceProblems
{
    /// <summary>
    /// Clustering is a computational technique that divides the points in a data set into groups.
    /// A successful clustering results in groups that contain points that are related to one another.
    /// Whether those relationships are meaningful generally requires human verification.
    ///
    /// In clustering, the group (a.k.a. cluster) that a data point belongs to is not predetermined,
    /// but instead is decided during the run of the clustering algorithm.
    /// In fact, the algorithm is not guided to place any particular data point in any particular cluster
    /// by presupposed information. For this reason, clustering is considered an unsupervised method within
    /// the realm of machine learning. You can think of unsupervised as meaning not guided by foreknowledge.
    ///
    /// Clustering is a useful technique when you want to learn about the structure of a data set but you do not know
    /// ahead of time its constituent parts. For example, imagine you own a grocery store, and you collect data about
    /// customers and their transactions. You want to run mobile advertisements of specials at relevant times of the
    /// week to bring customers into your store. You could try clustering your data by day of the week and demographic
    /// information. Perhaps you will find a cluster that indicates younger shoppers prefer to shop on Tuesdays,
    /// and you could use that information to run an ad specifically targeting them on that day.
    ///
    /// k-means++, is another algorithm that attempts to solve the initialization problem by choosing centroids based on
    /// a probability distribution of distance to every point instead of pure randomness.
    /// </summary>
    public class L6_1_KMeansClustering
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public L6_1_KMeansClustering(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void KMeansTest()
        {
            var point1 = new DataPoint(new List<double> {2.0, 1.0, 1.0});
            var point2 = new DataPoint(new List<double> {2.0, 2.0, 5.0});
            var point3 = new DataPoint(new List<double> {3.0, 1.5, 2.5});
            var kMeans = new KMeans<DataPoint>(
                k: 2,
                points: new List<DataPoint> {point1, point2, point3},
                debugLogger: _testOutputHelper.WriteLine);

            var testClusters = kMeans.Run(100);
            for (var clusterIndex = 0; clusterIndex < testClusters.Count; clusterIndex++)
            {
                var points = string.Join(",", testClusters[clusterIndex].Points);
                _testOutputHelper.WriteLine($"Cluster {clusterIndex}: {points}");
            }
        }

        // Statistics primitives
        public class Statistics
        {
            private List<double> _list;

            public Statistics(List<double> list)
            {
                _list = list;
            }

            public double Sum() => _list.Sum();

            // Find the average (mean)
            public double Mean() => _list.Average();

            // Find the variance sum((Xi - mean)^2) / N
            public double Variance()
            {
                var mean = Mean();
                return _list
                    .Select(x => Math.Pow((x - mean), 2))
                    .Average();
            }

            // Find the standard deviation sqrt(variance)
            public double Std()
            {
                return Math.Sqrt(Variance());
            }

            /// <summary>
            /// Convert elements to respective z-scores, which is the number of standard deviations the original
            /// value is from the data set’s mean.
            ///
            /// z-score is calculated by taking a value, subtracting the mean of all of the values from it,
            /// and dividing that result by the standard deviation of all of the values.
            ///     formula z-score = (x - mean) / std
            /// </summary>
            public List<double> ZScored()
            {
                var mean = Mean();
                var std = Std();
                return _list
                    .Select(x => std != 0 ? ((x - mean) / std) : 0.0)
                    .ToList();
            }

            public double Max() => _list.Max();

            public double Min() => _list.Min();
        }

        // All clustering algorithms work with points of data
        public class DataPoint
        {
            // Every data point type has a certain number of dimensions
            public int NumDimensions { get; }
            // The list dimensions stores the actual values for each of those dimensions as doubles.
            // These dimensions may later be replaced with z-scores by k-means
            public List<double> Dimensions { get; set; }
            // we also keep a copy of the initial data in originals for later printing.
            private readonly List<double> _originals;

            public DataPoint(List<double> initials)
            {
                _originals = initials;
                Dimensions = new List<double>(initials);
                NumDimensions = Dimensions.Count;
            }

            // There are many ways to calculate distance, but the form most commonly used with k-means is Euclidean distance.
            public double Distance(DataPoint other)
            {
                var differences = 0.0;
                for (var i = 0; i < NumDimensions; i++) {
                    var difference = Dimensions[i] - other.Dimensions[i];
                    differences += Math.Pow(difference, 2);
                }
                return Math.Sqrt(differences);
            }

            public override string ToString()
            {
                return string.Join(",", _originals);
            }
        }

        /*
         * K-means Clustering algorithm
         *  Attempts to group data points into a certain predefined number of clusters.
         *
         * In every round of k-means, the distance between every data point and every center of a cluster
         * (a point known as a centroid) is calculated.
         * Points are assigned to the cluster whose centroid they are closest to.
         * Then the algorithm recalculates all of the centroids, finding the mean of each cluster’s assigned points
         * and replacing the old centroid with the new mean.
         * The process of assigning points and recalculating centroids continues until the centroids stop moving or a
         * certain number of iterations occurs.
         *
         * Each dimension of the initial points provided to k-means needs to be comparable in magnitude.
         * The process of making data comparable is known as normalization, a common way of normalizing data is to
         * evaluate each value based on its z-score.
         *
         * The main difficulty is choosing how to assign initial centroids. The most basic form is to place them
         * randomly.
         * Another difficulty is deciding how many clusters to divide the data into (the “k” in k-means). One way is to
         * let the user define the number by trial an error.
         *
         * Steps:
         * 1. Initialize all of the data points and “k” empty clusters.
         * 2. Normalize all of the data points.
         * 3. Create random centroids associated with each cluster.
         * 4. Assign each data point to the cluster of the centroid it is closest to.
         * 5. Recalculate each centroid so it is the center (mean) of the cluster it is associated with.
         * 6. Repeat steps 4 and 5 until a maximum number of iterations is reached or the centroids stop moving (convergence).
         *
         */

        public class KMeans<TPoint> where TPoint : DataPoint
        {
            // All of the points in the data set.
            private readonly List<TPoint> _points;

            private readonly Action<string> _debugLogger;

            // The points are further divided between the clusters
            private readonly List<Cluster> _clusters;

            public KMeans(int k, List<TPoint> points, Action<string> debugLogger = null)
            {
                // can't have negative or zero clusters
                if (k < 1) throw new ArgumentException("K must be >= 1");
                _points = points;
                _debugLogger = debugLogger ?? (_ => {});
                //  All of the data points that will be used in the algorithm are normalized by z-score
                ZScoreNormalize();

                // Initialize K empty clusters with random centroids
                _clusters = new List<Cluster>();
                for (var i = 0; i < k; i++) {
                    var randPoint = RandomPoint();
                    var cluster = new Cluster(new List<TPoint>(), randPoint);
                    _clusters.Add(cluster);
                }

            }

            public List<Cluster> Run(int maxIterations)
            {
                for (var iteration = 0; iteration < maxIterations; iteration++)
                {
                    // We clear all clusters to avoid putting duplicated values with the current implementation
                    foreach(var cluster in _clusters) cluster.Points.Clear();

                    AssignClusters();
                    var oldCentroids = new List<DataPoint>(Centroids());
                    GenerateCentroids(); // find new centroids
                    if (ListsEqual(oldCentroids, Centroids()))
                    {
                        _debugLogger("Converged after " + iteration + " iterations.");
                        return _clusters;
                    }
                }

                return _clusters;
            }

            // replaces the values in the dimensions list of every data point with its z-scored equivalent.
            // Although the values in the dimensions list are replaced, the originals list in the DataPoint are not.
            private void ZScoreNormalize()
            {
                var zscored = _points.Select(point => new List<double>()).ToList();

                for (var dimension = 0; dimension < _points[0].NumDimensions; dimension++)
                {
                    var dimensionSlice = DimensionSlice(dimension);
                    var stats = new Statistics(dimensionSlice);
                    var zscores = stats.ZScored();
                    for (var index = 0; index < zscores.Count; index++)
                    {
                        zscored[index].Add(zscores[index]);
                    }
                }
                for (var i = 0; i < _points.Count; i++)
                {
                    _points[i].Dimensions = zscored[i];
                }
            }

            // returns all of the centroids associated with the clusters that are associated with the algorithm.
            private List<DataPoint> Centroids() => _clusters.Select(cluster => cluster.Centroid).ToList();

            // convenience method that can be thought of as returning a column of data. It will return a
            // list composed of every value at a particular index in every data point.
            private List<Double> DimensionSlice(int dimension) => _points
                .Select(x => x.Dimensions[dimension])
                .ToList();

            // RandomPoint() method is used in the constructor to create the initial random centroids for each cluster.
            // It constrains the random values of each point to be within the range of the existing data points’ values.
            private DataPoint RandomPoint()
            {
                var randDimensions = new List<double>();
                var random = new Random();
                for (var dimension = 0; dimension < _points[0].NumDimensions; dimension++) {
                    var values = DimensionSlice(dimension);
                    var stats = new Statistics(values);
                    var max = stats.Max();
                    var min = stats.Min();
                    var randValue = random.NextDouble() * (max - min) + min;;
                    randDimensions.Add(randValue);
                }
                return new DataPoint(randDimensions);
            }

            // Find the closest cluster centroid to each point and assign the point to that cluster
            private void AssignClusters()
            {
                foreach (var point in _points)
                {
                    var lowestDistance = double.MaxValue;
                    var closestCluster = _clusters[0];
                    foreach(var cluster in _clusters) {
                        var centroidDistance = point.Distance(cluster.Centroid);
                        if (!(centroidDistance < lowestDistance)) continue;
                        lowestDistance = centroidDistance;
                        closestCluster = cluster;
                    }
                    closestCluster.Points.Add(point);
                }
            }

            // Find the center of each cluster and move the centroid to there
            // After every point is assigned to a cluster, the new centroids are calculated.
            // This involves calculating the mean of each dimension of every point in the cluster.
            // The means of these dimensions are then combined to find the “mean point” in the cluster,
            // which becomes the new centroid.
            // Note that we cannot use dimensionSlice() here, because the points in question are a subset of all
            // of the points (just those belonging to a particular cluster).
            private void GenerateCentroids()
            {
                foreach (var cluster in _clusters)
                {
                    // Ignore if the cluster is empty
                    if (cluster.Points.Count == 0) continue;

                    var means = new List<double>();
                    for (var i = 0; i < cluster.Points[0].NumDimensions; i++) {
                        // needed to use in scope of closure
                        var dimension = i;
                        var dimensionMean = cluster.Points
                            .Select(x => x.Dimensions[dimension])
                            .Average();
                        means.Add(dimensionMean);
                    }
                    cluster.Centroid = new DataPoint(means);
                }
            }

            // Check if two Lists of DataPoints are of equivalent DataPoints
            private bool ListsEqual(List<DataPoint> first, List<DataPoint> second)
            {
                if (first.Count != second.Count) return false;

                var doubleThreshold = 0.0001;
                var areEqual = first
                    .Zip(second)
                    .SelectMany(tuple => tuple.First.Dimensions.Zip(tuple.Second.Dimensions))
                    .All(tuple => Math.Abs(tuple.First - tuple.Second) < doubleThreshold);
                return areEqual;

                // for (var i = 0; i < first.Count; i++) {
                //     for (var j = 0; j < first[0].NumDimensions; j++) {
                //         if (Math.Abs(first[i].Dimensions[j] - second[i].Dimensions[j]) > 0.0001) {
                //             return false;
                //         }
                //     }
                // }
                //
                // return true;
            }

            // Each Cluster has data points and a centroid associated with it.
            public class Cluster
            {
                public List<TPoint> Points { get; set; }
                public DataPoint Centroid { get; set; }
                public Cluster(List<TPoint> points, DataPoint randPoint)
                {
                    Points = points;
                    Centroid = randPoint;
                }
            }
        }
    }
}
