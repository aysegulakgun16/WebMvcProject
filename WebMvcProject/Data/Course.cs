﻿using System.ComponentModel.DataAnnotations;

namespace WebMvcProject.Data
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Kurs Adı Boş Geçilemez")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kurs İçeriği Girmek Zorundasınız")]
        [MaxLength(250)]
        public string Content { get; set; }
        [Range(45000,65000,ErrorMessage ="Kurs Fiyatı 45 binle 65 bin Arasında Olabilir")]
       
        public decimal? Price  { get; set; }
        [Range(200, 300, ErrorMessage = "Kurslar 200 ile 300 Saat Arasında Planlanabiilir")]
        public int? TotalHourse { get; set; }
        [Required(ErrorMessage = "Başlangıç Saati Boş Geçilemez")]
        
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "Bitiş Saati Boş Geçilemez")]
        public DateTime? EndDate { get; set;}

        public int? CourseTeacherId { get; set; }

        public Teacher? CourseTeacher { get; set; }

      

        public List<Student> CourseStudents { get; set; } = new List<Student>();



    }
}
