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

namespace KMedoidsClustering
{
    public partial class Form1 : Form
    {
        List<Point> PList = null;
        KMedoid<Point> km = null;

        public Form1()
        {
            InitializeComponent();
            PList = new List<Point>();
        }

        private void btnLoadDataPoints_Click(object sender, EventArgs e)
        {
            try
            {
                PList.Clear();
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = @"S:\Users\Jkara\OneDrive\Documents\CPSC_590-AM\Assignments_Workspace\CPSC_590_Assignment7\Data";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filename = ofd.FileName;
                    FileInfo fi = new FileInfo(filename);
                    StreamReader sr = new StreamReader(fi.Open(FileMode.Open, FileAccess.Read));
                    string sline = sr.ReadLine();
                    while (sline != null)
                    {
                        string[] parts = sline.Split(new char[] { ',', ' ' });
                        Point pt = new Point(int.Parse(parts[1]), int.Parse(parts[2]));
                        PList.Add(pt);
                        sline = sr.ReadLine();
                    }
                    sr.Close();
                    MessageBox.Show("Data read for " + PList.Count.ToString() + " points");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDoKMedoidClustering_Click(object sender, EventArgs e)
        {
            try
            {
                // define distance function
                Func<Point, Point, double> euclidean = (a, b) => Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
                km = new KMedoid<Point>(euclidean);
                int k = int.Parse(txtNumClusters.Text);
                km.Compute(k, PList);
                Visualization.DrawClusters(pic1, PList, km.Clusters, k, km._medoids, 1);
                txtResult.Text = ComputeAndShowVarianceResults(k);

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
            int dataLength = 1000; // number of data points.
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
                Point pt = new Point();
                double rnum = rand.NextDouble();
                if (rnum < 0.5)
                    pt.X = (int)(rand.NextDouble() * stddevx0 / 2 + meanx0);
                else
                    pt.X = (int)(-1 * rand.NextDouble() * stddevx0 / 2 + meanx0);
                if (rnum < 0.5)
                    pt.Y = (int)(rand.NextDouble() * stddevy0 / 2 + meany0);
                else
                    pt.Y = (int)(-1 * rand.NextDouble() * stddevy0 / 2 + meany0);
                index++;
                PList.Add(pt);
            }

            for (int i = 0; i < dataLength / 4; i++)
            {
                Point pt = new Point();
                double rnum = rand.NextDouble();
                if (rnum < 0.5)
                    pt.X = (int)(rand.NextDouble() * stddevx1 / 2 + meanx1);
                else
                    pt.X = (int)(-1 * rand.NextDouble() * stddevx1 / 2 + meanx1);
                if (rnum < 0.5)
                    pt.Y = (int)(rand.NextDouble() * stddevy1 / 2 + meany1);
                else
                    pt.Y = (int)(-1 * rand.NextDouble() * stddevy1 / 2 + meany1);
                index++;
                PList.Add(pt);
            }

            for (int i = 0; i < dataLength / 4; i++)
            {
                double rnum = rand.NextDouble();
                Point pt = new Point();
                if (rnum < 0.5)
                    pt.X = (int)(rand.NextDouble() * stddevx2 / 2 + meanx2);
                else
                    pt.X = (int)(-1 * rand.NextDouble() * stddevx2 / 2 + meanx2);
                if (rnum < 0.5)
                    pt.Y = (int)(rand.NextDouble() * stddevy2 / 2 + meany2);
                else
                    pt.Y = (int)(-1 * rand.NextDouble() * stddevy2 / 2 + meany2);
                PList.Add(pt);
                index++;
            }

            for (int i = 0; i < dataLength / 4; i++)
            {
                double rnum = rand.NextDouble();
                Point pt = new Point();
                if (rnum < 0.5)
                    pt.X = (int)(rand.NextDouble() * stddevx3 / 2 + meanx3);
                else
                    pt.X = (int)(-1 * rand.NextDouble() * stddevx3 / 2 + meanx3);
                if (rnum < 0.5) 
                    pt.Y = (int)(rand.NextDouble() * stddevy3 / 2 + meany3);
                else
                    pt.Y = (int)(-1 * rand.NextDouble() * stddevy3 / 2 + meany3);
                index++;
                PList.Add(pt);
            }

            MessageBox.Show("Data initialized for " + PList.Count + " points");
        }

        private string ComputeAndShowVarianceResults(int numClusters)
        {
            // ----compute variance of cluster memberships
            int[] CCount = new int[numClusters];
            for (int i = 0; i < numClusters; i++)
                CCount[i] = 0;
            foreach (int clusterNum in km.Clusters)
                CCount[clusterNum] += 1;
            double variance = 0;
            for (int i = 0; i < numClusters; i++)
                variance += Math.Pow((CCount[i] - (PList.Count) / (double)numClusters), 2);
            double stddev = Math.Sqrt(variance);

            string out1 = "Std Dev = " + string.Format("{0:f2}", stddev) + "\r\n";
            for (int n = 0; n < CCount.Length; n++)
                out1 += "Cluster # " + n.ToString() + " count = " + CCount[n].ToString() + "\r\n";
            return out1;
        }

        private void btnRedrawClusters_Click(object sender, EventArgs e)
        {
            try
            {
                
                Visualization.DrawClusters(pic1, PList, km.Clusters, int.Parse(txtNumClusters.Text), km._medoids, 0.5);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
