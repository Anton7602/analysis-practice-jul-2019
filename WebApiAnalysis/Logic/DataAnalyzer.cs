using System;
using System.Collections.Generic;
using WebApiAnalysis.Models;

namespace WebApiAnalysis.Logic
{
    public static class DataAnalyser
    {
        private static uint _minNumberForAdvStat;

        public static void CalculateAdditionalInfo(PersonStatistics pStatistics)
        {
            if (pStatistics.AmountOfAttempts > _minNumberForAdvStat - 1)
            {
                var x = new double[pStatistics.AmountOfAttempts];
                for (var i = 0; i < x.Length; i++)
                {
                    x[i] = i + 1;
                }

                pStatistics.AdditionalInfo =
                    LinearApproximation.GetLinearApproximation(x,
                    pStatistics.Results.ConvertAll(Convert.ToDouble));
            }
            else
            {
                pStatistics.AdditionalInfo = null;
            }
        }

        /// <summary>
        /// Performs data analysis
        /// </summary>
        /// <param name="data">Data for analysis</param>
        /// <param name="minNumberForAdvStat">Minimum number of tests for one person to build advanced statistics</param>
        /// <returns>Report</returns>
        public static DataAnalyserReport Analyze(IEnumerable<PersonTestResult> data, uint minNumberForAdvStat = 4)
        {
            _minNumberForAdvStat = minNumberForAdvStat;

            var report = new DataAnalyserReport();

            // Collection of pair <Email, Amount of attempts>
            var personStatistics = new Dictionary<string, PersonStatistics>();

            // Collection of pair <Result, Amount>
            var resultDistribution = new Dictionary<int, int>();

            // Statistics on questions
            var qStatistics = new Dictionary<string, QuestionStatistics>();

            var totalAmount = 0U;
            var maxNumberOfAttempts = 0U;
            var personWithMaxNumberOfAttempts = "";

            foreach (var testResult in data)
            {
                if (testResult.Answers == null)
                {
                    continue;
                }

                personStatistics = GetPersonalStatistics(personStatistics, testResult);

                if (personStatistics[testResult.Person.Email].AmountOfAttempts > maxNumberOfAttempts)
                {
                    maxNumberOfAttempts = personStatistics[testResult.Person.Email].AmountOfAttempts;
                    personWithMaxNumberOfAttempts = testResult.Person.Email;
                }

                resultDistribution = GetResultDistribution(resultDistribution, testResult.Result);
                totalAmount++;

                // Collect statistics on questions
                qStatistics = StatisticsOnQuestions(qStatistics, testResult.Answers);
            }

            foreach (var pStatistics in personStatistics)
            {
                CalculateAdditionalInfo(pStatistics.Value);
            }

            report.TotalAmountOfTests = totalAmount;
            report.PersonStatistics = personStatistics;
            report.ResultDistribution = resultDistribution;
            report.QuestionStatistics = qStatistics;

            return report;
        }

        public static DataAnalyserReport Analyse(DataAnalyserReport report, PersonTestResult data, uint minNumberForAdvStat = 4)
        {
            if (data.Answers == null)
            {
                return report;
            }
            var personWithMaxNumberOfAttempts = "";
            var maxNumberOfAttempts = 0U;

            report.PersonStatistics = GetPersonalStatistics(report.PersonStatistics, data);

            if (report.PersonStatistics[data.Person.Email].AmountOfAttempts > maxNumberOfAttempts)
            {
                maxNumberOfAttempts = report.PersonStatistics[data.Person.Email].AmountOfAttempts;
                personWithMaxNumberOfAttempts = data.Person.Email;
            }

            report.ResultDistribution = GetResultDistribution(report.ResultDistribution, data.Result);
            report.TotalAmountOfTests++;

            // Collect statistics on questions
            report.QuestionStatistics = StatisticsOnQuestions(report.QuestionStatistics, data.Answers);
            foreach (var pStatistics in report.PersonStatistics)
            {
                CalculateAdditionalInfo(pStatistics.Value);
            }

            return report;
        }

