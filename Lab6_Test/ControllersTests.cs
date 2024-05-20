using Lab6_MathBalance.Calculate;
using Lab6_MathBalance.Controllers;
using Lab6_MathBalance.Models;
using Lab6_MathBalance.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lab6_Test
{
    public class ControllersTests
    {
        [Fact]
        public void EmptyList_Error()
        {
            InputData balanceInput = new InputData();
            balanceInput.Flows = new List<PotokInput>();
            ICalcServices calculatorService = new CalcServices();
            var conroller = new BalanceController(calculatorService);


            Assert.Throws<ValidationException>(() => conroller.Post(balanceInput));
        }



        [Fact]
        public void Controller_NotNullResult()
        {
            InputData balanceInput = new InputData();

            balanceInput.Flows = new List<PotokInput>()
            {
               new PotokInput {Id = 1, Name = "x1", Value = 10.555, Error = 0.220, IsUsed = true, LowerBound = 0, UpperBound = 10000, SourceId = -1, DestId = 1 },
               new PotokInput {Id = 2, Name = "x2", Value = 10.025, Error = 0.106, IsUsed = true, LowerBound = 0, UpperBound = 10000, SourceId = 1, DestId = 2 },
               new PotokInput {Id = 3, Name = "x3", Value = 10.005, Error = 0.150, IsUsed = true, LowerBound = 0, UpperBound = 10000, SourceId = 2, DestId = -1 }
            };
            ICalcServices calculatorService = new CalcServices();
            var conroller = new BalanceController(calculatorService);


            var result = conroller.Post(balanceInput);


            Assert.NotNull(result);
        }
        [Fact]
        public void error_when_ub_less_then_lb()
        {
            InputData balanceInput = new InputData();

            balanceInput.Flows = new List<PotokInput>()
            {
               new PotokInput {Id = 1, Name = "x1", Value = 10.005, Error = 0.200, IsUsed = true, LowerBound = 100, UpperBound = 10, SourceId = -1, DestId = 1 }
            };



            ICalcServices calculatorService = new CalcServices();
            var conroller = new BalanceController(calculatorService);

           
            Assert.Throws<ValidationException>(() => conroller.Post(balanceInput));
        }

        [Fact]
        public void positive_big_data()
        {
            InputData data = new InputData();
            ICalcServices calculatorService = new CalcServices();
            var conroller = new BalanceController(calculatorService);

            var flows = new List<PotokInput>();
            int flowsCount = 100;

            for (int i = 0; i < flowsCount; i++)
            {

                int src = i + 1;
                int dst = i + 2;

                if (i == 0)
                {
                    src = -1;
                    dst = i + 1;
                }

                if (i == flowsCount - 1)
                {
                    src = i + 1;
                    dst = -1;
                }

                flows.Add(new PotokInput
                {
                    Id = i+1,
                    Name = $"x{i + 1}",
                    Value = flowsCount - i + 1,
                    Error = 0.200,
                    IsUsed = true,
                    LowerBound = 0,
                    UpperBound = 10000,
                    SourceId = src,
                    DestId = dst
                });
            }

            data.Flows = flows;

            var result = conroller.Post(data);
            Assert.NotNull(result);
        }

    }
}
