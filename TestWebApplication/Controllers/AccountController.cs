using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestWebApplication.Data;
using TestWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;

namespace TestWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext _dbContext;
        public IActionResult Index()
        {
            return View();
        }
        public AccountController(UserContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Password = HashPassword(model.Password.Trim()),
                    Email = model.Email,
                    Role = "User"
                };
                // Проверяем, существует ли пользователь с таким же именем
                if (_dbContext.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Пользователь с таким именем уже существует.");
                    return View(model);
                }

                // Проверяем, существует ли пользователь с такой же почтой
                if (_dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Пользователь с такой почтой уже существует.");
                    return View(model);
                }


                // Добавляем пользователя и сохраняем изменения в базе данных
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                // Перенаправляем на страницу входа или другую подходящую страницу
                return RedirectToAction("Login", "Account");
            }

            // Если ModelState не является допустимым, возвращаемся к представлению регистрации с ошибками валидации
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Попытка входа пользователя
                User user = new User
                {
                    Username = model.Username,
                    Password = HashPassword(model.Password.Trim())
                };
                
                var result = await _dbContext.Users
                .FirstOrDefaultAsync(m => m.Username == user.Username);
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "Ошибка входа. Проверьте Ваш логин");
                    return View(model);
                }
                else if (result.Password == user.Password)
                {
                    // Сохраняем ID пользователя и роль в куках или другом хранилище
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, result.UserID.ToString()),
                            new Claim(ClaimTypes.Name, result.Username.ToString()),
                            new Claim(ClaimTypes.Role, result.Role)
                        };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Ошибка входа. Проверьте Ваш пароль.");
                    return View(model);
                }
            }
            // Если ModelState не является допустимым, возвращаемся к представлению входа с ошибками валидации
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null)
                {
                    // Генерация кода для сброса пароля
                    var code = Guid.NewGuid().ToString();

                    // Сохранение кода в базу данных
                    var resetCodeEntity = new PasswordResetCode
                    {
                        UserId = user.UserID,
                        Code = code,
                        Expiration = DateTime.UtcNow.AddHours(1)
                    };

                    _dbContext.PasswordResetCodes.Add(resetCodeEntity);
                    await _dbContext.SaveChangesAsync();

                    // Отправка кода на почту
                    await EmailService.SendEmailAsync(model.Email, "reset password", $"code:{code}");

                    return RedirectToAction("ResetPassword", "Account");
                }

                // Если пользователь не существует, не показываем это для безопасности
                return RedirectToAction("ResetPassword", "Account");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Поиск кода сброса пароля
            var resetCodeEntity = await _dbContext.PasswordResetCodes.FirstOrDefaultAsync(c => c.Code == model.Code && c.Expiration > DateTime.UtcNow);

            if (resetCodeEntity != null)
            {
                // Поиск пользователя по ID
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserID == resetCodeEntity.UserId);

                if (user != null)
                {
                    // Сброс пароля
                    user.Password = HashPassword(model.NewPassword);

                    // Удаление использованного кода сброса пароля
                    _dbContext.PasswordResetCodes.Remove(resetCodeEntity);

                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Login", "Account");
                }
            }

            // Если код не существует или неактуален, не показываем это для безопасности
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Logout()
        {
            // Выход из системы - удаление аутентификационных куки
            HttpContext.SignOutAsync();

            // Редирект на главную страницу или другую страницу по вашему выбору
            return RedirectToAction("Index", "Home");
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Преобразование пароля в байтовый массив
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Вычисление хэша SHA-256
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                // Преобразование хэша в строку HEX
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
