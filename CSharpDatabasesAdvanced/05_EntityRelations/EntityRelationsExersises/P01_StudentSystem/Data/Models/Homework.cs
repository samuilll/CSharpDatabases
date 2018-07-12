using System;

namespace P01_StudentSystem.Data.Models
{
   public class Homework
    {
        public Homework()
        {

        }

        public Homework(string content, ContentType contentType, DateTime submissionTime, Student student, Course course)
        {
            Content = content;
            ContentType = contentType;
            SubmissionTime = submissionTime;
            Student = student;
            Course = course;
        }

        public int HomeworkId { get; set; }

        public string Content { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime SubmissionTime { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
