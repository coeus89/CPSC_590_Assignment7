using System;

namespace GMM
{
    public class SwarmResult : IComparable<SwarmResult>
    {
        public int SwarmId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double FunctionValue { get; set; }
        public override string ToString()
        {
            return "SwarmId:" + SwarmId.ToString() +
                   " X=" + X.ToString() +
                   " Y=" + Y.ToString() +
                   " Function Value=" + FunctionValue.ToString();
        }
        public int CompareTo(SwarmResult other)
        {
            return this.FunctionValue.CompareTo(other.FunctionValue);
        }

    }
}