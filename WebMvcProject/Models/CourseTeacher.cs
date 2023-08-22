using System.ComponentModel.DataAnnotations;

namespace WebMvcProject.Models
{
	public class CourseTeacher
	{
		[Required(ErrorMessage = "Kurs Seçimi Yapınız")]
		public string? CourseName { get; set; }
		[Required(ErrorMessage = "Öğretmen Seçimi Yapınız")]
		public int? TeacherId { get; set;}
	}
}
