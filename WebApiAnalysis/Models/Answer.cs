using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiAnalysis.Models
{
	public class Answer
	{
		public Question Question { get; set; }
		public uint AnswerIndex { get; set; }
	}
}
