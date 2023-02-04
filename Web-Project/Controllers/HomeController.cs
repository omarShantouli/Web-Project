using khalil_testing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web_Project.Models;

namespace Web_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext context;

        public HomeController(MyContext c)
        {
            context = c;
        }



        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public bool checkSessionData()
        {
            return HttpContext.Session.GetInt32("StudentId") != null;
        }

        public IActionResult Admin()
        {

            if (!checkSessionData())
                return RedirectToAction("Index", "Login");


            List<Section> sections = context.sections.ToList();

            List<Dictionary<string, dynamic>> courses = new List<Dictionary<string, dynamic>>();

            foreach (Section s in sections)
            {
                Dictionary<string, dynamic> temp = new Dictionary<string, dynamic>();
                Course cour = context.course.Where(c => c.Id == s.CourseId).ToList().First();
                temp["courseName"] = cour.Name;
                temp["courseHour"] = cour.Hours;
                temp["sectionName"] = s.Name;
                temp["days"] = s.Days;
                courses.Add(temp);
            }



            var sortedList = courses.OrderBy(d => d["courseName"]).ToList();

            ViewBag.courses = sortedList;
            ViewBag.admin = 1;
            return View("Index");
        }

        public IActionResult Index()
        {
            
            if (!checkSessionData())
                return RedirectToAction("Index", "Login");

            
            
            int StudentId = (int)HttpContext.Session.GetInt32("StudentId");
            Student student = context.students.Where(e => e.Id == StudentId).ToList().First();


            List<StudentSection> studentSection= context.studentsSection.Where(s=>s.StudentId== student.Id).ToList();
            List<Section> sections = new List<Section>();
            foreach (StudentSection s in studentSection)
            {
                Section sec = context.sections.Where(ss => ss.Id == s.SectionId).ToList().First();
                sections.Add(sec);
            }

            List<Dictionary<string, dynamic>> courses = new List<Dictionary<string, dynamic>>();

            foreach (Section s in sections)
            {
                Dictionary<string, dynamic> temp = new Dictionary<string, dynamic>();
                Course cour = context.course.Where(c => c.Id == s.CourseId).ToList().First();
                temp["courseName"] = cour.Name;
                temp["courseHour"] = cour.Hours;
                temp["sectionName"] = s.Name;
                temp["days"] = s.Days;
                courses.Add(temp);
            }



            ViewBag.name = context.students.Where(s => s.Id == StudentId).ToList().First().Name;
            ViewBag.courses = courses;
            return View();
        }

        public IActionResult Assignments()
        {
            if (!checkSessionData())
                return RedirectToAction("Index", "Login");

            int StudentId = (int)HttpContext.Session.GetInt32("StudentId");
            Student student = context.students.Where(e => e.Id == StudentId).ToList().First();


            List<StudentSection> studentSection = context.studentsSection.Where(s => s.StudentId == student.Id).ToList();
            List<Section> sections = new List<Section>();
            foreach (StudentSection s in studentSection)
            {
                Section sec = context.sections.Where(ss => ss.Id == s.SectionId).ToList().First();
                sections.Add(sec);
            }

            List<Dictionary<string, dynamic>> assignments = new List<Dictionary<string, dynamic>>();

            foreach (Section s in sections)
            {
                Dictionary<string, dynamic> temp = new Dictionary<string, dynamic>();
                Assignments assign = context.assignments.Where(a => a.SecionId == s.Id).ToList().First();
                temp["assignmentName"] = assign.AssignmentName;
                temp["assignmentDesc"] = assign.Description;
                temp["assignmentSub"] = assign.SubmitingTime;
                temp["assignmentMaxScore"] = assign.MaxScore;
                temp["sectionName"] = s.Name;
                temp["courseName"] = context.course.Where(c=>c.Id == s.CourseId).ToList().First().Name;

                assignments.Add(temp);
            }

            ViewBag.name = context.students.Where(s => s.Id == StudentId).ToList().First().Name;
            ViewBag.assignments = assignments;

            return View();
        }
        public IActionResult ViewForm()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            context.course.Add(course);
            context.SaveChanges();
            int id = context.students.Where(s => s.Email.Equals("admin@gmail.com")).ToList().First().Id;
            Section sec = new Section() { Name= "A", CourseId= course.Id, Days= "WT" };
            context.sections.Add(sec);
            context.SaveChanges();

            
            return RedirectToAction("Admin", "Home");
        }

        [HttpPost]
        public IActionResult Delete()
        {
            
            Course course = context.course.Where(c => c.Name == "Algorithm").ToList().First();
            context.course.Remove(course);
            context.SaveChanges();
            return RedirectToAction("Admin", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
