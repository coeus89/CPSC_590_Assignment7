using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM
{
    public class MyPoint
    {
        //public MyPoint(int clusterId, double x, double y, int group)
        //{
        //    ClusterId = clusterId;
        //    X = x;
        //    Y = y;
        //    //this.group = group;
        //}

        public MyPoint(double x, double y)
        {
            ClusterId = -1;
            X = x;
            Y = y;
            //this.group = -1;
        }

        public MyPoint(double x, double y, int group)
        {
            ClusterId = -1;
            X = x;
            Y = y;
            //this.group = group;
        }

        public MyPoint()
        {
            ClusterId = -1;
            X = -1;
            Y = -1;
            //this.group = -1;
        }

        public int ClusterId { get; set; }
        public double X {get; set; }
        public double Y {get; set; }
        //public int group { get; set; }

        
    }
}
