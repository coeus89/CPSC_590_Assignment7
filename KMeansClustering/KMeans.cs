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
            List<ClusterCenterPoint> Clist = null;
            
        }

        public static void InitializeCentersRandomlyBetweenMaxMinRanges(ref List<MyPoint> PList, ref List<ClusterCenterPoint> CList, int numClusters)
        {
            // determine ranges
            double minX = (from p in PList select p.X).Min();
            double minY = (from p in PList select p.Y).Min();
            double maxX = (from p in PList select p.X).Max();
            double maxY = (from p in PList select p.Y).Max();

            // initialize cluster centers randomly
            Random rand = new Random((int)DateTime.Now.Ticks);
            CList = new List<ClusterCenterPoint>();
            for (int i = 0; i < numClusters; i++)
            {
                //ClusterCenterPoint cp = new ClusterCenterPoint { Cx = rand.NextDouble() *(maxX - minX), Cy = rand.NextDouble() * (maxY - minY) };

                int num = (int)rand.NextDouble() * PList.Count;
                ClusterCenterPoint cp = new ClusterCenterPoint { Cx = PList[num].X, Cy = PList[num].Y };
                cp.ClusterID = i;
                CList.Add(cp);
            }
        }

        public static void InitializeCentersRandomlyFromGivenPoints(ref List<MyPoint> PList, ref List<ClusterCenterPoint> CList, int numClusters)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            CList = new List<ClusterCenterPoint>();
            List<int> PListInt = new List<int>();
            for (int i = 0; i < PList.Count; i++)
                PListInt.Add(i);
            for (int i = 0; i < numClusters; i++)
            {
                int num = rand.Next(PListInt.Count);
                ClusterCenterPoint cp = new ClusterCenterPoint { Cx = PList[num].X, Cy = PList[num].Y };
                cp.ClusterID = i;
                PListInt.RemoveAt(num);
                CList.Add(cp);
            }
            if (CList.Count < numClusters)
                throw new Exception("Problem in initializing cluster centers..");

        }

        public static void InitializeCentersRandomlyFromKPP(List<MyPoint> PList, ref List<ClusterCenterPoint> CList, int numClusters)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            CList = new List<ClusterCenterPoint>();
            List<int> PListInt = new List<int>();
            for (int i = 0; i < PList.Count; i++)
                PListInt.Add(i);
            //step 1 of KPP - choose a center randomly from given set of points
            int num = rand.Next(PListInt.Count);
            ClusterCenterPoint cp = new ClusterCenterPoint { Cx = PList[num].X, Cy = PList[num].Y };
            cp.ClusterID = 0;
            PListInt.RemoveAt(num); // remove the number that has been selected so that it does not get selected again
            CList.Add(cp);

            for (int i = 1; i < numClusters; i++)
            {
                //step 2 of KPP - Compute D(x)^2 where D(x) is the distance of x from closest centers chosen
                double dxSquared = 0;
                double[] dxprimeSquared = new double[PListInt.Count];
                for (int m = 0; m < PListInt.Count; m++)
                {
                    double distprev = double.MaxValue;
                    foreach(ClusterCenterPoint ccp in CList)
                    {
                        double dist = FindDistance(PList[PListInt[m]], ccp);
                        if (dist < distprev)
                            distprev = dist;
                    }
                    dxprimeSquared[m] = distprev * distprev; // dxprimesquared value is actually for point PListInt[m]
                    dxSquared += distprev * distprev;
                }

                // select the center according to probability d(x')^2/(d()^2
                double randNum = rand.NextDouble();
                double sumProbab = 0;
                for (int n=0; n<dxprimeSquared.Length; n++)
                {
                    sumProbab = sumProbab + dxprimeSquared[n] / dxSquared;
                    if(randNum <= sumProbab)
                    {
                        ClusterCenterPoint cpp = new ClusterCenterPoint
                        { // choose this PList[PListInt[n]] as the next cluster center
                            Cx = PList[PListInt[n]].X,
                            Cy = PList[PListInt[n]].Y
                        };
                        cpp.ClusterID = i;
                        PListInt.RemoveAt(n); // remove the number that has been selected so that it does not get selected again
                        CList.Add(cpp);
                        break;
                    }
                }
            }
        }

        public static double FindDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public static double FindDistance(MyPoint p1, ClusterCenterPoint p2)
        {
            return Math.Sqrt((p2.Cx - p1.X) * (p2.Cx - p1.X) + (p2.Cy - p1.Y) * (p2.Cy - p1.Y));
        }
    }
}
