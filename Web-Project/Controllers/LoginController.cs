using khalil_testing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Web_Project.Models;

namespace Web_Project.Controllers
{
    public class LoginController : Controller
    {

        private readonly MyContext context;
        public LoginController(MyContext c)
        {
            context = c;
        }


        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult SignUp()
        {
            List<Section> sections = context.sections.ToList();
            ViewBag.sections = sections;

            return View();
        }
        [HttpPost]
        public IActionResult CheckLogin(Student student)
        {

            
                List<Student> students = context.students.ToList();
                foreach (Student s in students)
                {
                    if (student.Email.Equals(s.Email))
                    {
                        if (student.Password.Equals(s.Password))
                        {
                            HttpContext.Session.SetInt32("UserId", s.Id);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            
            string message = "an error occured!";
            ViewBag.fillError = message;
            return View("Index");
        }

        [HttpPost]

        public IActionResult AddStudent(Student student) 
        {


            Student StudentToAdd = new Student { Name = student.Name, Email = student.Email, Password = student.Password, SectionId = student.SectionId };
            context.students.Add(StudentToAdd);
            context.SaveChanges();
            HttpContext.Session.SetInt32("UserId", StudentToAdd.Id);
            return RedirectToAction("Index", "Home");
            

            List<Section> sections = context.sections.ToList();
            string message = "an error occured!";
            ViewBag.fillError = message;
            ViewBag.sections = sections;
            return View("SignUp");
        }
    }
}
