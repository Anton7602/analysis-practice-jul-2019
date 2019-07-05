using System;
using Xunit;
using QuizResults;
using QuizResults.Models;
using System.Linq;
using System.Collections.Generic;

namespace QuizResults.Tests
{
	public class CsvParserTests
	{
		internal void CompareAnswers(Answer expected, Answer actual)
		{
			Assert.Equal(expected.Question.QuestionText, actual.Question.QuestionText);
			Assert.Equal(expected.Question.AnswersList.Count, actual.Question.AnswersList.Count);
			for (var i = 0; i < actual.Question.AnswersList.Count; i++)
			{
				Assert.Equal(expected.Question.AnswersList.ElementAt(i), actual.Question.AnswersList.ElementAt(i));
			}
			Assert.Equal(expected.AnswerIndex, actual.AnswerIndex);
			Assert.Equal(expected.Question.CorrectAnswerIndex, actual.Question.CorrectAnswerIndex);
		}

		[Fact]
		public void OneCorrectTest()
		{
			var parser = new CsvParser();
			var data = parser.ParseFile("Tests/OneCorrectTest.txt");

			Assert.Single(data);
			var test = data.First();

			Assert.Equal("email@site.com", test.Person.Email);
			Assert.Equal("����", test.Person.Name);
			Assert.Null(test.Notes);
			Assert.Equal((uint)60, test.Result);

			Assert.Equal(5, test.Answers.Count);

			var expected = new Answer
			{
				Question = new Question
				{
					QuestionText = "�������� �������� �������� ������� � ������ ���������� ����� ��������� �� �����. �� ��� �� ����� ������?",
					AnswersList = new List<string>
					{
						"bread bread burger cheese",
						"bread burger cheese bread",
						"bread burger bread cheese",
						"burger cheese bread bread"
					},
					CorrectAnswerIndex = 2
				},
				AnswerIndex = 2
			};
			CompareAnswers(expected, test.Answers.ElementAt(0));

			expected = new Answer
			{
				Question = new Question
				{
					QuestionText = "����� - ��� ����, � ���� ? ��� �����?",
					AnswersList = new List<string>
					{
						"undefined true",
						"false true",
						"true true",
						"true undefined"
					},
					CorrectAnswerIndex = 0
				},
				AnswerIndex = 3
			};
			CompareAnswers(expected, test.Answers.ElementAt(1));

			expected = new Answer
			{
				Question = new Question
				{
					QuestionText = "������ �����, ��� � ��� �������� � �������.",
					AnswersList = new List<string>
					{
						"[ 'bacon and eggs' ]",
						"[ 'bacon and eggs', 'coffee' ]",
						"[ 'coffee', 'bacon and eggs' ]",
						"SyntaxError: Unexpected token"
					},
					CorrectAnswerIndex = 0
				},
				AnswerIndex = 0
			};
			CompareAnswers(expected, test.Answers.ElementAt(2));

			expected = new Answer
			{
				Question = new Question
				{
					QuestionText = "������� � ���� ������ �������� �����������. ���� ��������� � ����� ������� 2 ����������, � ������� ������� � ����� 4. �� � ����� ������ 1 ������� �����, � � �������� - ������ 3. ������� �������� ����� �� �� �������?",
					AnswersList = new List<string>
					{
						"'1-23-4'",
						"-2",
						"'1-2-1'",
						"'-13-4'"
					},
					CorrectAnswerIndex = 2
				},
				AnswerIndex = 2
			};
			CompareAnswers(expected, test.Answers.ElementAt(3));

			expected = new Answer
			{
				Question = new Question
				{
					QuestionText = "���������� ������ ����������� � ������. ��������� �� � ���? ��� ����� �������� � ������� ����� �������������� input ��������?",
					AnswersList = new List<string>
					{
						"focus pocus",
						"focus",
						"������",
						"undefined"
					},
					CorrectAnswerIndex = 2
				},
				AnswerIndex = 0
			};
			CompareAnswers(expected, test.Answers.ElementAt(4));
		}
	}
}
