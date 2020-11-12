using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Assignment3_N01450753_WafaMustafa_5101B.Models;

namespace Assignment3_N01450753_WafaMustafa_5101B.Controllers
{
    public class TeacherController : Controller
    {
        //USED INDEX PAGE TO CREAT BUTTONS FOR TEACHERS AND STUDENTS
        // GET: Teacher
        public ActionResult index()
        {
            return View();
        }


        // GET: Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> TeacherNames= controller.ListTeachers();
            return View(TeacherNames);
        }


        //GET: Teacher/Show
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher moreteacherInfo = controller.Teacherinfo(id);

            return View(moreteacherInfo);
        
        }

    }
}