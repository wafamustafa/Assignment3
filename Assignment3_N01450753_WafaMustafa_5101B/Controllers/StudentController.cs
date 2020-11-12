using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3_N01450753_WafaMustafa_5101B.Models;

namespace Assignment3_N01450753_WafaMustafa_5101B.Controllers
{
    public class StudentController : Controller
    {
        // USED INDEX PAGE TO CREAT BUTTONS FOR TEACHERS AND STUDENTS
        //GET: Student
        public ActionResult Index()
        {
            return View();
     
        }

        // GET: Student/List
        public ActionResult ListStudents()
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Students> StudentNames = controller.ListStudents();
            return View(StudentNames);
        }


        //GET: Student/Show
        public ActionResult ShowStudents(int id)
        {
            StudentDataController controller = new StudentDataController();
            Students morestudentInfo = controller.Studentinfo(id);

            return View(morestudentInfo);

        }
    }


}