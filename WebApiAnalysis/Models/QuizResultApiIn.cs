using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAnalysis.Models
{
    public class QuizResultApiIn
    {
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("comment")]
        public string Comment { get; set; } = string.Empty;

        [BsonElement("percentUserAnswersCorrect")]
        public int PercentUserAnswersCorrect { get; set; } = 0;

        [BsonElement("questionResults")]
        public List<QuestionResult> QuestionResults { get; set; } = null;
    }
}
