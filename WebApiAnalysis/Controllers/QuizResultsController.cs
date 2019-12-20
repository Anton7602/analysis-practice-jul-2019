using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiAnalysis.Models;
using WebApiAnalysis.Services;

namespace WebApiAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizResultsController : ControllerBase
    {
        private readonly QuizResultService quizResultService;

        public QuizResultsController(QuizResultService quizResultService)
        {
            this.quizResultService = quizResultService;
        }

        [HttpPost]
        public void Post([FromBody] QuizResultApiIn quizResultApiIn)
        {
            Person person = new Person { Email = quizResultApiIn.Email, Name = quizResultApiIn.Name };
            
            //TODO: add List<Answer> to PersonTestResult

            PersonTestResult personTestResult = 
                new PersonTestResult 
                { 
                    Person = person, 
                    Notes = quizResultApiIn.Comment, 
                    Result = quizResultApiIn.PercentUserAnswersCorrect
                };
            
            quizResultService.Create(personTestResult);
        }
    }
}