        public static Dictionary<string, QuestionStatistics> StatisticsOnQuestions(Dictionary<string, QuestionStatistics> qustion_statistics, List<Answer> answers)
        {
            foreach (var answer in answers)
            {
                if (!qustion_statistics.ContainsKey(answer.Question.QuestionText))
                {
                    qustion_statistics.Add(answer.Question.QuestionText, new QuestionStatistics());
                }

                if (answer.AnswerIndex == answer.Question.CorrectAnswerIndex)
                {
                    qustion_statistics[answer.Question.QuestionText].RightAnswersAmount++;
                }
                else
                {
                    qustion_statistics[answer.Question.QuestionText].WrongAnswersAmount++;
                }

                qustion_statistics[answer.Question.QuestionText].AnswersDistribution[answer.AnswerIndex]++;
                qustion_statistics[answer.Question.QuestionText].RightAnswerIndex = answer.Question.CorrectAnswerIndex;
            }
            return qustion_statistics;
        }


        public static Dictionary<string, PersonStatistics> GetPersonalStatistics(Dictionary<string, PersonStatistics> personStatistics, PersonTestResult data)
        {
            if (!personStatistics.ContainsKey(data.Person.Email))
            {
                personStatistics.Add(data.Person.Email, new PersonStatistics());
            }
            personStatistics[data.Person.Email].Results.Add(data.Result);

            //if (personStatistics[data.Person.Email].AmountOfAttempts > maxNumberOfAttempts)
            //{
            //    maxNumberOfAttempts = personStatistics[data.Person.Email].AmountOfAttempts;
            //    personWithMaxNumberOfAttempts = data.Person.Email;
            //}

            return personStatistics;
        }

        public static Dictionary<int, int> GetResultDistribution(Dictionary<int, int> resultDistribution, int data)
        {
            if (!resultDistribution.ContainsKey(data))
            {
                resultDistribution.Add(data, 0);
            }
            resultDistribution[data]++;
            return resultDistribution;
        }


        /// <summary>
        /// Analyzes new information and adds data to an existing report
        /// </summary>
        /// <param name="report">An existing report</param>
        /// <param name="newData">Collection of new tests</param>
        public static void AddNewData(this DataAnalyserReport report, IEnumerable<PersonTestResult> newData)
        {
            foreach (var el in newData)
                AddNewData(report, el);
        }

        /// <summary>
        /// Analyzes new information and adds data to an existing report
        /// </summary>
        /// <param name="report">An existing report</param>
        /// <param name="newData">Information about new test</param>
        public static void AddNewData(this DataAnalyserReport report, PersonTestResult newData)
        {
            if (!report.PersonStatistics.ContainsKey(newData.Person.Email))
                report.PersonStatistics.Add(newData.Person.Email, new PersonStatistics());
            report.PersonStatistics[newData.Person.Email].Results.Add(newData.Result);
            CalculateAdditionalInfo(report.PersonStatistics[newData.Person.Email]);

            if (!report.ResultDistribution.ContainsKey(newData.Result))
                report.ResultDistribution.Add(newData.Result, 0);
            report.ResultDistribution[newData.Result]++;

            report.TotalAmountOfTests++;

            foreach (var answer in newData.Answers)
            {
                if (!report.QuestionStatistics.ContainsKey(answer.Question.QuestionText))
                    report.QuestionStatistics.Add(answer.Question.QuestionText, new QuestionStatistics());
                var el = report.QuestionStatistics[answer.Question.QuestionText];

                if (answer.AnswerIndex == answer.Question.CorrectAnswerIndex)
                    el.RightAnswersAmount++;
                else
                    el.WrongAnswersAmount++;

                el.RightAnswerIndex = answer.Question.CorrectAnswerIndex;

                el.AnswersDistribution[answer.AnswerIndex]++;
            }
        }
    }
}
