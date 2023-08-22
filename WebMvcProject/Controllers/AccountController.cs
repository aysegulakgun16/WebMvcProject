using Microsoft.AspNetCore.Mvc;
using WebMvcProject.Data;
using WebMvcProject.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace WebMvcProject.Controllers
{
    public class AccountController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    User user = new User();
                    user.Email = model.Email;
                    user.UserName = model.UserName;

                    string salt = BCrypt.Net.BCrypt.GenerateSalt();

                    string passHash = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);
                    user.PasswordSalt = salt;
                    user.PasswordHash = passHash;

                    db.Users.Add(user);
                    db.SaveChanges();

                    ViewBag.Message = "Kayıt Başarılı. Sisteme Giriş Yapabilirsiniz.";


                }
                catch (Exception)
                {

                    ModelState.AddModelError("Email","Aynı E-Posta Adresinden Mevcut");
                    ModelState.AddModelError("UserName", "Aynı UserName'den Mevcut");


                   
                }

            }
            return View();
        }

        [HttpGet]
        public IActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
              var user  = db.Users.Include(x => x.Roles).FirstOrDefault(y => y.UserName == model.UserName);

                if (user != null)
                {
                    var hasPass = BCrypt.Net.BCrypt.HashPassword(model.Password,user.PasswordSalt);

                    if (user.PasswordHash == hasPass)
                    {
                        List<Claim> claims = new List<Claim>();

                        var claim1 = new Claim(ClaimTypes.Name, user.UserName);
                        var claim2 = new Claim(ClaimTypes.Email, user.UserName);

                        claims.Add(claim1);
                        claims.Add(claim2);

                        foreach (var role in user.Roles)
                        {
                            var claim3 = new Claim(ClaimTypes.Role, role.Name);

                            claims.Add(claim3);

                        }

                        var identity = new ClaimsIdentity(claims, "3526");

                        var claimPrinciple = new ClaimsPrincipal(identity);

                        var authProps = new AuthenticationProperties();

                        authProps.IsPersistent = model.RememberMe;

                        if (model.RememberMe)
                        {
                            authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30);
                        }

                        HttpContext.SignInAsync(claimPrinciple, authProps);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Parola Doğru Değil");
                        return View();
                    }
                }
            }

            return View();
        }


        [Authorize]
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync("3526");

            return RedirectToAction("Index","Home");

        }
    }
}
