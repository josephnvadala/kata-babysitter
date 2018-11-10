using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KataBabysitter.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KataBabysitter.API
{
    [Produces("application/json")]
    [Route("api/IncomeCalculation")]
    public class IncomeCalculationController : Controller
    {        
        // POST: api/IncomeCalculation
        [HttpPost]
        public void Post(ShiftInformation shiftInformation)
        {
            var information = shiftInformation;
        }

    }
}
