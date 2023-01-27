using khalil_testing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public bool checkSessionData()
        {
            return HttpContext.Session.GetInt32("StudentId") != null;
        }

        public IActionResult Index()
        {
            if(!checkSessionData())
            return RedirectToAction("Index", "Login");

            int StudentId = (int)HttpContext.Session.GetInt32("StudentId");
            Student student = context.students.Where(e => e.Id == StudentId).ToList().First();
            List<Assignments> assignments = context.assignments.Where(e => e.SecionId == student.SectionId).ToList();
            List<Section> sections = context.sections.ToList();

            Dictionary<int, string> sectionMap = new Dictionary<int, string>();
            foreach (Section section in sections)
            {
                sectionMap[section.Id] = section.Name;
            }

            ViewBag.assignments = assignments;
            ViewBag.student = student;
            ViewBag.sections = sectionMap;

            return View();
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
