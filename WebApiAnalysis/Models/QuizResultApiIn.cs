using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAnalysis.Models
{
    public class QuizResultApiIn
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int PercentUserAnswersCorrect { get; set; } = 0;
        public List<QuestionResult> QuestionResults { get; set; } = null;
    }
}
