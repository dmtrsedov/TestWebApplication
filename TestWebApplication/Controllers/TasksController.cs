using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestWebApplication.Data;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class TasksController : Controller
    {
        private readonly UserContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TasksController(UserContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var userContext = _context.Tasks.Include(t => t.AssignedUser).Include(t => t.Sprint);
            return View(await userContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.AssignedUser)
                .Include(t => t.Sprint)
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["AssignedUserID"] = new SelectList(_context.Users, "UserID", "Email");
            ViewData["SprintID"] = new SelectList(_context.Sprints, "SprintID", "SprintID");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskID,SprintID,TaskName,Description,Status,Comment,AssignedUserID")] Models.Task task, IFormFile files)
        {
            if (files != null && files.Length > 0)
            {
                var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files/Tasks/");

                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(files.FileName);

                var filePath = Path.Combine(uploadsPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }
                task.Files = "Files/Tasks/" + fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignedUserID"] = new SelectList(_context.Users, "UserID", "Email", task.AssignedUserID);
            ViewData["SprintID"] = new SelectList(_context.Sprints, "SprintID", "SprintID", task.SprintID);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["AssignedUserID"] = new SelectList(_context.Users, "UserID", "Email", task.AssignedUserID);
            ViewData["SprintID"] = new SelectList(_context.Sprints, "SprintID", "SprintID", task.SprintID);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskID,SprintID,TaskName,Description,Status,Comment,AssignedUserID")] Models.Task task, IFormFile files)
        {
            if (id != task.TaskID)
            {
                return NotFound();
            }
            if (files != null && files.Length > 0)
            {
                var uploadsPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files/Sprints/");

                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(files.FileName);

                var filePath = Path.Combine(uploadsPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }
                task.Files = "Files/Tasks/" + fileName;
            }
            else
            {
                var result = await _context.Tasks.FindAsync(id);
                task.Files = result.Files;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskID))
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
            ViewData["AssignedUserID"] = new SelectList(_context.Users, "UserID", "Email", task.AssignedUserID);
            ViewData["SprintID"] = new SelectList(_context.Sprints, "SprintID", "SprintID", task.SprintID);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.AssignedUser)
                .Include(t => t.Sprint)
                .FirstOrDefaultAsync(m => m.TaskID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskID == id);
        }
    }
}
