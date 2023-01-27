using khalil_testing.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Threading.Tasks;

namespace Web_Project.Models
{
    public static class InitiateData
    {

        public static void Initiate(MyContext context)
        {
            context.Database.EnsureCreated();

            if (context.sections.Any())
            {
                return;
            }
            // method 1

            Section section1 = new Section { Name= "A"};
            Section section2 = new Section { Name = "B" };
            context.sections.Add(section1);
            context.sections.Add(section2);
            context.SaveChanges();



            Student student1 = new Student { Name = "omar hantouli", Email = "omar@gmail.com", Password = "1234", SectionId = section1.Id };
            Student student2 = new Student { Name = "jameel sawafta", Email = "jameel@gmail.com", Password = "0000", SectionId = section2.Id };

            context.students.Add(student1);
            context.students.Add(student2);
            context.SaveChanges();

            // method 2

            Assignments[] assignments = new Assignments[]
            {
                    new Assignments { CourseName="Assignment1", Description="description for assginment1",SubmitingTime="12:00", MaxScore=10, SecionId= section1.Id },
                    new Assignments { CourseName="Assignment2", Description="description for assginment2",SubmitingTime="10:00", MaxScore=15, SecionId= section1.Id },
                    new Assignments { CourseName="Assignment3", Description="description for assginment3",SubmitingTime="15:30", MaxScore=15, SecionId= section2.Id },
                    new Assignments { CourseName="Assignment4", Description="description for assginment4",SubmitingTime="05:00", MaxScore=20, SecionId= section2.Id },
                    new Assignments { CourseName="Assignment5", Description="description for assginment5",SubmitingTime="01:00", MaxScore=10, SecionId= section1.Id },
                   
                };

            foreach (Assignments assignment in assignments)
            {
                context.assignments.Add(assignment);
            }

            context.SaveChanges();

        }
    }
}