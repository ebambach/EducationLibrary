using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EducationLibrary;

namespace TestEducationLibrary {
	class Program {
		static void Main(string[] args) {
			new Program().Run();
		}

		void Run() {
			//We made the methods in "StudentCollection" static, which lets us cheat a little, and create
			//"StudentCollection students," without having an instance of "StudentCollection" yet.
			StudentCollection students = StudentCollection.Select();
			Student ispresent = StudentCollection.Select(4);
			Student stud = new Student();
			stud.firstname = "Phillip";
			stud.lastname = "Fry";
			stud.address = "Planet Express";
			stud.city = "New New York";
			stud.state = "NY";
			stud.zipcode = "test";
			stud.birthday = new DateTime(1982, 3, 14);
			stud.phonenumber = "5135558008";
			stud.email = "pjf@planetexpress.com";
			stud.majorid = 4;
			stud.sat = 800;
			stud.gpa = 2.9;

			bool rc = StudentCollection.Insert(stud);

			Student changeName = StudentCollection.Select(13);
			changeName.firstname = "Phil";

			rc = StudentCollection.Update(changeName);
			rc = StudentCollection.Delete(13);
		}
	}
}
