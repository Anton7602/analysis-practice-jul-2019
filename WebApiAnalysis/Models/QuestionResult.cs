using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAnalysis.Models
{
    public class QuestionResult
    {
        public string QuestionText { get; set; }

        public List<AnswerResult> AnswerResults { get; set; }
    }
}
