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
