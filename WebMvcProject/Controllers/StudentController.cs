using Microsoft.AspNetCore.Mvc;
using WebMvcProject.Data;

namespace WebMvcProject.Controllers
{
    public class StudentController : Controller
    {

        ProjectDbContext db = new ProjectDbContext();
        public IActionResult Index()
        {
           
            List<Student> studentlist = db.Students.ToList();
            return View(studentlist);
                                 
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
           Student student = db.Students.Find(id);
           return View(student);

        }

        [HttpPost]
        public IActionResult Update(Student student) 
        {
            db.Students.Update(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student student = db.Students.Find(id);
            return View(student);

        }

        [HttpPost]
        public IActionResult Delete(Student student)
        {
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
