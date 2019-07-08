﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace QuizResults
{
	class Program
	{
		static IConfigurationRoot BuildConfiguration()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");

			return builder.Build();
		}

		static void Main(string[] args)
		{
			var parser = new CsvParser();
			var config = BuildConfiguration();
			var data = parser.ParseFile(config["DataFilePath"]);
			if (parser.ErrorMessage != null)
			{
				Console.WriteLine("Parsing failed");
				Console.WriteLine(parser.ErrorMessage);
			}

			var report = DataAnalyzer.Analyze(data);

			Console.WriteLine("Всего тестов: {0}", report.TotalAmountOfTests);
			Console.WriteLine("Количество уникальных e-mail'ов: {0}", report.AmountOfUniqueEmails);

			Console.WriteLine("Распределение попыток:");
			for (var i = 0; i < report.AttemptDistribution.Length; i++)
			{
				if (report.AttemptDistribution[i] != 0)
					Console.WriteLine("{0}: {1}", i + 1, report.AttemptDistribution[i]);
			}

			Console.WriteLine("Распределение результатов:");
			foreach (var el in report.ResultDistribution)
			{
				Console.WriteLine("{0}: {1}", el.Key, el.Value);
			}

			Console.WriteLine("Статистика вопросов:");
			foreach (var el in report.QuestionStatistics)
			{
				Console.WriteLine("Вопрос: {0}\nПравильных ответов: {1}, неправильных: {2}, всего: {3}",
					el.Key, el.Value.RightAnswersAmount, el.Value.WrongAnswersAmount,
					el.Value.RightAnswersAmount + el.Value.WrongAnswersAmount);
			}
		}
	}
}