using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Controllers
{
    [Authorize(Roles = "student")]
    public class StudentMainController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentMainController(SchoolDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Student/DetailsByIdentityNumber/12345678
        public async Task<IActionResult> DetailsByIdentityNumber(string identityNumber)
        {
            if (string.IsNullOrEmpty(identityNumber))
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.IdentityNumber == identityNumber);
            if (student == null)
            {
                return NotFound();
            }

            var announcements = await _context.Announcements.ToListAsync();
            ViewBag.Announcements = announcements;

            return View("MyDetails", student); 
        }

        public IActionResult LessonPlan()
        {
            return View();
        }
        public IActionResult AcademicCalendar()
        {
            return View();
        }

       
        public async Task<IActionResult> MyGrades(string identityNumber)
        {
            if (string.IsNullOrEmpty(identityNumber))
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.IdentityNumber == identityNumber);

            if (student == null)
            {
                return NotFound();
            }

            // Öğrenciye ait notları çekmek
            var grades = await _context.Grades
            .Where(g => g.StudentNumber == student.StudentNumber)
            .ToListAsync();

            foreach (var grade in grades)
            {
                // Ders adını çekmek için ilgili dersin koduna göre ilgili dersi bulun
                var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Code == grade.Code);

                // Eğer ders bulunamazsa, uyarı verebilirsiniz veya herhangi bir işlem yapabilirsiniz
                if (lesson != null)
                {
                    // Ders adını not nesnesine atayın
                    grade.LessonName = lesson.Name;
                }
            }
            
            // Görünüme öğrenci ve notları bir arada gönder
            ViewBag.Student = student;
            ViewBag.Grades = grades;
            
            return View();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(s => s.Id == id);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentityNumber,StudentNumber,Name,Surname,Gender,BirthDate,PhoneNumber,Email,Address")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    // Genel bir hata mesajı göster
                    ModelState.AddModelError("", "An error occurred while saving your changes.");
                    return View(student);
                }
            }
            return View(student);
        }
        [Authorize(Roles = "student")]
        public async Task<IActionResult> Inbox()
        {
            var identityNumber = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(identityNumber))
                return Unauthorized();

            var messages = await _context.Messages
                .Where(m => m.ReceiverIdentityNumber == identityNumber)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();

            return View(messages);
        }




    }
}