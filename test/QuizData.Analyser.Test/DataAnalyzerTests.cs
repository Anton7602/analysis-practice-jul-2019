using QuizData.Parser;
using Xunit;
using QuizData.Analyser.Models;

namespace QuizData.Analyser.Test
{
	public class DataAnalyzerTests
	{
		internal void CompareQuestionStatistics(QuestionStatistics expected, QuestionStatistics actual)
		{
			Assert.Equal(expected.AnswersDistribution, actual.AnswersDistribution);
			Assert.Equal(expected.RightAnswerIndex, actual.RightAnswerIndex);
			Assert.Equal(expected.RightAnswersAmount, actual.RightAnswersAmount);
			Assert.Equal(expected.WrongAnswersAmount, actual.WrongAnswersAmount);
		}

		[Fact]
		public void TestOneQuiz()
		{
			var parser = new CsvParser();
			var data = parser.ParseFile("data/testOneQuiz.txt");
			var results = DataAnalyser.Analyze(data);

			Assert.Equal(1U, results.AmountOfUniqueEmails);

			var attemptDistribution = new uint[] { 1 };
			Assert.Equal(attemptDistribution, results.AttemptDistribution);

			var questions = new[]
			{
				"�������� �������� �������� ������� � ������ ���������� ����� ��������� �� �����. �� ��� �� ����� ������?",
				"����� - ��� ����, � ���� ? ��� �����?",
				"������ �����, ��� � ��� �������� � �������.",
				"������� � ���� ������ �������� �����������. ���� ��������� � ����� ������� 2 ����������, � ������� ������� � ����� 4. �� � ����� ������ 1 ������� �����, � � �������� - ������ 3. ������� �������� ����� �� �� �������?",
				"���������� ������ ����������� � ������. ��������� �� � ���? ��� ����� �������� � ������� ����� �������������� input ��������?"
			};

			var qStatistics = new QuestionStatistics[]
			{
				new QuestionStatistics
				{
					AnswersDistribution = new uint[] {0, 0, 1, 0},
					RightAnswerIndex = 2,
					RightAnswersAmount = 1,
					WrongAnswersAmount = 0
				},
				new QuestionStatistics
				{
					AnswersDistribution = new uint[] {0, 0, 0, 1},
					RightAnswerIndex = 0,
					RightAnswersAmount = 0,
					WrongAnswersAmount = 1
				},
				new QuestionStatistics
				{
					AnswersDistribution = new uint[] {1, 0, 0, 0},
					RightAnswerIndex = 0,
					RightAnswersAmount = 1,
					WrongAnswersAmount = 0
				},
				new QuestionStatistics
				{
					AnswersDistribution = new uint[] {0, 0, 1, 0},
					RightAnswerIndex = 2,
					RightAnswersAmount = 1,
					WrongAnswersAmount = 0
				},
				new QuestionStatistics
				{
					AnswersDistribution = new uint[] {1, 0, 0, 0},
					RightAnswerIndex = 2,
					RightAnswersAmount = 0,
					WrongAnswersAmount = 1
				}
			};

			Assert.Equal(5, results.QuestionStatistics.Count);
			for (var i = 0; i < questions.Length; i++)
			{
				Assert.True(results.QuestionStatistics.ContainsKey(questions[i]));
				CompareQuestionStatistics(qStatistics[i], results.QuestionStatistics[questions[i]]);
			}

			Assert.Single(results.ResultDistribution);
			Assert.True(results.ResultDistribution.ContainsKey(60U));
			Assert.Equal(1U, results.ResultDistribution[60U]);

			Assert.Equal(1U, results.TotalAmountOfTests);
		}
	}
}
