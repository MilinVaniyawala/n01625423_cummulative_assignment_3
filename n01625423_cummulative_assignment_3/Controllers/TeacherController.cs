using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using n01625423_cummulative_assignment_3.Models;

namespace n01625423_cummulative_assignment_3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        // GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        //GET : /Teacher/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, string Salary)
        {
            // the inputs provided from the form
            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = TeacherFname;
            NewTeacher.TeacherLName = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        // GET : /Teacher/Update/{id}
        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/5</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectTeacher = controller.FindTeacher(id);

            return View(SelectTeacher);
        }

        // POST : /Teacher/Update/{id}
        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="EmployeeNumber">The updated bio of the teacher.</param>
        /// <param name="Salary">The updated email of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the author.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Owen",
        ///	"TeacherLname":"Laing",
        ///	"EmployeeNumber":"L874",
        ///	"Salary":"85.65"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, string Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFName = TeacherFname;
            TeacherInfo.TeacherLName = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            int result = controller.UpdateTeacher(id, TeacherInfo);

            if (result == 0)
            {
                // Validation failed: Please check the input data.
                ViewBag.Result = 0;
            }
            else if (result == -1)
            {
                // An error occurred during validation.
                ViewBag.Result = -1;
            }
            return RedirectToAction("Show/" + id);
        }

        // GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        // POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.RemoveTeacher(id);
            return RedirectToAction("List");
        }
    }
}