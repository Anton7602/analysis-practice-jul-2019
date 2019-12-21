using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAnalysis.Interfaces;
using WebApiAnalysis.Models;

namespace WebApiAnalysis.Services
{
    public class DataStorage : IDataStorage
    {
        private readonly IMongoDatabase database;
        private readonly string personTestResultCollectionName;

        public DataStorage(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            database = client.GetDatabase(settings.Value.Database);
            personTestResultCollectionName = settings.Value.PersonTestResultCollectionName;
        }

        private IMongoCollection<PersonTestResult> GetPersonTestResultCollection()
        {
            return database.GetCollection<PersonTestResult>(personTestResultCollectionName);
        }

        public PersonTestResult SavePersonTestResult(PersonTestResult testResult)
        {
            GetPersonTestResultCollection().InsertOne(testResult);
            return testResult;
        }

        public List<PersonTestResult> GetPersonTestResults() =>
            GetPersonTestResultCollection().Find(result => true).ToList();

        public List<PersonTestResult> GetPersonTestResults(Person person) =>
            GetPersonTestResultCollection().Find<PersonTestResult>(result => result.Person.Equals(person)).ToList();
    }
}
