using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
		public string phonenumber { get; set; }
		public string email { get; set; }
		//Needs a default in the Program, "majorid" is allowed to be "null" in SQL
		public int majorid { get; set; }
		public int sat { get; set; }
		public double gpa { get; set; }

		private void SetDataFromReader(SqlDataReader reader) {
			//These variables will record all of the information on the Student table in SQL (as
			//the table currently stands, if any new fields were added, this would need to be
			//updated).
			id = reader.GetInt32(reader.GetOrdinal("Id"));
			firstname = reader.GetString(reader.GetOrdinal("FirstName"));
			lastname = reader.GetString(reader.GetOrdinal("LastName"));
			address = reader.GetString(reader.GetOrdinal("Address"));
			city = reader.GetString(reader.GetOrdinal("City"));
			state = reader.GetString(reader.GetOrdinal("State"));
			zipcode = reader.GetString(reader.GetOrdinal("Zipcode"));
			phonenumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
			email = reader.GetString(reader.GetOrdinal("Email"));
			birthday = reader.GetDateTime(reader.GetOrdinal("Birthday"));
			//SQL is set up to let this be "null," so we need a default for the "majorid" variable,
			//and then so long as the value in SQL != "null," we can update majorid with the value.
			majorid = 0;
			//DBNull.Value is a way of saying the "DataBase" value is null.
			if (!reader.GetValue(reader.GetOrdinal("MajorId")).Equals(DBNull.Value)) {
				majorid = reader.GetInt32(reader.GetOrdinal("MajorId"));
			}
			sat = reader.GetInt32(reader.GetOrdinal("SAT"));
			gpa = reader.GetDouble(reader.GetOrdinal("GPA"));
		}

		public Student() {

		}

		public Student(SqlDataReader reader) {
			SetDataFromReader(reader);
		}
	}
}
