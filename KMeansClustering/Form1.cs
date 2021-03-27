using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMeansClustering
{
    public partial class Form1 : Form
    {
        List<MyPoint> PList = null;

        public Form1()
        {
            InitializeComponent();
            PList = new List<MyPoint>();
        }

        private void btnLoadDataFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = @"S:\Users\Jkara\OneDrive\Documents\CPSC_590-AM\Assignments_Workspace\CPSC_590_Assignment7\Data";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    PList.Clear();
                    string dataFileName = ofd.FileName;
                    FileInfo fi = new FileInfo(dataFileName);
                    Stream strm = fi.Open(FileMode.Open, FileAccess.Read);
                    StreamReader strmr = new StreamReader(strm);
                    string sline = null;
                    char[] seps = { ',', ' ' }; // for comma or space separated list of coordinates
                                                // read each line and store the point coordinates in CList
                    sline = strmr.ReadLine();
                    while (sline != null)
                    {
                        string[] parts = sline.Split(seps);
                        MyPoint pt = new MyPoint();
                        pt.ClusterId = 0;
                        int ptnum = int.Parse(parts[0]); // point number - ignored
                        pt.X = double.Parse(parts[1]); // X coordinate value
                        pt.Y = double.Parse(parts[2]); // Y coordinate value
                        PList.Add(pt);
                        sline = strmr.ReadLine();
                    }
                    strmr.Close();
                    MessageBox.Show("Data File read, total points = " + PList.Count.ToString());
                    MyImageProc.DrawClusters(pic1, PList, 1.0, 1);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDoKMeans_Click(object sender, EventArgs e)
        {
            try
            {
                if (PList.Count == 0)
                    throw new Exception("No points data exists...");
                int numClusters = int.Parse(txtNumOfClusters.Text);
                List<ClusterCenterPoint> CList = null;
                KMeans.DoKMeans(numClusters, ref PList, ref CList, 0.1, 1000);
                MyImageProc.DrawClusters(pic1, PList, 1.0, numClusters);
                txtResult.Text = ComputeAndShowVarianceResults(numClusters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDoKMeansPlus_Click(object sender, EventArgs e)
        {
            try
            {
                if (PList.Count == 0)
                    throw new Exception("no points data exists...");
                int numClusters = int.Parse(txtNumOfClusters.Text);
                List<ClusterCenterPoint> CList = null;
                KMeans.DoKMeans(numClusters, ref PList, ref CList, 0.1, 1000, true); // true = do kmeans++
                MyImageProc.DrawClusters(pic1, PList, 1.0, numClusters);
                txtResult.Text = ComputeAndShowVarianceResults(numClusters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnKMeansPlusMinVar_Click(object sender, EventArgs e)
        {
            try
            {
                if (PList.Count == 0)
                    throw new Exception("no points data exists...");
                int numClusters = int.Parse(txtNumOfClusters.Text);
                List<ClusterCenterPoint> CList = null;
                KMeans.DoKMeansWithMinVariance(numClusters, ref PList, ref CList, 0.1, 1000, true); // true = do kmeans++
                MyImageProc.DrawClusters(pic1, PList, 1.0, numClusters);
                txtResult.Text = ComputeAndShowVarianceResults(numClusters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInitializeData_Click(object sender, EventArgs e)
        {
            PList.Clear();
            Random rand = new Random();
            int dataLength = 5000; // number of data points
                                   // create 4 distributions with different means and std devs
            double meanx0 = 150;
            double meanx1 = 250;
            double meanx2 = 375;
            double meanx3 = 475;
            double meany0 = 175;
            double meany1 = 250;
            double meany2 = 350;
            double meany3 = 450;
            double stddevx0 = 240;
            double stddevx1 = 270;
            double stddevx2 = 220;
            double stddevx3 = 260;
            double stddevy0 = 250;
            double stddevy1 = 240;
            double stddevy2 = 280;
            double stddevy3 = 245;
            int index = 0;
            for (int i = 0; i < dataLength / 4; i++)
            {
                MyPoint pt = new MyPoint();
                double rnum = rand.NextDouble();
                if (rnum < 0.5)
                    pt.X = rand.NextDouble() * stddevx0 / 2 + meanx0;
                else
                    pt.X = -1 * rand.NextDouble() * stddevx0 / 2 + meanx0;
                if (rnum < 0.5)
                    pt.Y = rand.NextDouble() * stddevy0 / 2 + meany0;
                else
                    pt.Y = -1 * rand.NextDouble() * stddevy0 / 2 + meany0;
                index++;
                PList.Add(pt);
            }
            for (int i = 0; i < dataLength / 4; i++)
            {
                MyPoint pt = new MyPoint();
                double rnum = rand.NextDouble();
                if (rnum < 0.5)
                    pt.X = rand.NextDouble() * stddevx1 / 2 + meanx1; 
                else
                    pt.X = -1 * rand.NextDouble() * stddevx1 / 2 + meanx1;
                if (rnum < 0.5)
                    pt.Y = rand.NextDouble() * stddevy1 / 2 + meany1;
                else
                    pt.Y = -1 * rand.NextDouble() * stddevy1 / 2 + meany1;
                index++;
                PList.Add(pt);
            }
            for (int i = 0; i < dataLength / 4; i++)
            {
                double rnum = rand.NextDouble();
                MyPoint pt = new MyPoint();
                if (rnum < 0.5)
                    pt.X = rand.NextDouble() * stddevx2 / 2 + meanx2;
                else
                    pt.X = -1 * rand.NextDouble() * stddevx2 / 2 + meanx2;
                if (rnum < 0.5)
                    pt.Y = rand.NextDouble() * stddevy2 / 2 + meany2;
                else
                    pt.Y = -1 * rand.NextDouble() * stddevy2 / 2 + meany2;
                PList.Add(pt);
                index++;
            }
            for (int i = 0; i < dataLength / 4; i++)
            {
                double rnum = rand.NextDouble();
                MyPoint pt = new MyPoint();
                if (rnum < 0.5)
                    pt.X = rand.NextDouble() * stddevx3 / 2 + meanx3;
                else
                    pt.X = -1 * rand.NextDouble() * stddevx3 / 2 + meanx3;
                if (rnum < 0.5)
                    pt.Y = rand.NextDouble() * stddevy3 / 2 + meany3;
                else
                    pt.Y = -1 * rand.NextDouble() * stddevy3 / 2 + meany3;
                index++;
                PList.Add(pt);
            }
            MyImageProc.DrawClusters(pic1, PList, 1.0, 1);
        }

        string ComputeAndShowVarianceResults(int numClusters)
        {
            // ----compute variance of cluster memberships
            int[] CCount = new int[numClusters];
            for (int i = 0; i < numClusters; i++)
                CCount[i] = 0;
            foreach (MyPoint mp in PList)
                CCount[mp.ClusterId] += 1;
            double variance = 0;
            for (int i = 0; i < numClusters; i++)
                variance += Math.Pow((CCount[i] - PList.Count / (double)numClusters), 2);
            double stddev = Math.Sqrt(variance);

            string out1 = "Std Dev = " + string.Format("{0:f2}", stddev) + "\r\n";
            for (int n = 0; n < CCount.Length; n++)
                out1 += "Cluster #" + n.ToString() + " count = " + CCount[n].ToString() + "\r\n";
            return out1;
        }
    }
}
