using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAnalysis.Models;

namespace WebApiAnalysis.Controllers
{
    public class Parser
    {
        public static PersonTestResult ParseQuizResultApiIn(QuizResultApiIn quizResultApiIn)
        {
            Person person = new Person { Email = quizResultApiIn.Email, Name = quizResultApiIn.Name };

            List<Answer> answers = new List<Answer>();
            foreach (var questionResult in quizResultApiIn.QuestionResults)
            {
                List<string> list = new List<string>();
                uint? correctAnswerIndex = null;
                uint? userAnswerIndex = null;

                for (uint i = 0; i < questionResult.AnswerResults.Count; i++)
                {
                    var answerResult = questionResult.AnswerResults[(int)i];
                    var answer = answerResult.Substring(answerResult.LastIndexOf(')') + 1);
                    list.Add(answer);
                    if (answerResult.Contains("(+)"))
                        correctAnswerIndex = i;
                    if (answerResult.Contains("(v)"))
                        userAnswerIndex = i;
                }

                if (!correctAnswerIndex.HasValue)
                {
                    throw new Exception("Question with no correct answer");
                }
                if (!userAnswerIndex.HasValue)
                {
                    throw new Exception("Question with no user answer");
                }

                var question = new Question
                {
                    QuestionText = questionResult.QuestionText,
                    AnswersList = list,
                    CorrectAnswerIndex = correctAnswerIndex.Value
                };

                answers.Add(new Answer
                {
                    Question = question,
                    AnswerIndex = userAnswerIndex.Value
                });
            }

            return new PersonTestResult
                {
                    Person = person,
                    Notes = quizResultApiIn.Comment,
                    Result = quizResultApiIn.PercentUserAnswersCorrect,
                    Answers = answers
                };
        }
    }
}
