using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    class KMeans
    {
        public static int DoKMeansWithMinVariance(int numClusters, ref List<MyPoint> PList, ref List<ClusterCenterPoint> CL, 
            double maxError, int maxIterations, bool minVariance)
        {
            
            return -1;
        }

        public static int DoKMeans(int numClusters, ref List<MyPoint> PList, ref List<ClusterCenterPoint> CL, 
            double maxError, int maxIterations, bool doKmeansPlusPlus = false)
        {
            
            return -1;
        }

        public static void InitializeCentersRandomlyBetweenMaxMinRanges(ref List<MyPoint> PList, ref List<ClusterCenterPoint> CList, int numClusters)
        {

        }

        public static void InitializeCentersRandomlyFromGivenPoints(ref List<MyPoint> PList, ref List<ClusterCenterPoint> CList, int numClusters)
        {

        }

        public static void InitializeCentersRandomlyFromKPP(List<MyPoint> PList, ref List<ClusterCenterPoint> CList, int numClusters)
        {

        }

        public static double FindDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

    }
}
