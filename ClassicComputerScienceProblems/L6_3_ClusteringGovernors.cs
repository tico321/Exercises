using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using static ClassicComputerScienceProblems.L6_1_KMeansClustering;

namespace ClassicComputerScienceProblems
{
    // Every American state has a governor. In June 2017, those governors ranged in age from 42 to 79. If we take the
    // United States from east to west, looking at each state by its longitude, perhaps we can find clusters of states
    // with similar longitudes and similar-age governors.
    public class L6_3_ClusteringGovernors
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public L6_3_ClusteringGovernors(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Cluster()
        {
            var governors = new List<Governor>();
            governors.Add(new Governor(-86.79113, 72, "Alabama"));
            governors.Add(new Governor(-152.404419, 66, "Alaska"));
            governors.Add(new Governor(-111.431221, 53, "Arizona"));
            governors.Add(new Governor(-92.373123, 66, "Arkansas"));
            governors.Add(new Governor(-119.681564, 79, "California"));
            governors.Add(new Governor(-105.311104, 65, "Colorado"));
            governors.Add(new Governor(-72.755371, 61, "Connecticut"));
            governors.Add(new Governor(-75.507141, 61, "Delaware"));
            governors.Add(new Governor(-81.686783, 64, "Florida"));
            governors.Add(new Governor(-83.643074, 74, "Georgia"));
            governors.Add(new Governor(-157.498337, 60, "Hawaii"));
            governors.Add(new Governor(-114.478828, 75, "Idaho"));
            governors.Add(new Governor(-88.986137, 60, "Illinois"));
            governors.Add(new Governor(-86.258278, 49, "Indiana"));
            governors.Add(new Governor(-93.210526, 57, "Iowa"));
            governors.Add(new Governor(-96.726486, 60, "Kansas"));
            governors.Add(new Governor(-84.670067, 50, "Kentucky"));
            governors.Add(new Governor(-91.867805, 50, "Louisiana"));
            governors.Add(new Governor(-69.381927, 68, "Maine"));
            governors.Add(new Governor(-76.802101, 61, "Maryland"));
            governors.Add(new Governor(-71.530106, 60, "Massachusetts"));
            governors.Add(new Governor(-84.536095, 58, "Michigan"));
            governors.Add(new Governor(-93.900192, 70, "Minnesota"));
            governors.Add(new Governor(-89.678696, 62, "Mississippi"));
            governors.Add(new Governor(-92.288368, 43, "Missouri"));
            governors.Add(new Governor(-110.454353, 51, "Montana"));
            governors.Add(new Governor(-98.268082, 52, "Nebraska"));
            governors.Add(new Governor(-117.055374, 53, "Nevada"));
            governors.Add(new Governor(-71.563896, 42, "New Hampshire"));
            governors.Add(new Governor(-74.521011, 54, "New Jersey"));
            governors.Add(new Governor(-106.248482, 57, "New Mexico"));
            governors.Add(new Governor(-74.948051, 59, "New York"));
            governors.Add(new Governor(-79.806419, 60, "North Carolina"));
            governors.Add(new Governor(-99.784012, 60, "North Dakota"));
            governors.Add(new Governor(-82.764915, 65, "Ohio"));
            governors.Add(new Governor(-96.928917, 62, "Oklahoma"));
            governors.Add(new Governor(-122.070938, 56, "Oregon"));
            governors.Add(new Governor(-77.209755, 68, "Pennsylvania"));
            governors.Add(new Governor(-71.51178, 46, "Rhode Island"));
            governors.Add(new Governor(-80.945007, 70, "South Carolina"));
            governors.Add(new Governor(-99.438828, 64, "South Dakota"));
            governors.Add(new Governor(-86.692345, 58, "Tennessee"));
            governors.Add(new Governor(-97.563461, 59, "Texas"));
            governors.Add(new Governor(-111.862434, 70, "Utah"));
            governors.Add(new Governor(-72.710686, 58, "Vermont"));
            governors.Add(new Governor(-78.169968, 60, "Virginia"));
            governors.Add(new Governor(-121.490494, 66, "Washington"));
            governors.Add(new Governor(-80.954453, 66, "West Virginia"));
            governors.Add(new Governor(-89.616508, 49, "Wisconsin"));
            governors.Add(new Governor(-107.30249, 55, "Wyoming"));

            var kmeans = new KMeans<Governor>(2, governors, _testOutputHelper.WriteLine);
            var govClusters = kmeans.Run(100);
            for (var clusterIndex = 0; clusterIndex < govClusters.Count; clusterIndex++)
            {
                _testOutputHelper.WriteLine($"Cluster {clusterIndex}: [{string.Join(",", govClusters[clusterIndex].Points)}]");
            }
        }

        public class Governor : DataPoint
        {
            private double _longitude;
            private double _age;
            private string _state;

            // A Governor has two named and stored dimensions: longitude and age.
            public Governor(double longitude, double age, string state) : base(new List<double>{longitude, age})
            {
                _longitude = longitude;
                _age = age;
                _state = state;
            }

            public override string ToString() => $"{_state}: (longitude: {_longitude}, age: {_age})";
        }
    }
}
