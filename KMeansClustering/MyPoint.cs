using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    class MyPoint : ICloneable
    {
        public int ClusterId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
