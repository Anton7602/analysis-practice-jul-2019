using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAnalysis.Models
{
    public class QuestionResult
    {
        [BsonElement("questionText")]
        public string QuestionText { get; set; }

        [BsonElement("answerResults")]
        public List<string> AnswerResults { get; set;  }
    }
}
