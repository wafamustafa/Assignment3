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
        /// Get: api/TeacherData/ListTeacher
        /// </summary>
        /// <returns>
        /// will connect to the School database and return the result of the List of teachers 
        /// with the first name and last name as we have coded our while loop to do
        /// <example>"<string>Alexander Bennett</string>"</example>
        /// </returns>
        [HttpGet]
        [Route("Api/TeacherData/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers()
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
            schooldbcmd.CommandText = "Select * from teachers";

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




    }



}

/*
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