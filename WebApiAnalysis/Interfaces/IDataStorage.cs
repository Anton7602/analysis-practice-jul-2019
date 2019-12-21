using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAnalysis.Models;

namespace WebApiAnalysis.Interfaces
{
    public interface IDataStorage
    {
        PersonTestResult SavePersonTestResult(PersonTestResult testResult);
        List<PersonTestResult> GetPersonTestResults();
        List<PersonTestResult> GetPersonTestResults(Person person);
    }
}
