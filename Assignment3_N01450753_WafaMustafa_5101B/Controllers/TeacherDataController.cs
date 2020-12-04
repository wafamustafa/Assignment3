using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using Assignment3_N01450753_WafaMustafa_5101B.Models;
using MySql.Data.MySqlClient;

namespace Assignment3_N01450753_WafaMustafa_5101B.Controllers
{
    public class TeacherDataController : ApiController
    {
        // Db context class which will allow my Get method to access the MySql SchoolDb and get the list of teacher 
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Get: api/TeacherData/ListTeacher/{searchkey}
        /// </summary>
        /// <returns>
        /// will connect to the School database and return the result of the List of teachers 
        /// with the first name and last name or we can search for a specfic name to look up teacher information as we have coded our while loop to do
        /// <example>"<string>Alexander Bennett</string>"</example>
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            List<Teacher> TeacherNames = new List<Teacher> { };

            //USING YOUR BLOG EXAMPLE TO CONNECT schooldb TO GET teacher info.
            //establishing a connection to the database
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection Db and web as shown in your blog example.
            Conn.Open();

            //Here we are establishing a command that will run in our PHPmyadmin schooldb to get the information needed
            MySqlCommand schooldbcmd = Conn.CreateCommand();

            //this is a command we are sending to our connect database once we put in execute our get method
            schooldbcmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            //@ key - to prevert SQLinjection attacks
            //adding innew parameters to ensure user is still able to search teacher data by lower or upper case string inserted in the search text
            schooldbcmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");


            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = schooldbcmd.ExecuteReader();


            //function we will use to return a list of teachers just by their first and last name
            while (ResultSet.Read())
            {
                Teacher schoolTeacher = new Teacher();
                //WHAT IF WE DECLARE NEW VARIABLES 
                schoolTeacher.teacherId = (int)ResultSet["teacherid"];
                schoolTeacher.teacherFname = (string)ResultSet["teacherfname"];
                schoolTeacher.teacherLname = (string)ResultSet["teacherlname"];
                schoolTeacher.employeeNumber = (string)ResultSet["employeenumber"];
                schoolTeacher.hireDate = (DateTime)ResultSet["hiredate"];
                schoolTeacher.saLary = (decimal)ResultSet["salary"];

                //RETURN IT HERE 


                //adding school teachers to the list 
                TeacherNames.Add(schoolTeacher);

            }
            //Once the loop has run through all the teacher first and last name we are closing the connetion.
            Conn.Close();

            return TeacherNames;

        }
        /// <summary>
        /// This method will give you more info on the teachers 
        /// </summary>
        /// <param name="id">we click om the list of teachers we are able to get more info</param>
        /// <returns>teacher infor such as hire date, employee number, salary.</returns>

        [HttpGet]
        [Route("api/TeacherData/Teacherinfo/{id}")]
        public Teacher Teacherinfo(int id)
        {
            //creating a return type when id number is selected
            Teacher moreteacherInfo = new Teacher();

            //creating and opening a connection to the "teachers database" to pull info from
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();

            //establishing a command to send to the database 
            MySqlCommand schooldbcmd = Conn.CreateCommand();
            schooldbcmd.CommandText = "Select * from teachers where teacherid =" + id;

            //variable for the resultset from the query
            MySqlDataReader moreInfo = schooldbcmd.ExecuteReader();

            while (moreInfo.Read())
            {

                moreteacherInfo.teacherId = (int)moreInfo["teacherid"];
                moreteacherInfo.teacherFname = (string)moreInfo["teacherfname"];
                moreteacherInfo.teacherLname = (string)moreInfo["teacherlname"];
                moreteacherInfo.employeeNumber = (string)moreInfo["employeenumber"];
                moreteacherInfo.hireDate = (DateTime)moreInfo["hiredate"];
                moreteacherInfo.saLary = (decimal)moreInfo["salary"];


            }

            return moreteacherInfo;
        }
        ///<summary>
        ///Investigating post request to delete teachers from the database  
        ///</summary>
        ///<returns>Delete confirmation screen and once confirmed, deletes the teacher name and info from the database
        ///</returns>

        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //creating and opening a connection to the "teacher database" to pull info from
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();

            //establishing a command to send to the database 
            MySqlCommand schooldbcmd = Conn.CreateCommand();
            schooldbcmd.CommandText = "delete from teachers where teacherid=@id";
            schooldbcmd.Parameters.AddWithValue("@id", id);
            schooldbcmd.Prepare();

