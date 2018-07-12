using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data;
using System;

namespace Students.App
{
    using P01_StudentSystem.Data.Models;

    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new StudentSystemContext())
            {
                //db.Database.EnsureDeleted();

               // db.Database.Migrate();

                Seed(db);
            }
        }

        private static void Seed(StudentSystemContext db)
        {
            var students = new Student[]
            {
                new Student("Ivan","0945043950",new DateTime(2005,2,23)),
                new Student("Pesho","0945043950",new DateTime(2001,2,25)),
                new Student("Stella","0896348238",new DateTime(1983,4,7))
            };

            var courses = new Course[]
                {
                    new Course("Linguistics","MindMappind",new DateTime(2000,1,1),new DateTime(2000,2,2),123.34m),
                    new Course("History","Cesar",new DateTime(1999,1,1),new DateTime(2000,2,2),123.34m),
                    new Course("Biology","Blood",new DateTime(2000,1,1),new DateTime(2000,2,2),123.34m)
                };

            var resources = new Resource[]
                {
                    new Resource("Bimbo","sdlf;dlf;dlsf",ResourceType.Presentation,courses[0]),
                    new Resource("Books","jkdfjkdsjfdsf",ResourceType.Video,courses[2]),
                    new Resource("Lambada","ksajdksjdks",ResourceType.Other,courses[1])
                };
            var homeworks = new Homework[]
                {
                    new Homework("AntientOlimpycGames",ContentType.Pdf,new DateTime(2000,1,1),students[0],courses[0]),
                    new Homework("Mathematics",ContentType.Application,new DateTime(1902,1,1),students[1],courses[0]),
                    new Homework("Dance",ContentType.Zip,new DateTime(1989,1,1),students[2],courses[1]),

                };
            var studentCourses = new StudentCourse[]
                {
                    new StudentCourse(students[0],courses[0]),
                    new StudentCourse(students[0],courses[1]),
                    new StudentCourse(students[2],courses[1])
                };

            db.Students.AddRange(students);
            db.Courses.AddRange(courses);
            db.Resources.AddRange(resources);
            db.HomeworkSubmissions.AddRange(homeworks);
            db.SaveChanges();
            db.StudentCourses.AddRange(studentCourses);
            db.SaveChanges();


        }
    }
}
