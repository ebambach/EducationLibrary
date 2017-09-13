using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationLibrary {
	//This class will not be inheriting a class, as in previous exercises, but it
	//will inherit a List of Student objects.
	public class StudentCollection : List<Student>{

		//^We just called StudentCollection a List of Student objects, so we are now
		//able to use "StudentCollection" as the return type below.
		//Instead of "Get()," we will call this method, and the one below, "Select()," to
		//remind us of the SQL commands used for that purpose.
		public static StudentCollection Select() {
			//Time to connect to SQL, so connectionStr is "where" we are connecting.
			var connectionStr = @"Server=STUDENT05;Database=DotNetDatabase;Trusted_Connection=yes";
			//Now that we know where we need to connect, let's make that connection.
			SqlConnection connection = new SqlConnection(connectionStr);
			//"Open, seasame!"
			connection.Open();
			
			//If the connection did not open, we really want to know that, as we
			//cannot go on without an open connection.
			if (connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("SQL Connection did not open.");
				//The first time we did an "if" like this, we were able to return nothing,
				//but this is part of a method that reutrns a "StudentCollection," so we
				//need to return "null" here if things are not working properly.
				return null;
			}

			//If the connection is open, we want to know that too.
			Console.WriteLine("SQL connection opened successfully.");

			//Before we actually start pulling any data in from SQL, let's make a list that we can
			//store the Student information in.
			//This is a little different from previous instance creations, because we are doing it
			//inside the class of the same name this time.  This works because the class is inheriting
			//List<Student>
			StudentCollection students = new StudentCollection();

			//The command that we have used to show everything on the Student table, that 
			//we will be using here in C#.
			var sql = "select * from Student";

			//Ok, "connection" let's us know "where" we are going, "sql" tells us "what" we
			//are doing there, time to "command," using "cmd," to make things happen...
			SqlCommand cmd = new SqlCommand(sql, connection);
			//...so long as we remember to establish a "reader" to "read" through the records.
			SqlDataReader reader = cmd.ExecuteReader();

			//Let's do some "reading."  So long as there are additional records to read through,
			//this "while" loop will continue through them.
			while (reader.Read()) {
				//These variables will record all of the information on the Student table in SQL (as
				//the table currently stands, if any new fields were added, this would need to be
				//updated).
				var id = reader.GetInt32(reader.GetOrdinal("Id"));
				var firstname = reader.GetString(reader.GetOrdinal("FirstName"));
				var lastname = reader.GetString(reader.GetOrdinal("LastName"));
				var address = reader.GetString(reader.GetOrdinal("Address"));
				var city = reader.GetString(reader.GetOrdinal("City"));
				var state = reader.GetString(reader.GetOrdinal("State"));
				var zipcode = reader.GetString(reader.GetOrdinal("Zipcode"));
				var phone = reader.GetString(reader.GetOrdinal("PhoneNumber"));
				var email = reader.GetString(reader.GetOrdinal("Email"));
				var birthday = reader.GetDateTime(reader.GetOrdinal("Birthday"));
				//SQL is set up to let this be "null," so we need a default for the "majorid" variable,
				//and then so long as the value in SQL != "null," we can update majorid with the value.
				var majorid = 0;
				if (!reader.GetValue(reader.GetOrdinal("MajorId")).Equals(DBNull.Value)) {
					majorid = reader.GetInt32(reader.GetOrdinal("MajorId"));
				}
				var sat = reader.GetInt32(reader.GetOrdinal("SAT"));
				var gpa = reader.GetDouble(reader.GetOrdinal("GPA"));

				//Becuase the above variables are going to have new information each time the loop runs,
				//we need to make sure that data is actually stored somewhere.  Let's make a Student,
				//and set the variables in the Student class to our variables.
				Student student = new Student();
				
				student.id = id;
				student.firstname = firstname;
				student.lastname = lastname;
				student.address = address;
				student.city = city;
				student.state = state;
				student.zipcode = zipcode;
				student.birthday = birthday;
				student.phone = phone;
				student.email = email;
				student.majorid = majorid;
				student.sat = sat;
				student.gpa = gpa;

				//The Student, student, is now set up, lets Add that information to the StudentCollection,
				//students.
				students.Add(student);
			}
			reader.Close();
			connection.Close();
			Console.WriteLine("SQL connection and reader closed.");
			return students;
		}

		//Unlike the above "Select," here we are going to return a single Student, using the id variable,
		//the primary key, to do so.
		//We can "get" away with using different "Select" methods because they take in different parameters,
		//much like the constructors that we have used thus far.
		public static Student Select(int id) {
			return null;
		}

		//To help remember what the SQL commands are, instead of calling this "Add()," this method
		//will be called "Insert()."  In the same manner, instead of "Change()," we will use "Update(),"
		//and instead of "Remove()," "Delete()."
		//This will be used to check if the student parameter was added to the StudentCollection.
		public static bool Insert(Student student) {
			return false;
		}
		public static bool Update(Student student) {
			return false;
		}
		//Unlike the above methods, we are trying to remove the whole Student, which we can
		//reference by the "id."
		public static bool Delete(int id) {
			return false;
		}
	}
}
