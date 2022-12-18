using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3_n01540458.Models;

namespace Assignment3_n01540458.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string searchKey)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();

            // Search functionality to find teacher in the list of teachers
            List<Teacher> NewTeachers = new List<Teacher> {};
            foreach (Teacher Teacher in Teachers)
            {
                if(searchKey != null)
                {
                    if ((Teacher.TeacherFname + " " + Teacher.TeacherLname).ToLower().Contains(searchKey.ToLower()))
                    {
                        NewTeachers.Add(Teacher);
                    }
                }
                else
                {
                    NewTeachers.Add(Teacher);
                }
            }
            IEnumerable<Teacher> Teachers2 = NewTeachers;
            return View(Teachers2);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string TeacherEmpNo, string TeacherSalary)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherEmpNo);
            Debug.WriteLine(DateTime.Now);
            Debug.WriteLine(TeacherSalary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.TeacherEmployeeNo = TeacherEmpNo;
            NewTeacher.TeacherHiredate = DateTime.Now;
            NewTeacher.TeacherSalary = (float)Math.Round(float.Parse(TeacherSalary, CultureInfo.InvariantCulture.NumberFormat), 2);

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the author and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/20</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="TeacherEmployeeNo">The updated employee number of teacher.</param>
        /// <param name="TeacherHiredate">The updated hiredate of teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/20
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Sonia",
        ///	"TeacherLname":"Tarik",
        ///	"TeacherEmployeeNo":"EMP1234",
        ///	"TeacherHiredate":"2022-11-30 10:10 AM"
        ///	"TeacherSalary":"535.11"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string TeacherEmpNo, string TeacherSalary)
        {
            Teacher teacherInfo = new Teacher();
            teacherInfo.TeacherFname = TeacherFname;
            teacherInfo.TeacherLname = TeacherLname;
            teacherInfo.TeacherEmployeeNo = TeacherEmpNo;
            teacherInfo.TeacherHiredate = DateTime.Now;
            teacherInfo.TeacherSalary = (float)Math.Round(float.Parse(TeacherSalary, CultureInfo.InvariantCulture.NumberFormat), 2);

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, teacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}