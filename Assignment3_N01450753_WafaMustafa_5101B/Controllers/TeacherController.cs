using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Assignment3_N01450753_WafaMustafa_5101B.Models;
using System.Diagnostics;

namespace Assignment3_N01450753_WafaMustafa_5101B.Controllers
{
    public class TeacherController : Controller
    {
        //USED INDEX PAGE TO CREATE BUTTONS FOR TEACHERS AND STUDENTS
        // GET: Teacher
        public ActionResult index()
        {
            return View();
        }


        // GET: Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> TeacherNames= controller.ListTeachers(SearchKey);
            return View(TeacherNames);
        }


        //GET: Teacher/Show
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher moreteacherInfo = controller.Teacherinfo(id);

            return View(moreteacherInfo);
        
        }

        //GET: Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher moreteacherInfo = controller.Teacherinfo(id);

            return View(moreteacherInfo);
        }

        /* my delete request is not completeing and im not sure why. I did a practise run for my students database and i managed to delete a few student... but unfortunately I wasnt able to do the same. Once I click confirm delete I get HTTP ERROR 404 and the url bar turns to http://localhost:63138/Teacher/DeleteConfirm/Teacher/Delete/13 */


        //POST: Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");

        }


        //GET: Teacher/Add
        public ActionResult Add()
        {
            return View();

        }

        //GET : Teacher/Ajax_Add
        public ActionResult Ajax_Add()
        {
            return View();
        }


        //POST: Teacher/Create
        [HttpPost]
        public ActionResult Create(string teacherFname, string teacherLname, string employeeNumber, DateTime hireDate, decimal saLary)
        {
            //declaring the data types we will be add to the teachers db
            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherFname = teacherFname;
            NewTeacher.teacherLname = teacherLname;
            NewTeacher.employeeNumber = employeeNumber;
            NewTeacher.hireDate = hireDate;
            NewTeacher.saLary = saLary;


            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);
            return RedirectToAction("List");


        }
        //GET: Teacher/Update/{id}
        /// <summary>
        /// Starting point to update teacher info. with this get request we can select the teacher we would like to update
        /// </summary>
        /// <param name="id">list --> show ---> update teacher info</param>
        /// <returns> this will bring us to the update page where old information is already in text boxes ready to be adjusted</returns>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher UpdatedTeacher = controller.Teacherinfo(id);

            // I didnt change the return name in my show method as I think this will give us a better understanding of what this method is returning
            return View(UpdatedTeacher);
            
        }

        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher UpdatedTeacher = controller.Teacherinfo(id);

            return View(UpdatedTeacher);

        }



            /// <summary>
            /// this is where we are able to change info on teachers and have it display on the show page
            /// </summary>
            /// <param name="id">selects the teacher we want to update</param>
            /// <param name="teacherFname"></param>
            /// <param name="teacherLname"></param>
            /// <param name="employeeNumber"></param>
            /// <param name="hireDate"></param>
            /// <param name="saLary"></param>
            /// <returns></returns>

            //POST: Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string teacherFname, string teacherLname, string employeeNumber, DateTime hireDate, decimal saLary)
        {

            Teacher UpdatedTeacherInfo = new Teacher();
            UpdatedTeacherInfo.teacherFname = teacherFname;
            UpdatedTeacherInfo.teacherLname = teacherLname;
            UpdatedTeacherInfo.employeeNumber = employeeNumber;
            UpdatedTeacherInfo.hireDate = hireDate;
            UpdatedTeacherInfo.saLary = saLary;


            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, UpdatedTeacherInfo);
            return RedirectToAction("Show/" + id);
        }

    }
}

/****** FOR DEBUGGING
 *  Debug.WriteLine("I have access the create");
 *  Debug.WriteLine(teacherFname);
 *  Debug.WriteLine(teacherLname);
 *  Debug.WriteLine(employeeNumber);
 *  Debug.WriteLine(hireDate);
 *  
 *  
 *  
 *  
 *  ***/