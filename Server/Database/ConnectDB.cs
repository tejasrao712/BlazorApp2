using Amazon.Runtime.Internal;
using BlazorApp2.Server.Controllers;
using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using BlazorApp2.Client.Models;

namespace BlazorApp2.Server.Database
{
    public class ConnectDB
    {
        private readonly IMongoCollection<Transaction> _transactions;

        public ConnectDB(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _transactions = (IMongoCollection<Transaction>?)database.GetCollection<Transaction>("Transactions");
        }

        public async Task StoreTransaction(int firstNumber, int secondNumber, string operation, double result)
        {
            var transaction = new Transaction
            {
                FirstNumber = firstNumber,
                SecondNumber = secondNumber,
                Operation = operation,
                Result = result,
                Timestamp = DateTime.UtcNow
            };

            await _transactions.InsertOneAsync(transaction);

        }
    }

    public class Transaction
    {
        public int Id { get; set; }
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

