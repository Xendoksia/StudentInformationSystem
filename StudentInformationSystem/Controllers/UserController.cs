using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly SchoolDbContext _context;

        public UserController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index(string search)
        {
            var users = _context.Users.AsQueryable();

            if (!String.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.Username.Contains(search));
            }

            return View(await users.ToListAsync());
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Role,IdentityNumber")] User user)
        {
            // 1️⃣ Server-side uniqueness check (IdentityNumber zaten varsa hata ekle)
            if (_context.Users.Any(u => u.IdentityNumber == user.IdentityNumber))
            {
                ModelState.AddModelError(nameof(user.IdentityNumber), "This identity already exists.");
            }

            // 2️⃣ ModelState'i kontrol et (hem Required hem Remote Validation hataları burada toplanır)
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            // 3️⃣ Eğer bu identity öğrenci tablosunda varsa kullanıcı adını öğrenciden al, yoksa default üret
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.IdentityNumber == user.IdentityNumber);

            if (student != null)
            {
                user.Username = $"{student.Name.ToLower()}.{student.Surname.ToLower()}";
            }
            else if (string.IsNullOrWhiteSpace(user.Username))
            {
                var last4 = user.IdentityNumber.Length >= 4
                    ? user.IdentityNumber[^4..]
                    : "user";
                user.Username = $"user{last4}";
            }

            // 4️⃣ Şifre boşsa, frontend'de göründüğü gibi rastgele oluştur
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                user.Password = GenerateRandomPassword(8);
            }

            // 5️⃣ Veritabanına ekle ve kaydet
            _context.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateIdentityNumber(string identityNumber)
        {
            bool exists = _context.Users.Any(u => u.IdentityNumber == identityNumber);
            return exists ? Json("This identity already exists.") : Json(true);
        }

        private string GenerateRandomPassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable
                .Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }


        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Role,IdentityNumber")] User user)
        {
            if (id != user.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // GET: User/Login
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == model.Username &&
                u.IdentityNumber == model.IdentityNumber &&
                u.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username, password, or identity number.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.IdentityNumber) // Bu çok önemli!
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            switch (user.Role)
            {
                case "admin":
                    return RedirectToAction("Index", "Admin");
                case "teacher":
                    return RedirectToAction("Index", "TeacherMain");
                case "student":
                    return RedirectToAction("DetailsByIdentityNumber", "StudentMain", new { identityNumber = user.IdentityNumber });
                default:
                    ModelState.AddModelError(string.Empty, "Unknown user role.");
                    return View(model);
            }
        }

        // LOGOUT
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

        private bool UserExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }
    }
}
