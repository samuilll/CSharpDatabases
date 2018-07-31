using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_01StudentsXml
{
[XmlRoot()]
[XmlType]
  public  class Student
    {
        public Student()
        {

        }
        public Student(string name, string gender, DateTime birthDate, string phoneNumber,
            string email, string university, string specialty, string facultyNumber, List<Exam> exams)
        {
            Name = name;
            Gender = gender;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            Email = email;
            University = university;
            Specialty = specialty;
            FacultyNumber = facultyNumber;
            Exams = exams;
        }

        [XmlElement("name")]
    public string Name { get; set; }
        [XmlElement("gender")]
        public string Gender { get; set; }
        [XmlElement("birthDate")]
        public DateTime BirthDate { get; set; }
        [XmlElement("phoneNumber")]
        public string PhoneNumber { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }
        [XmlElement("university")]
        public string University { get; set; }
        [XmlElement("specialty")]
        public string Specialty { get; set; }
        [XmlElement("facultuNumber")]
        public string FacultyNumber { get; set; }
        [XmlArrayItem("exam")]
        public List<Exam> Exams { get; set; }
}
}
