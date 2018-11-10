using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KataBabysitter.Classes;
using KataBabysitter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KataBabysitter.API
{
    [Produces("application/json")]
    [Route("api/IncomeCalculation")]
    public class IncomeCalculationController : Controller
    {        
        [HttpGet]
        public async Task<IActionResult> myControllerAction(ShiftInformation shiftInformation)
        {
            var paymentCalculationService = new PaymentCalculationService();
            var income = paymentCalculationService.CalculateAsync(shiftInformation);
            await Task.WhenAll(income);

            var data = new
            {
                income
            };

            return Ok(data);
        }

    }
}
