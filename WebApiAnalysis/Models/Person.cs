using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiAnalysis.Models
{
	public class Person
	{
		public string Name { get; set; }
		public string Email { get; set; }

		public override bool Equals(object obj)
		{
			Person person = obj as Person;
			if (person != null)
			{
				return this.Name == person.Name && this.Email == person.Email ? true : false;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
