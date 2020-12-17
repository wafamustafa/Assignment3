using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace Assignment3_N01450753_WafaMustafa_5101B.Models
{
    public class Teacher
    {
        public int teacherId;
        public string teacherFname;
        public string teacherLname;
        public string employeeNumber;
        public DateTime hireDate;
        public decimal saLary;

        //references from your blogproject 
        public bool IsValid()
        {
            bool valid = true;


            //taking out salary from validation as it might be subject to change after hire
            if (teacherFname == null || teacherLname == null || employeeNumber == null)
            {
                // if any of the above fields listed in the if statement return null
                valid = false;

            }
            else
            {
                if (teacherFname.Length <= 3 || teacherFname.Length > 200) valid = false;
                if (teacherLname.Length <= 3 || teacherLname.Length > 200) valid = false;
                if (employeeNumber.Length <= 2 || employeeNumber.Length > 5) valid = false;
            }

            return valid;
        }

        //referance from your video to add in the database
        public Teacher() { }
    }
}