﻿using Mapack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GMM
{
    class GMM_NDim
    {
        public Matrix X { get; set; }
        int k;  // number of clusters or mixture components
        int dim; // number of dimensions for data
        public Matrix[] mu = null;
        public Matrix[] sigma = null;
        double[,] pdf = null;
        public double[,] Gamma = null;
        double[] phi = null;
        public GMM_NDim(int k, int dim, Matrix x)
        {
            this.k = k;
            this.dim = dim;
            this.X = x;
            mu = new Matrix[k];  // mean for cluster k
            for (int i = 0; i < k; i++)
                mu[i] = new Matrix(1, dim);

            sigma = new Matrix[k];   // cov matrix for cluster k
            for (int i = 0; i < k; i++)
                sigma[i] = new Matrix(dim, dim);

            pdf = new double[x.Rows, k];
            // calculated pdf for each data point based on mean and var for cluster k

            Gamma = new double[x.Rows, k];
            // probablity matrix for each data point belonging to cluster k
            // i.e., probablity that a data point belongs to cluster i

            phi = new double[k];    // prior probabilities for each cluster
        }

        public void ComputeGMM_ND()
        {
            Random rand = new Random();
            // ----------Initialization step - randomly select k data poits to act as means
            List<int> RList = new List<int>();
            for (int i = 0; i < k; i++)
            {
                int rpos = rand.Next(X.Rows);
                if (RList.Contains(rpos))
                    rpos = rand.Next(X.Rows);
                for (int m = 0; m < dim; m++)
                    mu[i][0, m] = X[rpos,m];
            }

            // set the variance of each cluster to be the overall variance
            Matrix varianceOfData = ComputeCoVariance(X);
            for (int i = 0; i < varianceOfData.Rows; i++)
            {
                for (int j = 0; j < varianceOfData.Columns; j++)
                    varianceOfData[i, j] = varianceOfData[i, j] ;// Math.Sqrt(varianceOfData[i, j]);
            }
            for (int i = 0; i < k; i++)
            {
                sigma[i] = varianceOfData.Clone();  
            }

            // set prior probablities of each cluster to be uniform
            for (int i = 0; i < k; i++)
            {
                phi[i] = 1.0 / k;
            }
            //--------------------------end initialization-------------------------------------

            // ---------------------------Expectation Maximization------------------------------
            for (int n = 0; n < 1000; n++)
            {
                ManualResetEvent[,] pdfBlocker = new ManualResetEvent[X.Rows, k];
                ManualResetEvent[] GdenomBlocker = new ManualResetEvent[X.Rows];
                ManualResetEvent[,] GammaKBlocker = new ManualResetEvent[X.Rows, k];
                ManualResetEvent[] PhiBlocker = new ManualResetEvent[X.Rows];

                Parallel.For(0, X.Rows, (x) =>
                {
                    for (int y = 0; y < k; y++)
                    {
                        pdfBlocker[x, y] = new ManualResetEvent(false);
                        pdfBlocker[x, y].Reset();
                        GammaKBlocker[x, y] = new ManualResetEvent(false);
                    }
                    GdenomBlocker[x] = new ManualResetEvent(false);
                    PhiBlocker[x] = new ManualResetEvent(false);
                });

                List<Task> taskList = new List<Task>();

                #region perform Expectation step
                //---------perform Expectation step---------------------
                taskList.Add(Task.Factory.StartNew(
                    () =>
                    {
                        // Make Parallel.for
                        for (int i = 0; i < X.Rows; i++)
                        {
                            for (int k1 = 0; k1 < k; k1++)
                            {
                                pdf[i, k1] = GaussianMV(X, i, dim, mu[k1], sigma[k1]);
                                pdfBlocker[i, k1].Set();
                            }
                        }
                    }));


                double[] Gdenom = new double[X.Rows];
                taskList.Add(Task.Factory.StartNew(
                    () =>
                    {
                        // Make Parallel.for
                        for (int i = 0; i < X.Rows; i++) // denominator for Gamma
                        {
                            double sum = 0;
                            for (int k1 = 0; k1 < k; k1++)
                            {
                                pdfBlocker[i, k1].WaitOne();
                                sum = sum + phi[k1] * pdf[i, k1];
                            }
                            Gdenom[i] = sum;
                            GdenomBlocker[i].Set();
                        }
                    }));

                taskList.Add(Task.Factory.StartNew(
                    () =>
                    {
                        // Make Parallel.for
                        for (int i = 0; i < X.Rows; i++)
                        {
                            GdenomBlocker[i].WaitOne();
                            for (int k1 = 0; k1 < k; k1++)
                            {
                                pdfBlocker[i, k1].WaitOne();
                                Gamma[i, k1] = (phi[k1] * pdf[i, k1]) / Gdenom[i];
                                GammaKBlocker[i, k1].Set();
                            }
                        }
                    }));


                
                //for (int i = 0; i < X.Rows; i++)
                //{
                //    for (int k1 = 0; k1 < k; k1++)
                //    {
                //        pdf[i, k1] = GaussianMV(X, i, dim, mu[k1], sigma[k1]);
                //    }
                //}

                //for (int i = 0; i < X.Rows; i++) // denominator for Gamma
                //{
                //    double sum = 0;
                //    for (int k1 = 0; k1 < k; k1++)
                //    {
                //        sum = sum + phi[k1] * pdf[i, k1];
                //    }
                //    Gdenom[i] = sum;
                //}

                //for (int i = 0; i < X.Rows; i++)
                //{
                //    for (int k1 = 0; k1 < k; k1++)
                //    {
                //        Gamma[i, k1] = (phi[k1] * pdf[i, k1]) / Gdenom[i];
                //    }
                //}

                //-------------------end Expectation--------------------
                #endregion

                #region perform Maximization Step
                //---------perform Maximization Step--------------------
                //----------update phi--------------
                for (int k1 = 0; k1 < k; k1++)
                {
                    double sum = 0;
                    for (int i = 0; i < X.Rows; i++)
                    {
                        GammaKBlocker[i, k1].WaitOne();
                        sum += Gamma[i, k1];
                    }
                    phi[k1] = sum / (X.Rows);
                    PhiBlocker[k].Set();
                }
                //for (int k1 = 0; k1 < k; k1++)
                //{
                //    double sum = 0;
                //    for (int i = 0; i < X.Rows; i++)
                //    {
                //        sum += Gamma[i, k1];
                //    }
                //    phi[k1] = sum / (X.Rows);
                //}
                //---------------------------------
                #endregion

                #region update mu
                //-------------update mu-----------
                ManualResetEvent[,] MuNumberBlocker = new ManualResetEvent[k, dim];
                ManualResetEvent[] MuDenomBlocker = new ManualResetEvent[k];
                ManualResetEvent[,] MuBlocker = new ManualResetEvent[k, dim];

                // Left off here. Need to do tasks.
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        MuNumberBlocker[i, j] = new ManualResetEvent(false);
                        MuBlocker[i, j] = new ManualResetEvent(false);
                    }
                    MuDenomBlocker[i] = new ManualResetEvent(false);
                }
                double[,] MuNumer = new double[k, dim];
                for (int k1 = 0; k1 < k; k1++)
                {
                    double[] sum = new double[dim];
                    for (int i = 0; i < X.Rows; i++)
                    {
                        for (int m =0; m < dim; m++)
                        {
                            GammaKBlocker[i, k1].WaitOne();
                            sum[m] += Gamma[i, k1] * X[i, m];
                        }
                    }
                    for (int m = 0; m < dim; m++)
                    {
                        MuNumer[k1, m] = sum[m];
                        MuNumberBlocker[k1, m].Set();
                    }
                }

                double[] MuDenom = new double[k];
                for (int k1 = 0; k1 < k; k1++)
                {
                    double sum = 0;
                    for (int i = 0; i < X.Rows; i++)
                    {
                        GammaKBlocker[i, k1].WaitOne();
                        sum += Gamma[i, k1];
                    }
                    MuDenom[k1] = sum;
                    MuDenomBlocker[k1].Set();
                }
                for (int i = 0; i < k; i++)
                {
                    MuDenomBlocker[i].WaitOne();
                    for (int m = 0; m < dim; m++)
                    {
                        MuNumberBlocker[i, m].WaitOne();
                        mu[i][0, m] = MuNumer[i, m] / MuDenom[i];
                        MuBlocker[k, m].Set();
                    }
                }
                //-----------------------------------
                #endregion

                #region update sigma
                //-------------update sigma----------
                Matrix[] VarianceNumer = new Matrix[k];
                for (int k1 = 0; k1 < k; k1++)
                {
                    Matrix sum = new Matrix(dim, dim);

                    for (int i = 0; i < X.Rows; i++)
                    {
                        Matrix xi = new Matrix(1, dim);
                        for (int m = 0; m < dim; m++)
                            xi[0, m] = X[i, m]; 
                        sum += ((xi - mu[k1]).Transpose() * (xi - mu[k1])) * Gamma[i, k1];
                    }
                    VarianceNumer[k1] = sum;
                }
                for (int i = 0; i < k; i++)
                    sigma[i] = VarianceNumer[i] * (1 / MuDenom[i]);
                //--------------end update Sigma--------
                #endregion

                foreach (Task t in taskList)
                {
                    if (t.Status == TaskStatus.Running)
                        t.Wait();
                }
                //---------------end Maximization-------------------------------
            }
            var G = Gamma;
        }

        public Matrix ComputeCoVariance(Matrix data)
        {
            Matrix data2 = data.Clone();
            double[] sum = new double[dim];
            for (int i = 0; i < data.Rows; i++)
            {
                for (int m = 0; m < dim; m++)
                    sum[m] += data[i, m];
            }
            for (int i = 0; i < data.Rows; i++)
            {
                for (int m = 0; m < dim; m++)
                    data2[i, m] -= (sum[m] / data.Rows);
            }
            Matrix dt = (data2.Transpose() * data2);
            for (int i = 0; i < dt.Rows; i++)
            {
                for (int m = 0; m < dim; m++)
                    dt[i, m] /= data.Rows - 1;
            }
            return dt;
        }

        public double GaussianMV(Matrix xdata, int index, int dim, Matrix mean, Matrix cov)  // n-D gaussian
        {
            Matrix xi = new Matrix(1, dim);
            for (int i = 0; i < dim; i++)
                xi[0, i] = xdata[index, i];  
            var exp = (xi - mean) * cov.Inverse * (xi - mean).Transpose();
            var exp2 = exp[0, 0] * -0.5;
            double res = 1 / (Math.Sqrt(cov.Determinant) * Math.Sqrt(Math.Pow(2.0 * Math.PI, dim))) *
                Math.Exp(exp2);
            return res;
        }
    }
}
