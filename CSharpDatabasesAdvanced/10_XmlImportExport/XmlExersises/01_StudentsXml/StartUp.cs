using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace P_01StudentsXml
{
    class StartUp
    {
        static void Main(string[] args)
        {
            List<Exam> exams = CreateExams();

            List<Student> students = CreateStudents(exams);

            XmlSerializer serializer = new XmlSerializer(typeof(Student[]));

            using (var writer = new StreamWriter("../../../students.xml"))
            {
                serializer.Serialize(writer, students.ToArray());
            }

            XDocument doc = XDocument.Load("../../../students.xml");

            Console.WriteLine(doc);
        }

        private static List<Student> CreateStudents(List<Exam> exams)
        {
            return new List<Student>()
            {
                new Student("Genovev","Male",DateTime.ParseExact("1990/12/23","yyyy/mm/dd",CultureInfo.InvariantCulture),"34534252345345","haho@gmail.com","Yales","Humanitas","789789789",exams),
                new Student("Genovev","Male",DateTime.Parse("5-5-1911"),"1111111111111111","hihi@gmail.com","Cambridge","Mats","4564576546",exams.Skip(2).ToList()),
                new Student("Genovev","Male",DateTime.Parse("5-5-1944"),"222222222222","lala@gmail.com","SofiaUniversity","Programming","123123123",exams.Take(2).ToList())
            };
        }

        private static List<Exam> CreateExams()
        {
            return new List<Exam>()
            {
                new Exam("Biology",DateTime.Parse("01-01-1644"),5.33m),
                new Exam("Geography",DateTime.Parse("01-01-1544"),3.12m),
                new Exam("History",DateTime.Parse("01-01-1944"),5.12m),
                new Exam("Maths",DateTime.Parse("01-01-1344"),5.99m),
                new Exam("French",DateTime.Parse("01-01-1444"),4.12m),
            };
        }
    }
}
