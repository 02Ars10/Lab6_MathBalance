using Lab6_MathBalance.Calculate;
using Lab6_MathBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lab6_Test
{
    public class CalculateTests
    {
        [Fact]
        public void Solver_AXequalsB_True()
        {
            InputData inputData = new InputData();

            int maxIter = 1000;

            double[,]Ab = new double[,]
            {
                {1,  -1,   -1,    0,   0,  0,    0},
                {0,   0,    1,    -1, -1,  0,    0},
                {0,   0,    0,    0,   1,  -1,  -1},
            };
 

            double[] x0 = new double[] { 10.005, 3.033, 6.831, 1.985, 5.093, 4.057, 0.991 };
            double[] tols = new double[] { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020 };
            byte[] isMEas = new byte[] { 1, 1, 1, 1, 1, 1, 1 };
            double[] downBorder = new double[] { 0, 0, 0, 0, 0, 0, 0 };
            double[] topBorder = new double[] { 10000, 10000, 10000, 10000, 10000, 10000, 10000 };

            ICalculator calculatorBalance = new Calculator();

            List<double> x = new List<double>();
            x = calculatorBalance.CalcMathBalance(Ab, x0, tols, isMEas, downBorder, topBorder, maxIter);

            bool flag = true;

            double sum = 0.0;
            for (int i = 0; i < Ab.GetLength(0); i++)
            {
                sum = 0.0;
                for (int j = 0; j < Ab.GetLength(1); j++)
                {
                    sum += Ab[i, j] * x[j];
                }
                if (Math.Round(sum, 2) != 0)
                {
                    flag = false;
                    break;
                }
            }

            Assert.True(flag);
        }


        [Fact]
        public void Solver_AXequalsODD_Var_True()
        {
            InputData inputData = new InputData();

            int maxIter = 1000;

            double [,]Ab = new double[,]
            {
                {1,  -1,   -1,    0,   0,  0,   0,  -1},
                {0,   0,    1,   -1,  -1,  0,   0,   0},
                {0,   0,    0,    0,   1,  -1,  -1,  0},
            };
 

            double[] x0 = new double[] { 10.005, 3.033, 6.831, 1.985, 5.093, 4.057, 0.991, 6.667 };
            double[] tols = new double[] { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020, 0.667 };
            byte[]isMEas = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1 };
            double[] downBorder = new double[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] topBorder = new double[] { 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000 };

            ICalculator calculatorBalance = new Calculator();



            List<double> x = new List<double>();
            x = calculatorBalance.CalcMathBalance(Ab, x0, tols, isMEas, downBorder, topBorder, maxIter);

            bool flag = true;

            double sum = 0.0;
            for (int i = 0; i < Ab.GetLength(0); i++)
            {
                sum = 0.0;
                for (int j = 0; j < Ab.GetLength(1); j++)
                {
                    sum += Ab[i, j] * x[j];
                }
                if (Math.Round(sum, 6) != 0)
                {
                    flag = false;
                    break;
                }
            }

            Assert.True(flag);
        }


        [Fact]
        public void Solver_X1Equals10X2_True()
        {
            InputData inputData = new InputData();

            int maxIter = 1000;

            double [,] Ab = new double[,]
            {
                {1,  -1,   -1,    0,   0,  0,   0,  -1},
                {0,   0,    1,   -1,  -1,  0,   0,   0},
                {0,   0,    0,    0,   1,  -1,  -1,  0},
                {1,  -10,   0,    0,   0,  0,   0,   0}
            };

            double[] x0 = new double[] { 10.005, 3.033, 6.831, 1.985, 5.093, 4.057, 0.991, 6.667 };
            double[] tols = new double[] { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020, 0.667 };
            byte[]isMEas = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1 };
            double[] downBorder = new double[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] topBorder = new double[] { 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000 };

            ICalculator calculatorBalance = new Calculator();



            List<double> x = new List<double>();
            x = calculatorBalance.CalcMathBalance(Ab, x0, tols, isMEas, downBorder, topBorder, maxIter);

            bool flag = true;

            double sum = 0.0;
            for (int i = 0; i < Ab.GetLength(0); i++)
            {
                sum = 0.0;
                for (int j = 0; j < Ab.GetLength(1); j++)
                {
                    sum +=  Ab[i, j] * x[j];
                }
                if (Math.Round(sum, 6) != 0)
                {
                    flag = false;
                    break;
                }
            }

            if (Math.Round(x[0], 5) != 10 * Math.Round(x[1], 6))
            {
                flag = false;
            }

            Assert.True(flag);
        }
    }
}
