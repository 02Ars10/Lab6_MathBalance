using Lab6_MathBalance.Models;
using Lab6_MathBalance.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab6_MathBalance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private ICalcServices calculatorBalanceService;

        public BalanceController(ICalcServices calculatorBalanceService)
        {
            this.calculatorBalanceService = calculatorBalanceService;
        }

        [HttpPost]
        public ActionResult<DataOut> Post([FromBody] InputData inputData)
        {
            return calculatorBalanceService.Calculate(inputData);
        }
    }
}
