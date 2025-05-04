using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformationSystem.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize(Roles = "teacher")]
public class MessageController : Controller
{
    private readonly SchoolDbContext _context;

    public MessageController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult SendMessage()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendMessage(Message message)
    {
        if (ModelState.IsValid)
        {
            var senderIdentity = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Güvenlik kontrolü
            if (string.IsNullOrEmpty(senderIdentity))
            {
                ModelState.AddModelError("", "Could not determine sender identity.");
                return View(message);
            }

            message.SentAt = DateTime.Now;

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "TeacherMain");
        }

        return View(message); // ModelState hatalıysa formu tekrar göster
    }
}
