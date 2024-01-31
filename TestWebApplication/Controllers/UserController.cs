using Microsoft.AspNetCore.Mvc;
using TestWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace TestWebApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserContext _dbContext;

        public UserController(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Projects()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required");
            }
            var userProjects = await _dbContext.Projects
                .Where(p => p.Sprints.Any(s => s.Tasks.Any(t => t.AssignedUserID.ToString() == userId)))
                .ToListAsync();

            return View(userProjects);
        }
        public async Task<IActionResult> Sprints()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userSprints = await _dbContext.Sprints
                .Where(s => s.Tasks.Any(t => t.AssignedUserID.ToString() == userId))
                .ToListAsync();

            return View(userSprints);
        }
        public async Task<IActionResult> Tasks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userTasks = await _dbContext.Tasks
                .Where(t => t.AssignedUserID.ToString() == userId)
                .ToListAsync();

            return View(userTasks);
        }
    }
}
