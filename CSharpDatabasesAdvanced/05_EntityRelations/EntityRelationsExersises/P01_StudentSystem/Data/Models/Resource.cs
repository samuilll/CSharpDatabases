namespace P01_StudentSystem.Data.Models
{
   public class Resource
    {
        public Resource()
        {

        }

        public Resource(string name, string url, ResourceType resourceType, Course course)
        {
            Name = name;
            Url = url;
            ResourceType = resourceType;
            Course = course;
        }

        public int ResourceId { get; set; }

        public string Name{ get; set; }

        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
