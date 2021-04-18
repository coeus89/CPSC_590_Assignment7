using Mapack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace GMM
{
    public class SwarmSystem
    {
        public SwarmSystem(int snum, int k, int dim)
        {
            this.swarmNum = snum;
            this.k = k;
            this.dim = dim;
        }

        private int swarmNum;
        int datasize;
        int picwidth;
        int picheight;
        double[][] imgVectorC;
        Matrix imgVecMatrix;
        public int SwarmNum
        {
            get => swarmNum;
            //set => swarmNum = value;
        }

        int k { get; set; }
        int dim { get; set; }
        
        public Bitmap picTennis { get; set; }

        public void Initialize(Bitmap inputPic)
        {
            picTennis = inputPic;
            datasize = inputPic.Width * inputPic.Height;
            picwidth = inputPic.Width;
            picheight = inputPic.Height;

            imgVectorC = new double[datasize][];
            for (int i = 0; i < datasize; i++)
            {
                imgVectorC[i] = new double[dim];
            }
            Color c1 = new Color();
            //Parallel.For(0, picTennis.Height, (i) =>
            for (int i = 0; i < picheight; i++)
            {
                for (int j = 0; j < picwidth; j++)
                {
                    c1 = picTennis.GetPixel(j, i); // x = width = j, y = height = i
                                                   // blue
                    imgVectorC[i * (picTennis.Width) + j][0] = c1.B;
                    // green
                    imgVectorC[i * (picTennis.Width) + j][1] = c1.G;
                    // red
                    imgVectorC[i * (picTennis.Width) + j][2] = c1.R;
                }
            }//);
            imgVecMatrix = new Matrix(imgVectorC);
        }

        private double FunctionToSolve(double x, double y)
        {
            // Rosenbrock function
            //double res = (1.0 - x) * (1.0 - x) + 100.0 *(y - (x * x)) * (y - (x * x));
            
            // Himmelblau's function
            double res = Math.Pow((Math.Pow(x, 2) + y - 11), 2) + Math.Pow((x + Math.Pow(y, 2) - 7), 2);
            //double res = Math.Pow((x * x + y - 11.0), 2) + Math.Pow((x + y * y - 7.0), 2);
            
            return res;
        }

        public SwarmResult DoTennisGMM(SwarmResult srIN = null)
        {
            GMM_NDim[] gmms = new GMM_NDim[10];
            if (srIN == null)
            {
                // if first run, make GMMs from scratch
                for (int j = 0; j < 10; j++)
                {
                    gmms[j] = new GMM_NDim(k, dim, (Matrix)imgVecMatrix.Clone());
                    gmms[j].Initialize();
                }
            }
            else
            {
                for (int j = 0; j < 10; j++)
                {
                    gmms[j] = (GMM_NDim)srIN.MyGMM.Clone();
                    datasize = srIN.MyGMM.X.Columns * srIN.MyGMM.X.Rows;
                }
            }
            

            List<SwarmResult> swarmResults = new List<SwarmResult>();
            //Parallel.For(0, 10, (i) =>
            for (int i = 0; i < 10; i++) // iterations (need to make parallel)
            {
                if (i > 0)
                    Console.WriteLine("Wait here.");
                GMM_NDim tennisGMM = gmms[i];
                tennisGMM.ComputeGMM_ND_Swarm();
                //tennisGMM.ComputeGMM_ND_Parallel();

                // determine class membership i.e., which point belongs to which cluster
                int[,] imgClass3d = new int[picheight, picwidth];
                AssignPicturePointClasses(k, datasize, imgVecMatrix, tennisGMM, imgClass3d);
                double[] silhouette = computeSilhouette(k, imgClass3d);
                SwarmResult sr = new SwarmResult();
                sr.silhouette = silhouette.Sum();
                sr.MyGMM = tennisGMM;
                sr.SwarmId = swarmNum;
                swarmResults.Add(sr);
            }//);
            swarmResults.Sort();
            return swarmResults[0];
        }

        private static void AssignPicturePointClasses(int k, int datasize, Matrix imgVecMatrix, GMM_NDim tennisGMM, int[,] imgClass3d)
        {
            int[] imgClass = new int[datasize];
            for (int i = 0; i < imgVecMatrix.Rows; i++)
            {
                // Gamma matrix has the probabilities for a data point for its membership in each cluster
                double[] probabs = new double[k];
                int cnum = 0;
                double maxprobab = tennisGMM.Gamma[i, 0];
                for (int m = 0; m < k; m++)
                {
                    if (tennisGMM.Gamma[i, m] > maxprobab)
                    {
                        cnum = m;  // data i belongs to cluster m
                        maxprobab = tennisGMM.Gamma[i, m];
                    }
                }
                imgClass[i] = cnum;
                //MyPoint pt = new MyPoint { ClusterId = cnum, X = X[i, 0], Y = X[i, 1] }; //delete
                //PList.Add(pt); //delete
            }
            Buffer.BlockCopy(imgClass, 0, imgClass3d, 0, 4 * imgClass.Length); // 4 bytes in an int
        }

        private static void ColorPictureByCluster(int k, Bitmap picTennis, int[,] imgClass3d, Bitmap myBit)
        {
            Color[] colors = new Color[k];
            Random r1 = new Random(1);
            for (int i = 0; i < k; i++)
            {
                byte R = (byte)r1.Next(255);
                byte G = (byte)r1.Next(255);
                byte B = (byte)r1.Next(255);
                colors[i] = Color.FromArgb(R, G, B);

            }
            int pixelNum = 0;
            int classnum = 0;
            for (int i = 0; i < picTennis.Height; i++)
                for (int j = 0; j < picTennis.Width; j++)
                {
                    pixelNum = i * picTennis.Height + j;
                    //classnum = imgClass[pixelNum];
                    classnum = imgClass3d[i, j];
                    myBit.SetPixel(j, i, colors[classnum]);
                }
        }

        public double[] computeSilhouette(int numClasses, int[,] imgClass3d)
        {
            List<MyPoint>[] classPoints = new List<MyPoint>[numClasses];
            List<MyPoint> allPoints = new List<MyPoint>();
            for (int i = 0; i < numClasses; i++)
            {
                classPoints[i] = new List<MyPoint>();
            }

            // put all points into lists
            for (int i = 0; i < picheight; i++)
                for (int j = 0; j < picwidth; j++)
                {
                    MyPoint p1 = new MyPoint(imgClass3d[i, j], j, i);
                    List<MyPoint> plist = classPoints[imgClass3d[i, j]];
                    plist.Add(p1);
                    allPoints.Add(p1);
                }
            double[] avgIntraDist = new double[numClasses];
            double[] avgInterDist = new double[numClasses];
            double[] silhouette = new double[numClasses];
            MyPoint[] ClusterMeans = new MyPoint[numClasses];
            for (int k = 0; k < numClasses; k++)
            {
                // intra cluster means
                MyPoint p2 = new MyPoint();
                p2.X = (int)(from p in classPoints[k] select p.X).ToArray().Average();
                p2.Y = (int)(from p in classPoints[k] select p.Y).ToArray().Average();
                p2.ClusterId = k;
                ClusterMeans[k] = p2;
            }

            // cluster distance
            for (int l = 0; l < classPoints.Length; l++)
            {
                // intra cluster distance
                int[] intraX = (from p in classPoints[l] select (int)p.X).ToArray();
                int[] intraY = (from p in classPoints[l] select (int)p.Y).ToArray();
                for (int i = 0; i < intraX.Length; i++)
                {
                    avgIntraDist[l] += Math.Sqrt(Math.Pow(ClusterMeans[l].X - intraX[i], 2) + Math.Pow(ClusterMeans[l].Y - intraY[i], 2));
                }
                avgIntraDist[l] = avgIntraDist[l] / intraX.Length;

                // inter cluster distance
                int[] interX = (from p in allPoints where p.ClusterId != l select (int)p.X).ToArray();
                int[] interY = (from p in allPoints where p.ClusterId != l select (int)p.Y).ToArray();
                for (int i = 0; i < interX.Length; i++)
                {
                    avgInterDist[l] += Math.Sqrt(Math.Pow(ClusterMeans[l].X - interX[i], 2) + Math.Pow(ClusterMeans[l].Y - interY[i], 2));
                }
                avgInterDist[l] = avgInterDist[l] / interX.Length;

                //Calc silhouette
                silhouette[l] = (avgInterDist[l] - avgIntraDist[l]) / Math.Max(avgInterDist[l], avgIntraDist[l]);
            }
            return silhouette;
        }

        //public SwarmResult DoPSO() // Particle movement to achieve
        //{
        //    // for particle swarm optimization
        //    Gx = PList[0].Xx;
        //    Gy = PList[0].Xy;
        //    for (int i = 0; i < 1000; i++) // iterations
        //    {
        //        // find best position in the swarm
        //        Px = PList[0].Xx;
        //        Py = PList[0].Xy;
        //        foreach (Particle pt in PList)
        //        {
        //            if (Math.Abs(FunctionToSolve(pt.Xx, pt.Xy)) < Math.Abs(FunctionToSolve(Px, Py)))
        //            {
        //                Px = pt.Xx;
        //                Py = pt.Xy;
        //            }
        //        }

        //        if (Math.Abs(FunctionToSolve(Px, Py)) < Math.Abs(FunctionToSolve(Gx, Gy)))
        //        {
        //            Gx = Px;
        //            Gy = Py;
        //        }

        //        foreach (Particle pt in PList)
        //        {
        //            pt.UpdateVelocity(Px, Py, Gx, Gy);
        //            pt.UpdatePosition();
        //        }
        //    }

        //    SwarmResult sr = new SwarmResult
        //    {
        //        SwarmId = swarmNum,
        //        X = Gx,
        //        Y = Gy,
        //        FunctionValue = FunctionToSolve(Gx, Gy)
        //    };
        //    return sr;
        //}
    }
}