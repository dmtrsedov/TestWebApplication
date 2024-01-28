using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using TestWebApplication.Data;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class RegController : Controller
    {
        private readonly UserContext _dbContext;
        public IActionResult Index()
        {
            return View();
        }
        public RegController(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map ViewModel to Entity
                var user = new User
                {
                    Username = model.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    Email = model.Email,
                    Role = "User"
                };

                // Add user to the DbSet and save changes to the database
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                // Redirect to login page or any other appropriate page
                return RedirectToAction("Home", "Aut");
            }

            // If the ModelState is not valid, return to the registration view with validation errors
            return View(model);
        }
    }
}
