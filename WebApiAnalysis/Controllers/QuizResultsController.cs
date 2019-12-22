using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiAnalysis.Interfaces;
using WebApiAnalysis.Models;

namespace WebApiAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizResultsController : ControllerBase
    {
        private readonly IDataStorage dataStorage;

        public QuizResultsController(IDataStorage dataStorage)
        {
            this.dataStorage = dataStorage;
        }

        [HttpPost]
        public IActionResult Post([FromBody] QuizResultApiIn quizResultApiIn)
        {
            try
            {
                List<string> errors = new List<string>();

                if (String.IsNullOrEmpty(quizResultApiIn.Name))
                {
                    errors.Add("Parameter \"name\" is empty");
                }
                if (String.IsNullOrEmpty(quizResultApiIn.Email))
                {
                    errors.Add("Parameter \"email\" is empty");
                }
                if (quizResultApiIn.QuestionResults == null)
                {
                    errors.Add("Parameter \"quizresults\" is empty");
                }
                if (errors.Count != 0)
                {
                    return BadRequest(new Status { Result = "Fail", Errors = errors });
                }

                dataStorage.SavePersonTestResult(Parser.ParseQuizResultApiIn(quizResultApiIn));

                return Ok(new Status { Result = "Success" });
            }
            catch (Exception e)
            {
                return BadRequest(new Status { Result = "Error", Errors = new List<string> { e.Message } });
            }
        }       
    }
}
