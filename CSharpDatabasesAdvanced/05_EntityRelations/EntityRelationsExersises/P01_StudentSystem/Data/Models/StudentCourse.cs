﻿namespace P01_StudentSystem.Data.Models
{
   public class StudentCourse
    {
        public StudentCourse()
        {

        }

        public StudentCourse(Student student, Course course)
        {
            Student = student;
            Course = course;
        }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
