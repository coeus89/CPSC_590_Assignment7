using System;

namespace GMM
{
    public class SwarmResult : IComparable<SwarmResult>, ICloneable
    {
        public int SwarmId { get; set; }
        public double silhouette { get; set; }
        public GMM_NDim MyGMM {get;set;}

        public object Clone()
        {
            var res = new SwarmResult
            {
                MyGMM = (GMM_NDim)this.MyGMM.Clone(),
                SwarmId = this.SwarmId,
                silhouette = this.silhouette
            };
            return res;
        }

        // public double FunctionValue { get; set; }
        public int CompareTo(SwarmResult other)
        {
            return (-1)*this.silhouette.CompareTo(other.silhouette); // I want the largest first
        }

    }
}