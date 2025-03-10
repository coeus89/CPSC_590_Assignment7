﻿using System;

namespace GMM
{
    public class Particle
    {
        public double W { get; set; } // inertia or weight
        public double C1 { get; set; } // cognitive social const
        public double C2 { get; set; }
        Random ran = new Random();
        // we will be solving a 2-D problem in x and y
        // so there will be two components to velocity and position
        public double Xx { get; set; } // poistion in x
        public double Xy { get; set; } // position in y
        public double Vx { get; set; } // velocity in x
        public double Vy { get; set; } // velocity in y
        public void UpdateVelocity(double Px, double Py,
            double Gx, double Gy)
        {
            // P is the current best position of any particle in the swarm
            // G is the global best position discovered so far
            Vx = W * Vx + C1 * ran.NextDouble() * (Px - Xx) +
                 C2 * ran.NextDouble() * (Gx - Xx);
            if (Vx > 5)
                Vx = 5;
            if (Vx < -5)
                Vx = -5;
            Vy = W * Vy + C1 * ran.NextDouble() * (Py - Xy) +
                 C2 * ran.NextDouble() * (Gy - Xy);
            if (Vy > 5)
                Vy = 5;
            if (Vy < -5)
                Vy = -5;
        }
        public void UpdatePosition()
        {
            Xx = Xx + Vx;
            // we need to put some bounds on the position
            if (Xx > 20)
                Xx = 20;
            if (Xx < -20)
                Xx = -20;
            Xy = Xy + Vy;
            if (Xy > 20)
                Xy = 20;
            if (Xy < -20)
                Xy = -20;
        }
    }
}