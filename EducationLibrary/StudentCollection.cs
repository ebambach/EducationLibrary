using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationLibrary {
	//This class will not be inheriting a class, as in previous exercises, but it
	//will inherit a List of Student objects.
	public class StudentCollection : List<Student> {

		//^We just called StudentCollection a List of Student objects, so we are now
		//able to use "StudentCollection" as the return type below.
		//Instead of "Get()," we will call this method, and the one below, "Select()," to
		//remind us of the SQL commands used for that purpose.

		//connectionStr is "where" we are connecting, when we connect to SQL
		private static string connectionStr = @"Server=STUDENT05;Database=DotNetDatabase;Trusted_Connection=yes";

		public static StudentCollection Select() {
			//Let's make that connection.
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
				//This while loop greatly cuts down on the needed code compared to the first time that we did
				//an exercise like this.  Instead of setting up an instance of a Student, we go straight to 
				//calling a constructor in the Student class, which sets up the information from the SQL
				//table.  This information is then added to our StudentCollection.
				students.Add(new Student(reader));
			}
			reader.Close();
			connection.Close();
			Console.WriteLine("SQL connection and reader closed.");
			return students;
		}

		//Unlike the above "Select," here we are going to return a single Student, using the id variable,
		//the primary key, to do so.  
		//This "Select" will use near identical code to the above "Select,"
		//but we only want one Student this time, so "var sql = $"select * from Student where Id = {id}";"

		//We can "get" away with using different "Select" methods because they take in different parameters,
		//much like the constructors that we have used thus far.
		public static Student Select(int id) {
			SqlConnection connection = new SqlConnection(connectionStr);
			connection.Open();

			if (connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("SQL Connection did not open.");
				return null;
			}

			Console.WriteLine("SQL connection opened successfully.");

			StudentCollection students = new StudentCollection();

			var sql = $"select * from Student where Id = {id}";

			SqlCommand cmd = new SqlCommand(sql, connection);
			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read()) {
				students.Add(new Student(reader));
			}
			reader.Close();
			connection.Close();
			Console.WriteLine("SQL connection and reader closed.");
			if (students.Count == 0) {
				return null;
			}
			else {
				return students[0];
			}
		}

		//To help remember what the SQL commands are, instead of calling this "Add()," this method
		//will be called "Insert()."  In the same manner, instead of "Change()," we will use "Update(),"
		//and instead of "Remove()," "Delete()."
		//This will be used to check if the student parameter was added to the StudentCollection.
		public static bool Insert(Student student) {
			SqlConnection connection = new SqlConnection(connectionStr);
			connection.Open();

			if (connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("SQL Connection did not open.");
				return false;
			}

			Console.WriteLine("SQL connection opened successfully.");

			StudentCollection students = new StudentCollection();

			var sql = $"INSERT into Student (FirstName, LastName, Address, City, State, Zipcode," +
				$"PhoneNumber, Email, Birthday, MajorId, SAT, GPA)" +
				$"VALUES" +
				$"('{student.firstname}', '{student.lastname}', '{student.address}', '{student.city}'," +
				$"'{student.state}', '{student.zipcode}', '{student.phonenumber}', '{student.email}'," +
				$"'{student.birthday}', {student.majorid}, {student.sat}, {student.gpa})";

			SqlCommand cmd = new SqlCommand(sql, connection);
			var recsAffected = cmd.ExecuteNonQuery();
			if (recsAffected == 1) {
				return true;
			}
			else {
				return false;
			}
		}
		public static bool Update(Student student) {
			SqlConnection connection = new SqlConnection(connectionStr);
			connection.Open();

			if (connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("SQL Connection did not open.");
				return false;
			}

			Console.WriteLine("SQL connection opened successfully.");

			StudentCollection students = new StudentCollection();

			var sql = $"UPDATE Student Set " +
				$"FirstName = '{student.firstname}'," +
				$"LastName = '{student.lastname}'," +
				$"Address = '{student.address}'," +
				$"City = '{student.city}'," +
				$"State = '{student.state}'," +
				$"Zipcode = '{student.zipcode}'," +
				$"PhoneNumber = '{student.phonenumber}'," +
				$"Email = '{student.email}'," +
				$"Birthday = '{student.birthday}'," +
				$"MajorId = {student.majorid}," +
				$"SAT = {student.sat}," +
				$"GPA = {student.gpa}" +
				$" WHERE ID = {student.id}";

			SqlCommand cmd = new SqlCommand(sql, connection);
			var recsAffected = cmd.ExecuteNonQuery();
			if (recsAffected == 1) {
				return true;
			}
			else {
				return false;
			}
		}
		//Unlike the above methods, we are trying to remove the whole Student, which we can
		//reference by the "id."
		public static bool Delete(int id) {
			SqlConnection connection = new SqlConnection(connectionStr);
			connection.Open();

			if (connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("SQL Connection did not open.");
				return false;
			}

			Console.WriteLine("SQL connection opened successfully.");

			StudentCollection students = new StudentCollection();

			var sql = $"Delete From Student Where Id = {id}";
			SqlCommand cmd = new SqlCommand(sql, connection);
			var recsAffected = cmd.ExecuteNonQuery();
			if (recsAffected == 1) {
				return true;
			}
			else {
				return false;
			}
		}
	}
}
