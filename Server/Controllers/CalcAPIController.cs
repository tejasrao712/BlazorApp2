using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BlazorApp2.Server.Controllers

{
    public class CalcAPIController : ControllerBase
    {
        private readonly IMongoCollection<CalculatorTransaction> _transactions;

        public CalcAPIController(IMongoClient client)
        {
            var client1 = new MongoClient("mongodb://localhost:27017");
            var database = client1.GetDatabase("Calculator");
            _transactions = database.GetCollection<CalculatorTransaction>("Operations");
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<int>> Calculate(CalculatorRequest request)
        {
            int result;
            switch (request.Operation)
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

            // Store transaction in database
            var transaction = new CalculatorTransaction
            {
                FirstNumber = request.FirstNumber,
                SecondNumber = request.SecondNumber,
                Operation = request.Operation,
                Result = result,
                CreatedOn = DateTime.UtcNow
            };
            await _transactions.InsertOneAsync(transaction);

            return result;
        }
    }

    public class CalculatorRequest
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public string Operation { get; set; }
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