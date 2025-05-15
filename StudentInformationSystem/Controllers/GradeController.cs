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
    public class GradeController : Controller
    {
        private readonly SchoolDbContext _context;

        public GradeController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Grade
        public async Task<IActionResult> Index(string search)
        {
            var grades = from g in _context.Grades
                         select g;

            if (!String.IsNullOrEmpty(search))
            {
                grades = grades.Where(g => g.StudentNumber.Contains(search));
            }

            return View(await grades.ToListAsync());
        }

        // GET: Grade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grade/Create
        public IActionResult Create()
        {
            ViewBag.StudentList = new SelectList(_context.Students, "StudentNumber", "StudentNumber");
            ViewBag.LessonList = new SelectList(_context.Lessons, "Code", "Code");
            return View();
        }

        // POST: Grade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentNumber,Code,GradeValue")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == grade.StudentNumber);
                var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Code == grade.Code);

                if (student == null || lesson == null)
                {
                    return NotFound();
                }

               
                bool gradeExists = await _context.Grades.AnyAsync(g => g.StudentNumber == grade.StudentNumber && g.Code == grade.Code);
                if (gradeExists)
                {
                    ModelState.AddModelError("", "This student already has a grade for the selected lesson.");
                    ViewBag.StudentList = new SelectList(_context.Students, "StudentNumber", "StudentNumber", grade.StudentNumber);
                    ViewBag.LessonList = new SelectList(_context.Lessons, "Code", "Code", grade.Code);
                    return View(grade);
                }

                grade.StudentId = student.Id;
                grade.LessonId = lesson.Id;

                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.StudentList = new SelectList(_context.Students, "StudentNumber", "StudentNumber", grade.StudentNumber);
            ViewBag.LessonList = new SelectList(_context.Lessons, "Code", "Code", grade.Code);
            return View(grade);
        }

        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: Grade/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentNumber,Code,GradeValue")] Grade grade)
        {
            if (id != grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}
