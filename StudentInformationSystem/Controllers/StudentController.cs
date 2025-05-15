using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Controllers
{
    [Authorize(Roles = "admin, teacher")]
    public class StudentController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index(string search)
        {
            ViewData["Layout"] = "~/Views/Shared/_StudentLayout.cshtml";

            var students = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                students = students.Where(s =>
                    EF.Functions.Like(s.Name, $"%{search}%") ||
                    EF.Functions.Like(s.Surname, $"%{search}%"));
            }

            return View(await students.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdentityNumber,StudentNumber,Name,Surname,Gender,BirthDate,PhoneNumber,Email,Address")] Student student)
        {
          
            if (_context.Students.Any(s => s.Email == student.Email))
            {
                ModelState.AddModelError(nameof(student.Email), "This email already exists.");
            }

            if (_context.Students.Any(s => s.StudentNumber == student.StudentNumber))
            {
                ModelState.AddModelError(nameof(student.StudentNumber), "This student number already exists.");
            }

            if (!ModelState.IsValid)
                return View(student);

            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string email)
        {
            var exists = _context.Students.Any(s => s.Email == email);
            return exists ? Json("This email already exists.") : Json(true);
        }

     
        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        public IActionResult ValidateStudentNumber(string studentNumber)
        {
            var exists = _context.Students.Any(s => s.StudentNumber == studentNumber);
            return exists ? Json("This student number already exists.") : Json(true);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentityNumber,StudentNumber,Name,Surname,Gender,BirthDate,PhoneNumber,Email,Address")] Student student)
        {
            if (id != student.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
                _context.Students.Remove(student);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
