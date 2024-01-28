using TestWebApplication.Models;

namespace TestWebApplication.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UserContext context)
        {
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            // Users
            var users = new User[]
            {
            new User
            {
                Username = "user1",
                Password = "password1", // Здесь лучше использовать хэшированный пароль, а не просто текст
                Email = "user1@example.com",
                Role = "User"
            },
            new User
            {
                Username = "user2",
                Password = "password2",
                Email = "user2@example.com",
                Role = "User"
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
        }
    }
}
