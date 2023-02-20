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

        [HttpPost]
        public async Task<ActionResult> Calculate(CalculationModel model)
        {
            double result;
            switch (model.SelectedOperation)
            {
                case "add":
                    result = Convert.ToDouble(model.FirstNumber) + Convert.ToDouble(model.SecondNumber);
                    break;
                case "subtract":
                    result = Convert.ToDouble(model.FirstNumber) - Convert.ToDouble(model.SecondNumber);
                    break;
                case "multiply":
                    result = Convert.ToDouble(model.FirstNumber) * Convert.ToDouble(model.SecondNumber);
                    break;
                case "divide":
                    result = Convert.ToDouble(model.FirstNumber) == 0 ? throw new ArgumentException("Cannot divide by zero") : Convert.ToDouble(model.FirstNumber) / Convert.ToDouble(model.SecondNumber);
                    break;
                default:
                    throw new ArgumentException("Invalid operation");
            }
            
            var transactionService = new ConnectDB("mongodb://localhost:27017", "ASPDB");
            await transactionService.StoreTransaction((int)Convert.ToDouble(model.FirstNumber), (int)Convert.ToDouble(model.SecondNumber), model.SelectedOperation, result);

            return Ok(result);
        }
    }
}
   