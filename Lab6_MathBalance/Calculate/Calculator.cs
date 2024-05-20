using Accord.Math.Optimization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6_MathBalance.Calculate
{
    public class Calculator : ICalculator
    {
        public List<double> CalcMathBalance(double[,] Ab, double[] x0, double[] errors, byte[] I, double[] lb, double[] ub, int maxIterations = 5000)
        {
            int n = errors.GetLength(0);
            int m = Ab.GetLength(0);
            double[] x = new double[n];
            // Формирование матрицы H
            double[,] H = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        //H = W * I, W = 1/error[i]^2
                        H[i, j] = I[i] / Math.Pow(errors[i], 2);
                    }
                    else
                    {
                        H[i, j] = 0.0;
                    }
                }
            }

            double[] d = new double[n];
            for (int iter = 0; iter < maxIterations; iter++)
            {
                for (int i = 0; i < n; i++)
                {
                    d[i] = 0.0;
                }

                // Формирование вектора d = -H * x0
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        d[i] += -H[i, j] * x0[j];
                    }
                }


                //QuadraticObjectiveFunction quadraticObjectiveFunction = new QuadraticObjectiveFunction(H, d);

                //var solver = new GoldfarbIdnani(quadraticObjectiveFunction, A, b);
                //solver.Minimize();
                //for (int i = 0; i < n; i++)
                //{
                //    x0[i] = solver.Solution[i];
                //}
                try
                {
                    // Переменные для алглиба
                    double[] s = new double[n]; // == 1
                    int[] ct = new int[m];// == 0
                    bool isupper = true;

                    for (int i = 0; i < n; i++) s[i] = 1;
                    for (int i = 0; i < m; i++) ct[i] = 0;


                    alglib.minqpstate state;
                    alglib.minqpreport rep;

                    //create solver
                    alglib.minqpcreate(n, out state);
                    alglib.minqpsetquadraticterm(state, H, isupper);
                    alglib.minqpsetlinearterm(state, d);
                    alglib.minqpsetlc(state, Ab, ct);

                    alglib.minqpsetbc(state, lb, ub);

                    // Set scale of the parameters.
                    alglib.minqpsetscale(state, s);

                    // Solve problem with the sparse interior-point method (sparse IPM) solver.
                    alglib.minqpsetalgosparseipm(state, 0.0);
                    alglib.minqpoptimize(state);
                    alglib.minqpresults(state, out x, out rep);

                }

                catch (alglib.alglibexception alglib_exception)
                {
                    System.Console.WriteLine("ALGLIB exception with message '{0}'", alglib_exception.msg);
                }
            }
            for (int i = 0; i < n; i++)
            {
                x0[i] = x[i];
            }
            for (int i = 0; i < n; i++)
                x[i] = Math.Round(x[i], 3);
            return x.ToList();
    }
    }
}
