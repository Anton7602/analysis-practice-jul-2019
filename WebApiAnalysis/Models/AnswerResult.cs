using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAnalysis.Models
{
    public class AnswerResult
    {
        public string AnswerText { get; set; }

        public bool IsUserChosen { get; set; }

        public bool IsCorrect { get; set; }
    }
}
