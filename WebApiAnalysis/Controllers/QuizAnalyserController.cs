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
    public class QuizAnalysisController : ControllerBase
    {
        private readonly QuizResultService quizResultService;

        // public QuizResultsController(QuizResultService quizResultService)
        // {
        //     this.quizResultService = quizResultService;
        // }
    }
}