            //excute a non select statment
            schooldbcmd.ExecuteNonQuery();

            Conn.Close();


            //no return need as it will be deleting the teacher and bringing the user back to list page

        }

        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //creating and opening a connection to the "students database" to pull info from
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();

            //establishing a command to send to the database 
            MySqlCommand schooldbcmd = Conn.CreateCommand();

            //SQL QUERY to insert new teachers 
            schooldbcmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@teacherFname,@teacherLname,@employeeNumber, TIMESTAMP(@hireDate), @saLary)";
            //NOTE: TIMESTAMP value obtained from Simon's class on data types. dateTime was not working for me.


            //what values are we getting and where are we going to get it from
            schooldbcmd.Parameters.AddWithValue("@teacherFname", NewTeacher.teacherFname);
            schooldbcmd.Parameters.AddWithValue("@teacherLname", NewTeacher.teacherLname);
            schooldbcmd.Parameters.AddWithValue("@employeeNumber", NewTeacher.employeeNumber);
            schooldbcmd.Parameters.AddWithValue("@hireDate", NewTeacher.hireDate);
            schooldbcmd.Parameters.AddWithValue("@saLary", NewTeacher.saLary);

            schooldbcmd.Prepare();

            //excute a non select statment
            schooldbcmd.ExecuteNonQuery();

            Conn.Close();


        }

        [HttpPost]
        public void UpdateTeacher(int id, [FromBody] Teacher UpdatedTeacherInfo)
        {
            //creating and opening a connection to the "students database" to pull info from
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();

            //establishing a command to send to the database 
            MySqlCommand schooldbcmd = Conn.CreateCommand();

            //SQL QUERY to insert new teachers 
            schooldbcmd.CommandText = "update teachers set teacherfname=@teacherFname, teacherlname=@teacherLname, employeenumber=@employeeNumber, hiredate=TIMESTAMP(@hireDate), salary=@saLary where teacherid=@teacherId";
            //NOTE: TIMESTAMP value obtained from Simon's class on data types. dateTime was not working for me.


            //what values are we getting and where are we going to get it from
            schooldbcmd.Parameters.AddWithValue("@teacherFname", UpdatedTeacherInfo.teacherFname);
            schooldbcmd.Parameters.AddWithValue("@teacherLname", UpdatedTeacherInfo.teacherLname);
            schooldbcmd.Parameters.AddWithValue("@employeeNumber", UpdatedTeacherInfo.employeeNumber);
            schooldbcmd.Parameters.AddWithValue("@hireDate", UpdatedTeacherInfo.hireDate);
            schooldbcmd.Parameters.AddWithValue("@saLary", UpdatedTeacherInfo.saLary);
            schooldbcmd.Parameters.AddWithValue("@teacherId", id);


            schooldbcmd.Prepare();

            //excute a non select statment
            schooldbcmd.ExecuteNonQuery();

            Conn.Close();





        }



    }

}

/*CODE HAS BEEN UPDATED TO SHOW TEACHERS WHEN SEARCHED (DEC 1/2020)
 * added in search method and investigated the SQL injection attacks in the teahcers controller.
 * 
 * 
 * 
 * CODE UPDATED (NOV 11/2020)
 * PRACTICE CODE TO SEE WHAT WOULD WORK!!
 ****figure out how to declare date and time for a variable
 *  Teacher.TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string) ResultSet["teacherfname"];
                string TeacherLname = (string) ResultSet["teacherlname"];
                string EmployeeNum = (string) ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

 //accessing column name as an index that will result in a list of teacher name
                string TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
                //the new list we created by called TeacherNames will return a string data type of the resultset
                // and ".add" every fname and lname.
                TeacherNames.Add(TeacherName);

 
 teacherName.teacherid = (int) ResultSet["teacherid"];
                teacherName.teacherfname = (string) ResultSet["teacherfname"];
                teacherName.teacherlname = (string)ResultSet["teacherlname"];
                
             
                TeacherNames.Add(teacherName);

  schoolTeacher.teacherid = (int)ResultSet["teacherid"];
                schoolTeacher.teacherfname = (string)ResultSet["teacherfname"];
                schoolTeacher.teacherlname = (string)ResultSet["teacherlname"];
*/