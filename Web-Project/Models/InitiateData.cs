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
            

            Course course1 = new Course { Name = "Operating Systems", Hours=3};
            Course course2 = new Course { Name = "Web Programming", Hours = 3};
            Course course3 = new Course { Name = "Artificial Intelligence", Hours = 3};
            Course course4 = new Course { Name = "Electronics Lab", Hours = 1};
            context.course.Add(course1);
            context.course.Add(course2);
            context.course.Add(course3);
            context.course.Add(course4);
            context.SaveChanges();

            Section section1 = new Section { Name= "A", CourseId=course1.Id, Days = "MT" };
            Section section2 = new Section { Name = "B", CourseId = course1.Id, Days = "WT" };
            Section section3 = new Section { Name = "A", CourseId = course2.Id, Days = "MT" };
            Section section4 = new Section { Name = "B", CourseId = course2.Id , Days = "MT" };
            Section section5 = new Section { Name = "C", CourseId = course2.Id , Days = "WT" };
            Section section6 = new Section { Name = "A", CourseId = course3.Id , Days = "WT" };
            Section section7 = new Section { Name = "A", CourseId = course4.Id, Days = "N" };


            context.sections.Add(section1);
            context.sections.Add(section2);
            context.sections.Add(section3);
            context.sections.Add(section4);
            context.sections.Add(section5);
            context.sections.Add(section6);
            context.sections.Add(section7);
            context.SaveChanges();


            Student student1 = new Student { Name = "Omar Hantouli", Email = "omar@gmail.com", Password = "1234"};
            Student student2 = new Student { Name = "Jameel Sawafta", Email = "jameel@gmail.com", Password = "0000"};
            Student student3 = new Student { Name = "Ahmed Ghannam", Email = "ahmed@gmail.com", Password = "0000"};
            Student student4 = new Student { Name = "Abeer Saqf-al-hayt", Email = "abeer@gmail.com", Password = "0000"};

            context.students.Add(student1);
            context.students.Add(student2);
            context.students.Add(student3);
            context.students.Add(student4);
            context.SaveChanges();

            StudentSection[] studentSection1 = new StudentSection[]
            {
                new StudentSection {StudentId= student1.Id, SectionId= section1.Id},
                new StudentSection {StudentId= student1.Id, SectionId= section3.Id},
                new StudentSection {StudentId= student1.Id, SectionId= section6.Id},
                new StudentSection {StudentId= student1.Id, SectionId= section7.Id},
                new StudentSection {StudentId= student2.Id, SectionId= section1.Id},
                new StudentSection {StudentId= student2.Id, SectionId= section3.Id},
                new StudentSection {StudentId= student3.Id, SectionId= section2.Id},
                new StudentSection {StudentId= student3.Id, SectionId= section4.Id},
                new StudentSection {StudentId= student4.Id, SectionId= section2.Id},
                new StudentSection {StudentId= student4.Id, SectionId= section7.Id}
            };


            foreach(StudentSection studentSection in studentSection1)
            {
                context.studentsSection.Add(studentSection);
            }

            context.SaveChanges();

          

            Assignments[] assignments = new Assignments[]
            {
                    new Assignments { AssignmentName="Assignment1", Description="description for assginment1",SubmitingTime="12:00", MaxScore=10, SecionId= section1.Id },
                    new Assignments { AssignmentName="Assignment2", Description="description for assginment2",SubmitingTime="10:00", MaxScore=15, SecionId= section2.Id },
                    new Assignments { AssignmentName="Assignment3", Description="description for assginment3",SubmitingTime="15:30", MaxScore=15, SecionId= section3.Id },
                    new Assignments { AssignmentName="Assignment4", Description="description for assginment4",SubmitingTime="05:00", MaxScore=20, SecionId= section4.Id },
                    new Assignments { AssignmentName="Assignment5", Description="description for assginment5",SubmitingTime="11:00", MaxScore=10, SecionId= section5.Id },
                    new Assignments { AssignmentName="Assignment6", Description="description for assginment6",SubmitingTime="15:00", MaxScore=20, SecionId= section6.Id },
                    new Assignments { AssignmentName="Assignment7", Description="description for assginment7",SubmitingTime="07:00", MaxScore=15, SecionId= section7.Id },
                   
                };

            foreach (Assignments assignment in assignments)
            {
                context.assignments.Add(assignment);
            }

            context.SaveChanges();

        }
    }
}