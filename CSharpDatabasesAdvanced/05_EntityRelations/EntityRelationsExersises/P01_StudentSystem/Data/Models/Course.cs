﻿using System;
using System.Collections.Generic;

namespace P01_StudentSystem.Data.Models
{
  public  class Course
    {
        public Course()
        {

        }

        public Course(string name, string description, DateTime startDate, DateTime endDate, decimal price)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
        }

        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<Homework> HomeworkSubmissions { get; set; }

        public ICollection<Resource> Resources { get; set; }

        public ICollection<StudentCourse> StudentsEnrolled { get; set; }



    }
}
