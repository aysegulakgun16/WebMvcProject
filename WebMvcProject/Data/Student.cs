namespace WebMvcProject.Data
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
        public string PhoneNumber { get; set; }

        public List<Course> Courses { get; set; } 

    }
}
