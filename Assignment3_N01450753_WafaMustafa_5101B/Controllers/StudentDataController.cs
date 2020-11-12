using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using Assignment3_N01450753_WafaMustafa_5101B.Models;
using MySql.Data.MySqlClient;

namespace Assignment3_N01450753_WafaMustafa_5101B.Controllers
{
    public class StudentDataController : ApiController
    {
        // Db context class which will allow my Get method to access the MySql SchoolDb and get the list of students 
        private SchoolDbContext Schoolstudents = new SchoolDbContext();


        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Students> ListStudents()
        {
            List<Students> StudentNames = new List<Students> { };

            //USING YOUR BLOG EXAMPLE TO CONNECT schooldb TO GET student info.
            //establishing a connection to the database
            MySqlConnection Conn = Schoolstudents.AccessDatabase();

            //Open the connection Db and web as shown in your blog example.
            Conn.Open();

            //Here we are establishing a command that will run in our PHPmyadmin schooldb to get the information needed
            MySqlCommand schooldbcmd = Conn.CreateCommand();

            //this is a command we are sending to our connect database once we put in execute our get method
            schooldbcmd.CommandText = "Select * from students";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = schooldbcmd.ExecuteReader();


            //function we will use to return a list of student just by their first and last name
            while (ResultSet.Read())
            {
                Students allStudents = new Students();

                //looked through you extra links to Microsoft .NET helplinks "convert.toint"
                allStudents.studentId = Convert.ToInt32(ResultSet["studentid"]);
                allStudents.studentFname = ResultSet["studentfname"].ToString();
                allStudents.studentLname = ResultSet["studentlname"].ToString();
                allStudents.studentNumber = ResultSet["studentnumber"].ToString();
                allStudents.enrolDate = ResultSet["enroldate"].ToString();


                //adding school students to the list 
                StudentNames.Add(allStudents);

            }
            //Once the loop has run through all the teacher first and last name we are closing the connetion.
            Conn.Close();

            return StudentNames;

        }

        [HttpGet]
        [Route("api/StudentData/Studentinfo/{id}")]
        public Students Studentinfo(int id)
        {
            //creating a return type when id number is selected
            Students morestudentInfo = new Students();

            //creating and opening a connection to the "students database" to pull info from
            MySqlConnection Conn = Schoolstudents.AccessDatabase();
            Conn.Open();

            //establishing a command to send to the database 
            MySqlCommand schooldbcmd = Conn.CreateCommand();
            schooldbcmd.CommandText = "Select * from students where studentid =" +id;

            //variable for the resultset from the query
            MySqlDataReader morestuInfo = schooldbcmd.ExecuteReader();

            while (morestuInfo.Read())
            {

                morestudentInfo.studentId = Convert.ToInt32(morestuInfo["studentid"]);
                morestudentInfo.studentFname = morestuInfo["studentfname"].ToString();
                morestudentInfo.studentLname = morestuInfo["studentlname"].ToString();
                morestudentInfo.studentNumber = morestuInfo["studentnumber"].ToString();
                morestudentInfo.enrolDate = morestuInfo["enroldate"].ToString();



            }

            return morestudentInfo;
        }
    }
}
