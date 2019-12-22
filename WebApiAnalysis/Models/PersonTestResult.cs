using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiAnalysis.Models
{
	public class PersonTestResult
	{
		public MongoDB.Bson.ObjectId Id { get; set; }
		public Person Person { get; set; }
		public List<Answer> Answers { get; set; }
		public int Result { get; set; }
		public string Notes { get; set; }
	}
}
