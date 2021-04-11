using System;
using System.Collections.Generic;

namespace GMM
{
    public class SwarmSystem
    {
        public SwarmSystem(int snum, int pNum = 50)
        {
            this.swarmNum = snum;
            this.particleNum = pNum;
        }

        private int swarmNum;
        public int SwarmNum
        {
            get => swarmNum;
            //set => swarmNum = value;
        }

        private int particleNum;
        public int ParticleNum => particleNum;

        public List<Particle> PList = new List<Particle>();
        
        public double Px { get; set; }
        public double Py { get; set; }
        public double Gx { get; set; }
        public double Gy { get; set; }

        public void Initialize()
        {
            Random ran = new Random();
            for (int i = 0; i < particleNum; i++) //50 particles in swarm by default
            {
                Particle p = new Particle();
                p.W = 0.73;
                p.C1 = 1.4;
                p.C2 = 1.5;
                p.Xx = ran.NextDouble() * 20.0;
                p.Xy = ran.NextDouble() * 20.0;
                double num = ran.NextDouble();
                if (num > 0.5)
                {
                    p.Xx = -1.0 * p.Xx;
                    p.Xy = -1.0 * p.Xy;
                }
                p.Vx = ran.NextDouble() * 5.0;
                p.Vy = ran.NextDouble() * 5.0;
                num = ran.NextDouble();
                if (num > 0.5)
                {
                    p.Vx = -1.0 * p.Vx;
                    p.Vy = -1.0 * p.Vy;
                }
                PList.Add(p);
            }
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

        public SwarmResult DoPSO() // Particle movement to achieve
        {
            // for particle swarm optimization
            Gx = PList[0].Xx;
            Gy = PList[0].Xy;
            for (int i = 0; i < 1000; i++) // iterations
            {
                // find best position in the swarm
                Px = PList[0].Xx;
                Py = PList[0].Xy;
                foreach (Particle pt in PList)
                {
                    if (Math.Abs(FunctionToSolve(pt.Xx, pt.Xy)) < Math.Abs(FunctionToSolve(Px, Py)))
                    {
                        Px = pt.Xx;
                        Py = pt.Xy;
                    }
                }

                if (Math.Abs(FunctionToSolve(Px, Py)) < Math.Abs(FunctionToSolve(Gx, Gy)))
                {
                    Gx = Px;
                    Gy = Py;
                }

                foreach (Particle pt in PList)
                {
                    pt.UpdateVelocity(Px, Py, Gx, Gy);
                    pt.UpdatePosition();
                }
            }

            SwarmResult sr = new SwarmResult
            {
                SwarmId = swarmNum,
                X = Gx,
                Y = Gy,
                FunctionValue = FunctionToSolve(Gx, Gy)
            };
            return sr;
        }
    }
}