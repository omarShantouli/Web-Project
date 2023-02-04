using khalil_testing.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
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

        public IActionResult Logout()
        {
        
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
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
            
            if (student.Email.Equals("admin@gmail.com"))
            {
                if (student.Password.Equals("1111"))
                {
                    Student admin = context.students.Where(s => s.Email.Equals("admin@gmail.com")).ToList().First();
                    HttpContext.Session.SetInt32("StudentId", admin.Id);
                    ViewBag.name = "admin";
                    return RedirectToAction("Admin", "Home");
                }
            }


            foreach (Student s in students)
            {
                if (student.Email.Equals(s.Email))
                {
                    if (student.Password.Equals(s.Password))
                    {
                        HttpContext.Session.SetInt32("StudentId", s.Id);
                        ViewBag.name = s.Name;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            string message = "The email address or password is incorrect. Please retry...";
            ViewBag.Error = message;

             
            return View("Index");
        }

        [HttpPost]

        public IActionResult AddStudent(Student student)
        {

            List<Section> sections = context.sections.ToList();
            List<Student> students = context.students.ToList();

            foreach (Student s in students)
            {
                if(s.Email.Equals(student.Email))
                {
                    string message = "account is already exsits, please login!";
                    ViewBag.fillError = message;
                    ViewBag.sections = sections;
                    return View("SignUp");
                }
            }

            Student StudentToAdd = new Student { Name = student.Name, Email = student.Email, Password = student.Password };
            context.students.Add(StudentToAdd);
            context.SaveChanges();
            HttpContext.Session.SetInt32("StudentId", StudentToAdd.Id);
            return RedirectToAction("Index", "Home");
            
        }

    }
}