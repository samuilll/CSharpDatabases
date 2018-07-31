using System;
using System.Xml.Serialization;

[XmlType]
public class Exam
{
    public Exam()
    {

    }
    public Exam(string name, DateTime dateTaken, decimal grade)
    {
        Name = name;
        DateTaken = dateTaken;
        Grade = grade;
    }

    [XmlElement("examName")]
    public string Name { get; set; }
    [XmlElement("examDateTaken")]
    public DateTime DateTaken { get; set; }
    [XmlElement("grade")]
    public decimal Grade { get; set; }
}