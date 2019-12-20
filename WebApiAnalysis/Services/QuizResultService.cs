using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAnalysis.Models;

namespace WebApiAnalysis.Services
{
    public class QuizResultService
    {
        private readonly IMongoCollection<PersonTestResult> testResults;

        public QuizResultService(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.Database);
            testResults = database.GetCollection<PersonTestResult>("QuizResults");
        }

        public PersonTestResult Create(PersonTestResult testResult)
        {
            testResults.InsertOne(testResult);
            return testResult;
        }
    }
}
