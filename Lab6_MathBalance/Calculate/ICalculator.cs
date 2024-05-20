using System.Collections.Generic;

namespace Lab6_MathBalance.Calculate
{
    public interface ICalculator
    {
        public List<double> CalcMathBalance(double[,] Ab, double[] x0, double[] errors, byte[] I, double[] lb, double[] ub, int maxIterations = 5000);
    }
}
