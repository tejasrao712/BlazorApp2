using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using BlazorApp2.Client.Models;
using BlazorApp2.Server.Database;

namespace BlazorApp2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalcAPIController : ControllerBase
    {

        [HttpPost("calculate")]
        public async Task<ActionResult> Calculate(CalculationModel request)
        {
            int result;
            switch (request.SelectedOperation)
            {
                case "add":
                    result = request.FirstNumber + request.SecondNumber;
                    break;
                case "subtract":
                    result = request.FirstNumber - request.SecondNumber;
                    break;
                case "multiply":
                    result = request.FirstNumber * request.SecondNumber;
                    break;
                case "divide":
                    result = request.SecondNumber == 0 ? throw new ArgumentException("Cannot divide by zero") : request.FirstNumber / request.SecondNumber;
                    break;
                default:
                    throw new ArgumentException("Invalid operation");
            }
            
            var transactionService = new ConnectDB("","");
            await transactionService.StoreTransaction(request.FirstNumber, request.SecondNumber, request.SelectedOperation, result);

            return Ok(result);
        }
        public class CalculatorTransaction
        {
            public int FirstNumber { get; set; }
            public int SecondNumber { get; set; }
            public string Operation { get; set; }
            public int Result { get; set; }
            public DateTime CreatedOn { get; set; }
        }
    }
}
   