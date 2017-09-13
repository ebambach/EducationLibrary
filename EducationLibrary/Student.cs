using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationLibrary {
	public class Student {

		//As with the previous exercise, CSharpToSql, this class will be using the same
		//information that is in the SQL table to set up a "Student"

		//"id" = primary key
		public int id { get; set; }
		public string firstname { get; set; }
		public string lastname { get; set; }
		public string address { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zipcode { get; set; }
		public DateTime birthday { get; set; }
		public string phone { get; set; }
		public string email { get; set; }
		//Needs a default in the Program, "majorid" is allowed to be "null" in SQL
		public int majorid { get; set; }
		public int sat { get; set; }
		public double gpa { get; set; }
	}
}
