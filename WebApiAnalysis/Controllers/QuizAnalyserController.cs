using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiAnalysis.Interfaces;
using WebApiAnalysis.Logic;
using WebApiAnalysis.Models;
using WebApiAnalysis.Services;

namespace WebApiAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizAnalysisController : ControllerBase
    {
        private readonly IDataStorage dataStorage;

        public QuizAnalysisController(IDataStorage dataStorage)
        {
            this.dataStorage = dataStorage;
        }

        [HttpGet("analyserAlldata")]
        public IActionResult AnalyserAlldata()
        {
            DataAnalyser.Analyze(dataStorage.GetPersonTestResults());
            return Ok(dataStorage.GetPersonTestResults());
        }

        

    }
}
