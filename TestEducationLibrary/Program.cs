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

		}
	}
}
