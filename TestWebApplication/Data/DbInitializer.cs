using Microsoft.EntityFrameworkCore;
using TestWebApplication.Controllers;
using TestWebApplication.Models;

namespace TestWebApplication.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            // Users
            var users = new User[]
            {
            new User
            {
                Username = "user",
                Password = AccountController.HashPassword("user"),
                Email = "user@example.com",
                Role = "User"
            },
            new User
            {
                Username = "manager",
                Password = AccountController.HashPassword("manager"),
                Email = "manager@example.com",
                Role = "Manager"
            },
            new User
            {
                Username = "admin",
                Password = AccountController.HashPassword("admin"),
                Email = "dmtrsedov@gmail.com",
                Role = "Admin"
            },
                // Добавьте дополнительные пользователи, если необходимо
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            // Projects
            var projects = new Project[]
            {
            new Project
            {
                ProjectName = "Project 1",
                Description = "Description for Project 1"
            },
            new Project
            {
                ProjectName = "Project 2",
                Description = "Description for Project 2"
            },
                // Добавьте дополнительные проекты
            };

            context.Projects.AddRange(projects);
            context.SaveChanges();

            // Sprints
            var sprints = new Sprint[]
            {
            new Sprint
            {
                ProjectID = 1,
                StartDate = DateTime.Parse("2022-01-01"),
                EndDate = DateTime.Parse("2022-01-15"),
                SprintName = "Sprint 1",
                Description = "Description for Sprint 1",
                Comment = "Comment for Sprint 1",
                Files = "Files for Sprint 1"
            },
            new Sprint
            {
                ProjectID = 1,
                StartDate = DateTime.Parse("2022-01-16"),
                EndDate = DateTime.Parse("2022-01-31"),
                SprintName = "Sprint 2",
                Description = "Description for Sprint 2",
                Comment = "Comment for Sprint 2",
                Files = "Files for Sprint 2"
            },
                // Добавьте дополнительные спринты
            };

            context.Sprints.AddRange(sprints);
            context.SaveChanges();

            // Tasks
            var tasks = new Models.Task[]
            {
            new Models.Task
            {
                SprintID = 1,
                TaskName = "Task 1",
                Description = "Description for Task 1",
                Status = "In Progress",
                Comment = "Comment for Task 1",
                Files = "Files for Task 1",
                AssignedUserID = 1
            },
            new Models.Task
            {
                SprintID = 1,
                TaskName = "Task 2",
                Description = "Description for Task 2",
                Status = "Completed",
                Comment = "Comment for Task 2",
                Files = "Files for Task 2",
                AssignedUserID = 2
            },
                // Добавьте дополнительные задачи
            };

            context.Tasks.AddRange(tasks);
            context.SaveChanges();
//            var resetCodes = new PasswordResetCode[]
//{
//            new PasswordResetCode
//            {
//                UserId = 1, 
//                Code = "1234", 
//                Expiration = DateTime.UtcNow.AddHours(1)
//            },
//            new PasswordResetCode
//            {
//                UserId = 2,
//                Code = "1234",
//                Expiration = DateTime.UtcNow.AddHours(1)
//    },
//    // Добавьте дополнительные коды сброса пароля
//};

//            context.PasswordResetCodes.AddRange(resetCodes);
//            context.SaveChanges();

        }
    }
}
