using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    class ClusterCenterPoint : ICloneable
    {
        public int ClusterID { get; set; }
        public double Cx { get; set; }
        public double Cy { get; set; }
        public List<MyPoint> PList { get; set; } // list of points belonging to this cluster
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
