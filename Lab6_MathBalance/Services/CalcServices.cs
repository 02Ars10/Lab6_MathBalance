using Lab6_MathBalance.Calculate;
using Lab6_MathBalance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lab6_MathBalance.Services
{
    public class CalcServices : ICalcServices
    {
        public DataOut Calculate(InputData inputData)
        {
            if (inputData.Flows.Count == 0)
            {
                throw new ValidationException("Список пуст!");
            }


            Calculator calculator = new Calculator();
            DataOut outputData = new DataOut();
            outputData.Flows = new List<PotokOut>();
           

            double[] x0 = new double[inputData.Flows.Count];
            double[] errors = new double[inputData.Flows.Count];
            byte[] I = new byte[inputData.Flows.Count];
            double[] lb = new double[inputData.Flows.Count];
            double[] ub = new double[inputData.Flows.Count];

            int i = 0;
            int size = 0;

            foreach (PotokInput flow in inputData.Flows.OrderBy(f => f.Id))
            {
                if (flow.LowerBound > flow.UpperBound)
                {
                    throw new ValidationException($"Верхняя граница меньше нижней в потоке {i + 1}!");
                }
                if (flow.Id != i + 1)
                {
                    throw new ValidationException($"Поток {i + 1} отсутствует!");
                }
                if (flow.Error < 0)
                {
                    throw new ValidationException($"Некорректное значение или ошибка {i + 1} потока");
                }
                outputData.Flows.Add(new PotokOut {  Name = flow.Name });
                // Заполнение данных, которые есть всегда
                x0[i] = flow.Value;
                errors[i] = flow.Error;
                if (flow.IsUsed)
                {
                    I[i] = 1;
                }
                else
                {
                    I[i] = 0;
                }

                lb[i] = flow.LowerBound;
                ub[i] = flow.UpperBound;

                // Размер матрицы A
                if (flow.DestId > size)
                {
                    size = flow.DestId;
                }

                i++;
            }

            double[,] Ab = new double[size, inputData.Flows.Count+1];
            i = 0;


            foreach (PotokInput flow in inputData.Flows.OrderBy(f => f.Id))
            {
                if (flow.SourceId != -1)
                {
                    Ab[(flow.SourceId - 1), i] = -1;
                }
                if (flow.DestId != -1)
                {
                    Ab[(flow.DestId - 1), i] = 1;
                }

                i++;
            }

            List<double> result = calculator.CalcMathBalance(Ab, x0, errors, I, lb, ub);

            i = 0;
            foreach (PotokInput flow in inputData.Flows.OrderBy(f => f.Id))
            {
                outputData.Flows[i].Value = result[i];
               

                i++;
            }
            outputData.IsBalanced = checkBalanced(Ab, result);
            return outputData;
    }
        public bool checkBalanced(double[,] Ab, List<double> result)
        {
            bool isAppropriate = true;

            double sum = 0;
            for (int i = 0; i < Ab.GetLength(0); i++)
            {
                sum = 0;
                for (int j = 0; j < Ab.GetLength(1) - 1; j++)
                {
                    sum += Ab[i, j] * result[j];
                }

                if (Math.Round(sum, 1) != 0)
                {
                    isAppropriate = false;
                    break;
                }
            }

            return isAppropriate;
        }
    }
}
