using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMvcProject.Data;
using WebMvcProject.Models;

namespace WebMvcProject.Controllers
{
	public class CourseController : Controller
	{
		ProjectDbContext db = new ProjectDbContext();

		[HttpGet]
		public IActionResult Index(int sayfa, string aranan = "", string fiyatSiralama = "asc")
		{
			var courseList = db.Courses.Include(x => x.CourseTeacher).Include(x => x.CourseStudents).Where(x => x.StartDate.Value > DateTime.Now.Date).Where(x => EF.Functions.Like(x.Name, "%" + aranan + "%")).OrderBy(x => x.StartDate).ToList();

			if (fiyatSiralama == "asc")
			{
				courseList = courseList.OrderBy(x => x.Price).ToList();
			}
			else if (fiyatSiralama == "desc")
			{
				courseList = courseList.OrderByDescending(x => x.Price).ToList();
			}

			courseList = courseList.Skip((sayfa - 1) * 5).Take(5).ToList();

			double kayitSayisi = Convert.ToDouble(db.Courses.Where(x => x.StartDate.Value > DateTime.Now.Date).Where(x => EF.Functions.Like(x.Name, "%" + aranan + "%")).Count());

			int sayfaSayisi = Convert.ToInt32(Math.Ceiling(kayitSayisi / 5));

			ViewBag.SayfaSayisi = sayfaSayisi;
			ViewBag.OncekiSayfa = sayfa == 1 ? 1 : sayfa - 1;
			ViewBag.SonrakiSayfa = sayfa == sayfaSayisi ? sayfaSayisi : sayfa + 1;

			ViewBag.Aranan = aranan;
			ViewBag.Sayfa = sayfa;
			ViewBag.FiyatSiralama = fiyatSiralama;

			return View(courseList);
		}


		[HttpGet]
		public IActionResult AssignTeacherToCourse()
		{

			ViewBag.Teachers = db.Teachers.ToList();
			ViewBag.Courses = db.Courses.ToList();

			return View();
		}
		[HttpPost]
		public IActionResult AssignTeacherToCourse(CourseTeacher courseTeacher)
		{
			ViewBag.Teachers = db.Teachers.ToList();
			ViewBag.Courses = db.Courses.ToList();


			if (ModelState.IsValid)
			{
				Course course = db.Courses.FirstOrDefault(x => x.Name == courseTeacher.CourseName);
				course.CourseTeacherId = courseTeacher.TeacherId;

				db.Courses.Update(course);
				db.SaveChanges();
			}


			return RedirectToAction("Index");

		}

		[HttpGet]
		public IActionResult AddStudent(int courseId)
		{
            Course course = db.Courses.Include(x => x.CourseTeacher).Include(x => x.CourseStudents).FirstOrDefault(x => x.Id == courseId);

            if (course == null)
			{
				return NotFound();
			}

			ViewBag.CourseId = course.Id;
			ViewBag.CourseName = course.Name;
			ViewBag.TotalHours = course.TotalHourse;
			ViewBag.CourseTeacher = course.CourseTeacher?.Name + " " + course.CourseTeacher?.Surname;
			ViewBag.CourseStudents = course.CourseStudents?.ToList();

			ViewBag.Students = db.Students.ToList();
            ViewBag.SelectedStudentIds = course.CourseStudents.Select(s => s.Id).ToList();
            return View();
		}

		[HttpPost]
		public IActionResult AddStudent(int courseId, List<int> studentIds)
		{
			if (studentIds != null && studentIds.Any())
			{
				Course course = db.Courses.Include(x => x.CourseStudents).FirstOrDefault(x => x.Id == courseId);
				if (course != null)
				{
					List<Student> selectedStudents = db.Students.Where(s => studentIds.Contains(s.Id)).ToList();
					course.CourseStudents.AddRange(selectedStudents);
					db.SaveChanges();
				}
			}

			return RedirectToAction("Index");
		}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Course course = db.Courses.Find(id);
            return View(course);

        }

        [HttpPost]
        public IActionResult Update(Course course)
        {
            db.Courses.Update(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Course course = db.Courses.Find(id);
            return View(course);

        }

        [HttpPost]
        public IActionResult Delete(Course course)
        {
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
